using Crypto;

namespace CryptoTest
{
    public class RsaTest
    {
        [Fact]
        public void Rsa_Should_Generate_Primes()
        {
            var generator = new RandomPrimeGenerator();
            var test = new MillerRabin();
            var value = generator.Generate();
            Assert.True(test.IsPrime(value));
        }
    }
}