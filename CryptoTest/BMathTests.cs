using Crypto;

namespace CryptoTest
{
    public class BMathTests
    {
        [Fact]
        public void Chunks_Should_Chunk()
        {
            Assert.Equal(0, 0.Chunks(1));
            Assert.Equal(1, 1.Chunks(1));
            Assert.Equal(2, 2.Chunks(1));
            Assert.Equal(1, 1.Chunks(3));
            Assert.Equal(1, 2.Chunks(3));
            Assert.Equal(1, 3.Chunks(3));
            Assert.Equal(2, 4.Chunks(3));
            Assert.Equal(1, 10.Chunks(11));
            Assert.Equal(1, 11.Chunks(11));
            Assert.Equal(2, 12.Chunks(11));
        }
    }
}