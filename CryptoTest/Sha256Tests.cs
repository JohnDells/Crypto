using Crypto;

namespace CryptoTest
{
    public class Sha256Tests
    {
        [Fact]
        public void Chunks_Should_Chunks()
        {
            var data = new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var blocks = data.ToBlocks(4).ToList();
        }
        [Fact]
        public void Sha256_Should_Hash_abc()
        {
            Assert.Equal("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad", Sha256.Hash("abc").HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_1()
        {
            Assert.Equal("ca978112ca1bbdcafac231b39a23dc4da786eff8147c4e72b9807785afee48bb", Sha256.Hash("a").HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_2()
        {
            Assert.Equal("961b6dd3ede3cb8ecbaacbd68de040cd78eb2ed5889130cceb4c49268ea4d506", Sha256.Hash(new String('a', 2)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_32()
        {
            Assert.Equal("3ba3f5f43b92602683c19aee62a20342b084dd5971ddd33808d81a328879a547", Sha256.Hash(new String('a', 32)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_48()
        {

            Assert.Equal("97daac0ee9998dfcad6c9c0970da5ca411c86233a944c25b47566f6a7bc1ddd5", Sha256.Hash(new String('a', 48)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_55()
        {
            Assert.Equal("9f4390f8d30c2dd92ec9f095b65e2b9ae9b0a925a5258e241c9f1e910f734318", Sha256.Hash(new String('a', 55)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_56()
        {
            //  This is the first message with more than one block.
            //  aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //  b35439a4ac6f0948b6d6f9e3c6af0f5f590ce20f1bde7090ef7970686ec6738a
            Assert.Equal("b35439a4ac6f0948b6d6f9e3c6af0f5f590ce20f1bde7090ef7970686ec6738a", Sha256.Hash(new String('a', 56)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_57()
        {
            //  aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //  f13b2d724659eb3bf47f2dd6af1accc87b81f09f59f2b75e5c0bed6589dfe8c6
            Assert.Equal("f13b2d724659eb3bf47f2dd6af1accc87b81f09f59f2b75e5c0bed6589dfe8c6", Sha256.Hash(new String('a', 57)).HexString().ToLower());
        }
        [Fact]
        public void Sha256_Should_Hash_128()
        {
            //  aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //  6836cf13bac400e9105071cd6af47084dfacad4e5e302c94bfed24e013afb73e
            Assert.Equal("6836cf13bac400e9105071cd6af47084dfacad4e5e302c94bfed24e013afb73e", Sha256.Hash(new String('a', 128)).HexString().ToLower());
        }
    }
}