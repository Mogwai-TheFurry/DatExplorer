using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class DatReader : IDisposable
    {
        private BinaryReader _reader;
        private FileStream _stream;
        private string _filename;

        public DatReader(string filePath)
        {
            _filename = filePath;
            _stream = new FileStream(_filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None);
            _reader = new BinaryReader(_stream);
        }

        public void Seek(uint offset, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            _stream.Seek(offset, seekOrigin);
        }

        public uint ReadUInt32()
        {
            return _reader.ReadUInt32();
        }

        public int Read(byte[] buffer, int index, int count)
        {
            return _reader.Read(buffer, index, count);
        }

        public void Dispose()
        {
            _reader.Dispose();
            _stream.Dispose();
        }
    }
}
