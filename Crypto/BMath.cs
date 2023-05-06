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

        public static uint PowerMod(this uint value, uint power, uint mod)
        {
            //  We use the power squares algorithm:
            //  x^15 mod m = x^(1+2+4+8) mod m = (x^1 mod m) * (x^2 mod m) * etc.
            //  where each multiplier can be modded individually to keep the math fast.

            uint result = 1;
            uint running = value;
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

            return result;
        }

        public static bool IsPrimeMillerRabin(this uint[] n)
        {
            //  uint is 32 bits
            return false;
        }

        public static bool IsPrimeMillerRabin(this uint n)
        {
            //  0.  Is 53 prime?  (n = 53)
            //  1.  Find n-1 = 2^k * m  (k and m are whole numbers)
            //      a.  Divide n-1 by 2^1, 2^2, 2^3 until you get a non-whole number.
            //      b.  52 = 2^2 * 13 = 4 * 13, so k = 2
            //  2.  Choose a:  1 < a < n-1
            //      a.  Pick a = 2  (between 1 and 52)
            //  3.  Compute b[0] = a^m (mod n), b[i] = b[i-1]^2
            //      a.  b[0] = 2^13 mod 53 = 8192 mod 53 = 30
            //      b.  if b[0] is 1 or -1, then it is prime (probably).
            //          * b[0] is the only step where +1 or -1 mean prime.
            //      c.  if not, we move to b[1] = b[0] ^ 2 mod 53 = 52 (-1)
            //      d.  +1 means it is composite, -1 means it is prime.
            //  4.  If it's neither, we keep going.

            //  Find largest k such that:  n-1 = 2^k * m
            //  (m must be an odd number)
            var k = 0;
            var foo = n - 1;
            while ((foo & 0b1) == 0)
            {
                foo = foo >> 1;
                k++;
            }
            var m = (uint)(n - 1) / (uint)Math.Pow(2, k);

            //  Choose a such that 1 < a < n
            var a = RandomRange(2, (int)(n - 1));
            var b0 = ((uint)a).PowerMod(m, n);
            if (b0 != 1 && b0 != (n - 1))
            {
                var b1 = b0.PowerMod(2, n);
                if (b1 != 1 && b1 != (n - 1))
                {
                    var b2 = b1.PowerMod(2, n);
                    if (b2 != 1 && b2 != (n - 1))
                    {
                        var b3 = b2.PowerMod(2, n);
                        if (b3 == 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static int RandomRange(int min, int max)
        {
            var random = new Random((int)(DateTime.Now.Ticks & 0b1111111111111111));
            var result = random.Next(min, max);
            return result;
        }

        public static bool IsPrimeBruteForce(this uint value)
        {
            var sqrt = Math.Sqrt(value);
            for (var i = 2; i <= sqrt; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
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