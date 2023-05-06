namespace Crypto
{
    public static class BMath
    {
        public static uint ShiftRight(this uint value, int bits)
        {
            return value >> bits;
        }

        public static uint RotateRight(this uint value, int bits)
        {
            return (value >> bits) | (value << (32 - bits));
        }

        public static uint Xor(this uint value, uint a)
        {
            return value ^ a;
        }

        public static uint Xor(this uint value, uint a, uint b)
        {
            return value ^ a ^ b;
        }

        /// <summary>
        /// This does NOT rotate the 33rd bit around.
        /// </summary>
        public static uint Add(this uint value, params uint[] values)
        {
            var result = value;
            if (values == null) return result;
            foreach (var i in values)
            {
                result += i;
            }
            return result;
        }

        public static uint Ch(this uint value, uint b, uint a)
        {
            //  foreach bit in value, if it is 0 take the bit from value0, else value1
            //  1010  choose
            //  1100  value1
            //  0011  value0
            //  ----
            //  1001  result
            return (b & value) | (a & ~value);
        }

        public static uint Maj(uint a, uint b, uint c)
        {
            //  if any two or all are 1, the result is 1.
            return (a & b) | (a & c) | (b & c);
        }

        public static uint σ0(this uint value)
        {
            var a = value.RotateRight(7);
            var b = value.RotateRight(18);
            var c = value.ShiftRight(3);
            return a.Xor(b, c);
        }

        public static uint σ1(this uint value)
        {
            var a = value.RotateRight(17);
            var b = value.RotateRight(19);
            var c = value.ShiftRight(10);
            return a.Xor(b, c);
        }

        public static uint Σ0(this uint value)
        {
            var a = value.RotateRight(2);
            var b = value.RotateRight(13);
            var c = value.RotateRight(22);
            return a.Xor(b, c);
        }

        public static uint Σ1(this uint value)
        {
            var a = value.RotateRight(6);
            var b = value.RotateRight(11);
            var c = value.RotateRight(25);
            return a.Xor(b, c);
        }

        public static uint PowerMod(this uint a, uint power, uint mod)
        {
            //  We use the power squares algorithm:
            //  x^15 mod m = x^(1+2+4+8) mod m = (x^1 mod m) * (x^2 mod m) * etc.
            //  where each multiplier can be modded individually to keep the math fast.

            //  TRICKY:  To multiple 32-bit uints, we need a 64-bit ulong to hold them.
            //      Once we're done and we mod to a 32-bit uint, the result will be a uint.
            ulong result = 1;
            ulong running = a;
            for (var i = 0; i < 32; i++)
            {
                var shift = (power >> i);
                if (shift == 0) break;
                var theBit = shift & 0b1;
                if (theBit == 1)
                {
                    result = (result * running) % mod;
                }

                //  Square running every loop.
                running = (running * running) % mod;
            }

            return (uint)result;
        }

        public static int RandomRange(int min, int max)
        {
            var random = new Random((int)(DateTime.Now.Ticks & 0b1111111111111111));
            var result = random.Next(min, max);
            return result;
        }

        public static uint GCD(uint value1, uint value2)
        {
            if (value1 == 0 || value2 == 0) throw new ArgumentException("There is no GCD for zero.");
            //  Given value2 < value1, divide value1 by value2, then value2 by the remainder, until you get zero.
            //  i.e.  GCD(40, 30) ->  40 % 30 = 10.  30 % 10 = 0.  Therefore, the GCD = 10.

            var firstGreater = value1 > value2;
            var a = firstGreater ? value1 : value2;
            var b = firstGreater ? value2 : value1;

            while (true)
            {
                var r = a % b;
                if (r == 0) return b;
                a = b;
                b = r;
            }
        }

        public static uint[] Random()
        {
            //  long = 64 bits
            //  For RSA we need a key size of 512 bits  (64 bytes = 16 uints)
            var foo = (uint)DateTime.Now.Ticks;
            //  i.e.  621 476 824 ~ 2^29  (29 bits)
            var bytes = new uint[] { foo }.ToByteArray();

            //  This gives 8 uints (256 bits).
            var result = Sha256.Hash(bytes);

            return result;
        }
    }
}