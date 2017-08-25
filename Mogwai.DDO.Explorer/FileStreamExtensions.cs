using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public static class FileStreamExtensions
    {
        public static uint ReadUInt32(this FileStream stream, int offset = 0, bool advance = false)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, offset, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
