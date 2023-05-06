namespace Crypto
{
    public static class ConverterExtensions
    {
        public static uint[] ToUintArray(this byte[] data)
        {
            var dataLengthBytes = data.Length;
            var resultLengthUints = dataLengthBytes.Chunks(4);
            var result = new uint[resultLengthUints];

            var count = 0;
            for (var i = 0; i < dataLengthBytes; i++)
            {
                var mod = i % 4;
                var shift = 8 * (3 - mod);
                var newBits = (uint)data[i] << shift;
                result[count] = result[count] | newBits;
                if (mod == 3) count++;
            }

            return result;
        }

        public static byte[] ToByteArray(this uint[] data)
        {
            //  bytes are 8 bits, uints are 32.
            var result = new byte[data.Length * 4];
            for (var i = 0; i < data.Length; i++)
            {
                var item = data[i].ToByteArray();
                Buffer.BlockCopy(item, 0, result, i * 4, 4);
            }
            return result;
        }

        public static byte[] ToByteArray(this uint data)
        {
            var result = new byte[4];
            result[0] = (byte)((data >> 24) & 0b11111111);
            result[1] = (byte)((data >> 16) & 0b11111111);
            result[2] = (byte)((data >> 8) & 0b11111111);
            result[3] = (byte)((data) & 0b11111111);
            return result;
        }

        public static int Chunks(this int totalLength, int chunkLength)
        {
            return ((totalLength - 1) / chunkLength) + 1;
        }

        public static IEnumerable<uint[]> ToBlocks(this uint[] data, int size)
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