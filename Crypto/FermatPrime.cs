namespace Crypto
{
    public class FermatPrime : IPrimeTest
    {
        public bool IsPrime(uint p)
        {
            if (p == 2 || p == 3 || p == 5 || p == 7) return true;

            //  a^(p-1) mod p != 1 only if not prime
            if (((uint)2).PowerMod(p - 1, p) != 1) return false;
            if (((uint)3).PowerMod(p - 1, p) != 1) return false;
            if (((uint)5).PowerMod(p - 1, p) != 1) return false;
            if (((uint)7).PowerMod(p - 1, p) != 1) return false;

            return true;
        }
    }
}