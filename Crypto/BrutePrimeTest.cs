namespace Crypto
{
    public class BrutePrimeTest : IPrimeTest
    {
        public bool IsPrime(uint value)
        {
            var sqrt = Math.Sqrt(value);
            for (var i = 2; i <= sqrt; i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }
    }
}