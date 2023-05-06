using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto;

namespace CryptoTest
{
    public class RsaTest
    {
        [Fact]
        public void Rsa_Should_Encrypt_Decrypt()
        {
            Assert.Equal("a", Rsa.Encrypt("a", 29, 133).Decrypt(41, 133));
        }
    }
}
