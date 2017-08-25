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

        private uint _magicNumber = 0;

        private readonly DatDirectory _rootDirectory;

        public DatDatabase(string filename)
        {
            _filename = filename;
            using (var dr = new DatReader(_filename))
            {
                dr.Seek(_offset);
                _magicNumber = dr.ReadUInt32();
                _blockSize = dr.ReadUInt32();
                _fileSize = dr.ReadUInt32();
                _fileVersion = dr.ReadUInt32();
                dr.ReadUInt32(); // unknown
                _firstFreeBlock = dr.ReadUInt32(); // guess?
                _lastFreeBlock = dr.ReadUInt32();
                _freeBlockCount = dr.ReadUInt32();
                _rootOffset = dr.ReadUInt32();
                
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
            byte[] buffer = new byte[length];

            using (var dr = new DatReader(_filename))
            {
                dr.Seek(offset, SeekOrigin.Begin);
                dr.Read(buffer, 0, length);
            }

            return buffer;
        }
    }
}
