using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Crypto
{
    public static class Sha256
    {
        public static uint[] Hash(string message)
        {
            var encoded = Encoding.ASCII.GetBytes(message); //  good
            var padded = encoded.Pad();                     //  good
            var converted = padded.ToUintArray();           //  good
            //var schedule = converted.MessageSchedule();     //  only copies first 16 uints??
            //var result = schedule.Compress();
            return converted;
        }

        public static byte[] Pad(this byte[] input)
        {
            //  Each byte is 8 bits.  We need 512 bit blocks.  This is 64 bytes
            //  The last 72 bits are reserved, 8 for 10000000 and 64 bits (8 bytes) for message length.

            //  Length in bytes.
            var length = input.Length;
            var paddingLength = 64 - (length % 64);

            //  Need to leave 9 bytes for break byte (1 byte) and message length (8 bytes) in bits.
            if (paddingLength < 9) paddingLength += 64;

            var finalLength = length + paddingLength;
            var result = new byte[finalLength];
            Buffer.BlockCopy(input, 0, result, 0, length);

            //  Set the 10000000 buffer byte
            result[length] = 128;

            //  Add message length
            var messageLengthBits = (ulong)(length * 8);
            var lengthInBits = BitConverter.GetBytes(messageLengthBits).Reverse().ToArray();
            Buffer.BlockCopy(lengthInBits, 0, result, finalLength - 8, 8);

            return result;
        }

        public static uint[] Schedule(this uint[] input)
        {
            //  Input = [4 bytes (32 bits) * 16 rows] = 512 bits.
            //  Output = [4 bytes (32 bits) * 64 rows] = 2,048 bits.

            //  256 bytes message schedule
            //  Each 4 bytes is a row W0, W1, to W64.
            var W = new uint[64];

            //  Copy the first 16 rows.
            Buffer.BlockCopy(input, 0, W, 0, 16);

            for (var t = 16; t < 64; t++)
            {
                W[t] = BMath.σ1(W[t - 2]) + W[t - 7] + BMath.σ0(W[t - 15]) + W[t - 16];
            }
            return W;
        }

        public static uint[] Compress(this uint[] data)
        {
            //  data should be 64 words of 32 bits each.

            var registers = Primes.SquareRoots(8);
            var constants = Primes.CubeRoots();

            //  Initial hash values.
            var a = registers[0];
            var b = registers[1];
            var c = registers[2];
            var d = registers[3];
            var e = registers[4];
            var f = registers[5];
            var g = registers[6];
            var h = registers[7];

            //  Each input block is 2048 bits, 64 uints long.

            var blocks = data.Length.Chunks(64);
            for (var block = 0; block < blocks; block++)
            {
                var a0 = a;
                var b0 = b;
                var c0 = c;
                var d0 = d;
                var e0 = e;
                var f0 = f;
                var g0 = g;
                var h0 = h;

                var offsetStart = block * 64;
                for (var x = 0; x < 64; x++)
                {
                    var offset = x + offsetStart;
                    var W = data[offset];

                    var K = constants[x];
                    var T1 = BMath.Σ1(e) + BMath.Ch(e, f, g) + h + K + W;
                    var T2 = BMath.Σ0(a) + BMath.Maj(a, b, c);

                    //  Move each register down 1 and set new register a and e.
                    h = g;
                    g = f;
                    f = e;
                    e = d;
                    d = c;
                    c = b;
                    b = a;
                    a = T1 + T2;
                    e += T1;
                }

                //  Lastly, take original hash value and add the registers.
                a += a0;
                b += b0;
                c += c0;
                d += d0;
                e += e0;
                f += f0;
                g += g0;
                h += h0;
            }
            return new uint[] { a, b, c, d, e, f, g, h };
        }
    }
}