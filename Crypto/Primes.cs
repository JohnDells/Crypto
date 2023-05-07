namespace Crypto
{
    public static class Primes
    {
        public static readonly uint[] Values = new uint[]
        {
              2,   3,   5,   7,  11,  13,  17,  19,
             23,  29,  31,  37,  41,  43,  47,  53,
             59,  61,  67,  71,  73,  79,  83,  89,
             97, 101, 103, 107, 109, 113, 127, 131,
            137, 139, 149, 151, 157, 163, 167, 173,
            179, 181, 191, 193, 197, 199, 211, 223,
            227, 229, 233, 239, 241, 251, 257, 263,
            269, 271, 277, 281, 283, 293, 307, 311
        };

        public static uint[] CubeRoots()
        {
            var result = new uint[64];
            var count = 0;
            foreach (var prime in Primes.Values)
            {
                var primeCubeRoot = Math.Pow(prime, ((double)1 / 3)) - 1;
                var foo = BinaryFraction(primeCubeRoot);
                result[count] = foo;
                count++;
            }
            return result;
        }

        public static uint[] SquareRoots(int count)
        {
            var K = new uint[count];
            for (var i = 0; i < count; i++)
            {
                var p0 = Primes.Values[i];
                K[i] = BinaryFraction(Math.Sqrt(p0));
            }
            return K;
        }

        private static uint BinaryFraction(double input)
        {
            if (input > 1) input = input - (int)input;
            var result = (uint)(input * Math.Pow(2, 32));
            return result;
        }

    }
}