using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer.UI
{
    public static class ByteUtils
    {
        public static string PrintBytes(this byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));

                if ((i + 1) % 4 == 0)
                    sb.Append(" ");

                if ((i + 1) % 16 == 0)
                    sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string PrintAsciiBytes(this byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                byte thisByte = data[i];
                char thisChar = '.';

                if (thisByte > 31)
                    thisChar = Encoding.ASCII.GetString(data, i, 1)[0];

                sb.Append(thisChar);

                if ((i + 1) % 4 == 0)
                    sb.Append(" ");

                if ((i + 1) % 16 == 0)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
