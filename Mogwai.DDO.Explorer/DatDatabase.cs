using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class DatDatabase
    {
        private const uint _offset = 0x140;
        private const uint _headerSize = 15 * sizeof(uint);

        private string _filename;
        private uint _blockSize = 0;
        private uint _rootOffset = 0;
        private uint _fileSize = 0;
        private uint _firstFreeBlock = 0;
        private uint _lastFreeBlock = 0;
        private uint _fileVersion = 0;
        private uint _freeBlockCount = 0;
        private uint _datpackEngine = 0;
        private uint _gameId = 0;

        private uint _magicNumber = 0;

        private readonly DatDirectory _rootDirectory;

        public DatDatabase(string filename)
        {
            _filename = filename;
            using (var dr = new DatReader(_filename))
            {
                dr.Seek(_offset);
                _magicNumber = dr.ReadUInt32();  // 0x0140
                _blockSize = dr.ReadUInt32();  // 0x0144
                _fileSize = dr.ReadUInt32(); // 0x0148
                _fileVersion = dr.ReadUInt32();  // 0x014C
                dr.ReadUInt32(); // unknown, 0x0150
                _firstFreeBlock = dr.ReadUInt32(); // guess?  0x0154
                _lastFreeBlock = dr.ReadUInt32(); // 0x0158
                _freeBlockCount = dr.ReadUInt32();  // 0x015C
                _rootOffset = dr.ReadUInt32(); // 0x0160

                dr.ReadUInt32(); // unknown, all 0s, 0x0164
                dr.ReadUInt32(); // unknown, all 0s, 0x0168
                dr.ReadUInt32(); // unknown, all 0s, 0x016C
                dr.ReadUInt32(); // unknown, all 0s, 0x0170

                _datpackEngine = dr.ReadUInt32(); // "datpack version engine" - from dndclient.log, 0x0174
                _gameId = dr.ReadUInt32(); // "datpack version game" - from dndclient.log, 0x0178

                _rootDirectory = new DatDirectory(_rootOffset, _blockSize, dr, this.AllFiles);
            }
        }

        public DatDirectory RootDirectory { get { return _rootDirectory; } }

        public List<DatFile> AllFiles { get; } = new List<DatFile>();

        public string FileName
        {
            get { return _filename; }
        }

        public byte[] GetData(uint offset, int length)
        {
            try
            {
                byte[] buffer = new byte[length];

                using (var dr = new DatReader(_filename))
                {
                    dr.Seek(offset, SeekOrigin.Begin);
                    dr.Read(buffer, 0, length);
                }

                return buffer;
            }
            catch (OutOfMemoryException)
            {
                return System.Text.ASCIIEncoding.ASCII.GetBytes("Not enough memory.");
            }
        }
    }
}
