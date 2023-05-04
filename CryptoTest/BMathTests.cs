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

        [Fact]
        public void PowerMod_Should_10_2_Mod7()
        {
            Assert.Equal((uint)2, ((uint)10).PowerMod(2, 7));
        }

        [Fact]
        public void PowerMod_Should_2_1_Mod5()
        {
            Assert.Equal((uint)2, ((uint)2).PowerMod(1, 5));
        }
        [Fact]
        public void PowerMod_Should_2_2_Mod5()
        {
            Assert.Equal((uint)4, ((uint)2).PowerMod(2, 5));
        }
        [Fact]
        public void PowerMod_Should_2_3_Mod5()
        {
            Assert.Equal((uint)3, ((uint)2).PowerMod(3, 5));
        }
        [Fact]
        public void PowerMod_Should_2_4_Mod5()
        {
            Assert.Equal((uint)1, ((uint)2).PowerMod(4, 5));
        }

        [Fact]
        public void PowerMod_Should_4_10_Mod5()
        {
            Assert.Equal((uint)1, ((uint)4).PowerMod(10, 5));
        }

        [Fact]
        public void PowerMod_Should_Big()
        {
            Assert.Equal((uint)11, ((uint)1234567891).PowerMod(182299883, 12));
        }


        [Fact]
        public void IsPrimeBruteForce_Should_Work()
        {
            Assert.True(((uint)7).IsPrimeBruteForce());
            Assert.False(((uint)8).IsPrimeBruteForce());
            Assert.False(((uint)9).IsPrimeBruteForce());
            Assert.False(((uint)10).IsPrimeBruteForce());
            Assert.True(((uint)11).IsPrimeBruteForce());
        }
    }
}