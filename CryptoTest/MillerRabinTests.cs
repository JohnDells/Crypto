using Crypto;
using NuGet.Frameworks;

namespace CryptoTest
{
    public class MillerRabinTests
    {
        private readonly IPrimeTest Test = new MillerRabin();

        [Theory]
        [InlineData(3, 1)]  //  (3-1) = 2^1 * 1
        [InlineData(4, 3)]  //  (4-1) = 2^0 * 3
        [InlineData(5, 1)]  //  (5-1) = 2^2 * 1
        [InlineData(7, 3)]  //  (7-1) = 2^1 * 3
        [InlineData(8, 7)]  //  (8-1) = 2^0 * 7
        public void DivisorOfLargestPower_Should_Work(uint value, uint expected)
        {
            Assert.Equal(expected, MillerRabin.DivisorOfLargestPower(value));
        }

        [Fact]
        public void MillerRabin_Prime_2_to_100()
        {
            for (uint i = 2; i < 100; i++)
            {
                var isPrime = Primes.Values.Contains(i);
                var testPrime = Test.IsPrime(i);
                if (isPrime != testPrime)
                {
                    var foo = 1;
                }
                Assert.Equal(isPrime, testPrime);
            }
        }

        [Fact]
        public void MillerRabin_Prime_15()
        {
            Assert.False(Test.IsPrime(15));
        }

        [Fact]
        public void MillerRabin_Prime_9()
        {
            Assert.False(Test.IsPrime(9));
        }

        [Fact]
        public void MillerRabin_Prime_23()
        {
            Assert.True(Test.IsPrime(23));
        }

        [Fact]
        public void MillerRabin_Prime_53()
        {
            Assert.True(Test.IsPrime(53));
        }

        [Fact]
        public void MillerRabin_Prime_561()
        {
            //  This is a carmichael number.
            Assert.False(Test.IsPrime((uint)561));
        }

        [Fact]
        public void MillerRabin_Prime_75361()
        {
            //  This is a carmichael number.
            //  This is one that the fermat fails on.
            Assert.False(Test.IsPrime(75361));
        }


        [Fact]
        public void MillerRabin_Prime_1111111121()
        {
            Assert.True(Test.IsPrime(1111111121));
        }
    }
}