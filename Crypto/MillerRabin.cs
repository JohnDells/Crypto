namespace Crypto
{
    public class MillerRabin : IPrimeTest
    {
        public bool IsPrime(uint n)
        {
            if (n == 2 || n == 3 || n == 5 || n == 7) return true;
            if (n % 2 == 0) return false;

            //  0.  Is 53 prime?  (n = 53)
            //  1.  Find n-1 = 2^k * m  (k and m are whole numbers)
            //      a.  Divide n-1 by 2^1, 2^2, 2^3 until you get a non-whole number.
            //      b.  52 = 2^2 * 13 = 4 * 13, so k = 2
            //  2.  Choose a:  1 < a < n-1
            //      a.  Pick a = 2  (between 1 and 52)
            //  3.  Compute b[0] = a^m (mod n), b[i] = b[i-1]^2
            //      a.  b[0] = 2^13 mod 53 = 8192 mod 53 = 30
            //      b.  if b[0] is 1 or -1, then it is prime (probably).
            //          * b[0] is the only step where +1 or -1 mean prime.
            //      c.  if not, we move to b[1] = b[0] ^ 2 mod 53 = 52 (-1)
            //      d.  +1 means it is composite, -1 means it is prime.
            //  4.  If it's neither, we keep going.

            //  Find largest k such that:  n-1 = 2^k * m
            var m = DivisorOfLargestPower(n);

            //  Choose [a] such that 1 < a < n  (why wouldn't you always choose 2 or n-1?
            uint a = 2;

            //  Compute [a]^m mod n.
            var b0 = a.PowerMod(m, n);
            if (b0 == 1 || b0 == (n-1)) return true;

            //  Repeat this check recursively (max level 100)
            for (var i = 0; i < 100; i++)
            {
                var b1 = b0.PowerMod(2, n);
                if (b1 == 1) return false;
                if (b1 == (n - 1)) return true;
                b0 = b1;
            }

            return false;
        }

        public static uint DivisorOfLargestPower(uint n)
        {
            //  Find the highest power of 2 (k) that will go into n-1.
            var k = 0;
            var running = n - 1;
            while ((running & 0b1) == 0)
            {
                running = running >> 1;
                k++;
            }
            var m = ((n - 1) >> k);
            return m;
        }
    }
}