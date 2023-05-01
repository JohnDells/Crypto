using System.Text;

namespace Crypto
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //  Start with message as a string.
            var message = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            //  Convert to a byte array.  (8 bits per byte)
            var bytes = Encoding.ASCII.GetBytes(message);
            //  Convert to a uint array.  (32 bits per byte)
            var uints = bytes.ToUintArray();
            //  Pad to blocks of 512 bits.  (16 uints)
            var blocks = uints.Blocks(16);
            //  Expand each block to 64 rows and calculate message schedule.

            //  For each block, compress - use registers from previous block as input to current block.



            //var sha256 = Sha256.Hash(message);

            //Console.WriteLine(message);
            //Console.WriteLine("REGISTERS:");
            //Console.WriteLine(sha256.BinaryString(1));
            //Console.WriteLine(sha256.HexString());
        }
    }
}