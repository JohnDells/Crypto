using System.Text;

namespace Crypto
{
    public static class DisplayExtensions
    {
        public static string BinaryString(this uint value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        public static string BinaryString(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static string BinaryString(this byte[] data, int columns = 8, bool rowNumbers = false)
        {
            var builder = new StringBuilder();
            var oneMinus = columns - 1;
            var row = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if (rowNumbers && (i % columns == 0))
                {
                    builder.Append($"W{row} ".PadLeft(5, ' '));
                }

                var b = Convert.ToString(data[i], 2).PadLeft(8, '0');
                builder.Append(b);
                if (i % columns == oneMinus)
                {
                    builder.AppendLine();
                    row++;
                }
                else
                {
                    builder.Append(" ");
                }
            }
            return builder.ToString();
        }

        public static string BinaryString(this uint[] data, int columns = 8, bool rowNumbers = false)
        {
            var builder = new StringBuilder();
            var row = 0;
            var count = 0;
            foreach (var value in data)
            {
                if (rowNumbers)
                {
                    builder.Append($"W{row} ".PadLeft(5, ' '));
                }

                var text = Convert.ToString(value, 2).PadLeft(32, '0');
                builder.Append(text);
                count++;
                if (count == columns)
                {
                    builder.AppendLine();
                    count = 0;
                }
                else
                {
                    builder.Append(" ");
                }
                row++;
            }
            return builder.ToString();
        }

        public static string MessageSchedule(this byte[] data)
        {
            //  The message schedule is 64 words of 4 bytes long.
            //  That is 256 bytes  (2048 bits) long.
            var builder = new StringBuilder();
            var length = data.Length / 4;
            for (var i = 0; i < length; i++)
            {
                var index = i * 4;
                builder.Append($"W{i} ".PadRight(4, ' '));
                builder.Append(data[index].BinaryString());
                if (length < index) break;
                builder.Append(data[index + 1].BinaryString());
                if (length < index + 1) break;
                builder.Append(data[index + 2].BinaryString());
                if (length < index + 2) break;
                builder.Append(data[index + 3].BinaryString());
                if (length < index + 3) break;
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public static string HexString(this byte[] data)
        {
            var builder = new StringBuilder();
            foreach (var item in data)
            {
                builder.Append(item.ToString("X"));
            }
            return builder.ToString();
        }

        public static string HexString(this uint[] data, int columns = 8, bool showColumns = false)
        {
            var builder = new StringBuilder();
            var count = 0;
            foreach (var value in data)
            {
                var text = value.ToString("X").PadLeft(8, '0');
                builder.Append(text);
                count++;

                if (showColumns)
                {
                    if (count == columns)
                    {
                        builder.AppendLine();
                        count = 0;
                    }
                    else
                    {
                        builder.Append(" ");
                    }
                }
            }
            return builder.ToString();
        }

        public static string HexString(this byte[] data, int columns = 8)
        {
            var builder = new StringBuilder();
            var count = 0;
            foreach (var value in data)
            {
                var text = value.ToString("X").PadLeft(8, '0');
                builder.Append(text);
                count++;

                if (count == columns)
                {
                    builder.AppendLine();
                    count = 0;
                }
                else
                {
                    builder.Append(" ");
                }
            }
            return builder.ToString();
        }

        //public static string ToHexString(this string text)
        //{
        //    var ba = Encoding.Default.GetBytes(text);
        //    var hexString = BitConverter.ToString(ba);
        //    var result = hexString.Replace("-", "");
        //    return result;
        //}
    }
}