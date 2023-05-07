using Crypto;
using NuGet.Frameworks;

namespace CryptoTest
{
    public class FermatPrimeTest
    {
        public readonly IPrimeTest Test = new FermatPrime();

        [Fact]
        public void Fermat_Prime_2_to_100()
        {
            for (uint i = 2; i < 100; i++)
            {
                var isPrime = Primes.Values.Contains(i);
                var testPrime = Test.IsPrime(i);
                Assert.Equal(isPrime, testPrime);
            }
        }

        [Fact]
        public void Fermat_Prime_561()
        {
            //  This is a carmichael number.
            Assert.False(Test.IsPrime(561));
        }

        [Fact]
        public void Fermat_Prime_75361()
        {
            //  This is a carmichael number.
            //Assert.False(Test.IsPrime(75361));
        }

        [Fact]
        public void Fermat_Prime_1111111121()
        {
            Assert.True(Test.IsPrime(1111111121));
        }
    }
}