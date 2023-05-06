using Crypto;

namespace CryptoTest
{
    public class ConverterExtensionsTest
    {
        [Fact]
        public void ToByteArray_Should_Work()
        {
            Assert.Equal(new byte[] { 0, 0, 0, 1 }, ((uint)1).ToByteArray());
        }

        [Fact]
        public void ToByteArray_Should_Work_Zero()
        {
            Assert.Equal(new byte[] { 0, 0, 0, 0 }, ((uint)0).ToByteArray());
            Assert.Equal(new byte[] { 0, 0, 0, 1 }, ((uint)1).ToByteArray());
        }

        [Fact]
        public void ToByteArray_Should_Work_Max()
        {
            Assert.Equal(new byte[] { 0b11111111, 0b11111111, 0b11111111, 0b11111111 }, ((uint)0b11111111111111111111111111111111).ToByteArray());
        }

        [Fact]
        public void ToByteArrayMultiple_Should_Work_Min()
        {
            Assert.Equal(new byte[] { 0b0, 0b0, 0b0, 0b0, 0b0, 0b0, 0b0, 0b0 }, new uint[] { 0b0, 0b0 }.ToByteArray());
        }
        [Fact]
        public void ToByteArrayMultiple_Should_Work_Max()
        {
            Assert.Equal(new byte[] { 0b11111111, 0b11111111, 0b11111111, 0b11111111, 0b11111111, 0b11111111, 0b11111111, 0b11111111 }, new uint[] { 0b11111111111111111111111111111111, 0b11111111111111111111111111111111 }.ToByteArray());
        }
    }
}