namespace Crypto
{
    public struct Block
    {
        public Block()
        {
        }

        /// <summary>
        /// uint is unsigned 32 bit integer.  (0 to 4,294,967,295)
        /// 16 * 32 = 512 bits
        /// </summary>
        public byte[] M0 { get; set; } = new byte[32];
        public byte[] M1 { get; set; } = new byte[32];
        public byte[] M2 { get; set; } = new byte[32];
        public byte[] M3 { get; set; } = new byte[32];
        public byte[] M4 { get; set; } = new byte[32];
        public byte[] M5 { get; set; } = new byte[32];
        public byte[] M6 { get; set; } = new byte[32];
        public byte[] M7 { get; set; } = new byte[32];
        public byte[] M8 { get; set; } = new byte[32];
        public byte[] M9 { get; set; } = new byte[32];
        public byte[] M10 { get; set; } = new byte[32];
        public byte[] M11 { get; set; } = new byte[32];
        public byte[] M12 { get; set; } = new byte[32];
        public byte[] M13 { get; set; } = new byte[32];
        public byte[] M14 { get; set; } = new byte[32];
        public byte[] M15 { get; set; } = new byte[32];
    }
}