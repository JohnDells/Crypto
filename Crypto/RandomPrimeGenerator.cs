namespace Crypto
{
    public class RandomPrimeGenerator : IRandomSource
    {
        public uint Generate()
        {
            var test = new MillerRabin();
            var seed = (int)(DateTime.Now.Ticks & 0b11111111111111111111111111111111);
            var random = new Random(seed);

            var result = (uint)(random.NextInt64() & 0b11111111111111111111111111111110 | 0b1);
            var isPrime = test.IsPrime(result);
            var count = 0;
            while (!isPrime)
            {
                result = result + 2;
                isPrime = test.IsPrime(result);
                count++;
            }

            return result;
        }
    }
}