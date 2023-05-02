using System.Text;

namespace Crypto
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var message = "abc";
            var padded = Sha256.Pad(message);
            var converted = padded.ToUintArray();
            var blocks = converted.ToBlocks(16).ToList();
            var schedule = blocks.Schedule().ToList();
            var sha256 = schedule.Compress();
            Console.WriteLine(sha256.HexString());
        }
    }
}