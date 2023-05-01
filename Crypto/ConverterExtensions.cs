namespace Crypto
{
    public static class ConverterExtensions
    {
        public static uint[] ToUintArray(this byte[] data)
        {
            var dataLength = data.Length;
            var length = dataLength.Chunks(4);
            var result = new uint[length];

            var count = 0;
            for (var i = 0; i < dataLength; i++)
            {
                var mod = i % 4;
                var shift = 8 * (3 - mod);
                var newBits = (uint)data[i] << shift;
                result[count] = result[count] | newBits;
                if (mod == 3) count++;
            }

            return result;
        }

        public static int Chunks(this int totalLength, int chunkLength)
        {
            return ((totalLength - 1) / chunkLength) + 1;
        }

        public static IEnumerable<uint[]> Blocks(this uint[] data, int size)
        {
            var chunks = data.Length.Chunks(size);
            //  TRICKY:  BlockCopy assumes bytes (8 bits), but we need to copy 32 bit uints.
            var uintSize = 4 * size;
            for (var i = 0; i < chunks; i++)
            {
                var item = new uint[size];
                Buffer.BlockCopy(data, i * uintSize, item, 0, uintSize);
                yield return item;
            }
        }
    }
}