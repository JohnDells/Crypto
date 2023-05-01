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
    }
}