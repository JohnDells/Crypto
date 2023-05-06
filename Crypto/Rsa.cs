using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public static class Rsa
    {
        public static (uint sk, uint pk, uint mod) Generate()
        {
            //  Choose two large primes p and q.
            //  They should be about the same length, about 512 bits each.
            var p = Primes.Random1();
            var q = Primes.Random2();
            //  n should be about 1024 bits.
            var n = p * q;
            var t = (p - 1) * (q - 1);

            //  Select public key E
            //      - must be prime
            //      - must be less than t.
            //      - must not be a factor of t.

            //  Select private key D
            //      - (D * E) mod t = 1


            return (5, 7, 11);
        }

        public static uint Encrypt(string input, uint privateKey, uint keyModulus)
        {
            var message = Encoding.ASCII.GetBytes(input).ToUintArray();
            var m1 = message[0];
            return m1.PowerMod(privateKey, keyModulus);
        }


        public static string Decrypt(this uint input, uint publicKey, uint keyModulus)
        {
            var result = input.PowerMod(publicKey, keyModulus);
            var foo = result.ToByteArray();
            return "";
        }

    }
}
