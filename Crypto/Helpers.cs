namespace Crypto
{
    public static class Helpers
    {
        public static Block[] ToBlocks(this byte[] data)
        {
            var blockCount = (int)(data.Length / 8) + 1;
            var result = new Block[blockCount];
            return result;
        }


        /// <summary>
        /// These are the first 32 bits of the square root of the first 8 prime numbers.
        /// 8 blocks of 4 byte registers M0-M8 = 8*4*8=256 bits
        /// </summary>
        /// <returns></returns>
        public static byte[] GetInitialHash()
        {
            return new byte[32]
            {
                0x6a, 0x09, 0xe6, 0x67,
                0xbb, 0x67, 0xae, 0x85,
                0x3c, 0x6e, 0xf3, 0x72,
                0xa5, 0x4f, 0xf5, 0x3a,
                0x51, 0x0e, 0x52, 0x7f,
                0x9b, 0x05, 0x68, 0x8c,
                0x1f, 0x83, 0xd9, 0xab,
                0x5b, 0xe0, 0xcd, 0x19,
            };
        }
    }
}