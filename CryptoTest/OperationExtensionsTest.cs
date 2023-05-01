using Crypto;

namespace CryptoTest
{
    public class OperationExtensionsTest
    {
        [Fact]
        public void Ch_Should_Choose()
        {
            Assert.Equal((uint)0b1, ((uint)0b0).Ch(0b0, 0b1));
            Assert.Equal((uint)0b1, ((uint)0b1).Ch(0b1, 0b0));
            Assert.Equal((uint)0b1111, ((uint)0b0000).Ch(0b0000, 0b1111));
            Assert.Equal((uint)0b1111, ((uint)0b1111).Ch(0b1111, 0b0000));


            Assert.Equal((uint)0b1010, ((uint)0b0011).Ch(0b0010, 0b1000));
        }

        [Fact]
        public void Xor_Should_ExclusiveOr()
        {
            Assert.Equal((uint)0b0, ((uint)0b0).Xor(0b0));
            Assert.Equal((uint)0b1, ((uint)0b1).Xor(0b0));
            Assert.Equal((uint)0b1, ((uint)0b0).Xor(0b1));
            Assert.Equal((uint)0b0, ((uint)0b1).Xor(0b1));

            Assert.Equal((uint)0b0110, ((uint)0b0011).Xor(0b0101));

        }

        [Fact]
        public void Maj_Should_Majority()
        {
            Assert.Equal((uint)0b0, BMath.Maj((uint)0b0, (uint)0b0, (uint)0b0));
            Assert.Equal((uint)0b0, BMath.Maj((uint)0b0, (uint)0b0, (uint)0b1));
            Assert.Equal((uint)0b0, BMath.Maj((uint)0b0, (uint)0b1, (uint)0b0));
            Assert.Equal((uint)0b1, BMath.Maj((uint)0b0, (uint)0b1, (uint)0b1));
            Assert.Equal((uint)0b0, BMath.Maj((uint)0b1, (uint)0b0, (uint)0b0));
            Assert.Equal((uint)0b1, BMath.Maj((uint)0b1, (uint)0b0, (uint)0b1));
            Assert.Equal((uint)0b1, BMath.Maj((uint)0b1, (uint)0b1, (uint)0b0));
            Assert.Equal((uint)0b1, BMath.Maj((uint)0b1, (uint)0b1, (uint)0b1));
        }
    }
}