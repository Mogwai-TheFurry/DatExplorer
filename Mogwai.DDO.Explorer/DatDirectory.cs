using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class DatDirectory
    {
        public uint Offset { get; set; }

        public uint Size { get; set; }

        private DatDirectory(uint offset, uint size)
        {
            Offset = offset;
            Size = size;
        }

        internal DatDirectory(uint offset, uint size, DatReader reader, List<DatFile> allFiles)
        {
            Offset = offset;
            Size = size;

            Parse(reader, allFiles);
        }

        private void Parse(DatReader reader, List<DatFile> allFiles)
        {
            reader.Seek(Offset); // actual directories are 2 DWORDS over, but validate those 2 DWORDS are 0
            uint word1 = reader.ReadUInt32();
            uint word2 = reader.ReadUInt32();

            if (word1 != 0 || word2 != 0)
                return;

            byte[] buffer = new byte[DatConstants.DIR_BLOCK_SIZE];
            reader.Read(buffer, 0, buffer.Length);

            // directories
            for (int i = 0; i < DatConstants.MAX_DIRS; i++)
            {
                uint newDirSize = BitConverter.ToUInt32(buffer, (i * 2) * sizeof(uint));
                uint newDirOffset = BitConverter.ToUInt32(buffer, (i * 2) * sizeof(uint) + 4);

                if (Size != newDirSize) break;
                if (newDirOffset == 0x00) break;
                if (newDirOffset == 0xCDCDCDCD) break;

                var subDir = new DatDirectory(newDirOffset, newDirSize);
                Directories.Add(subDir);
            }

            buffer = new byte[DatConstants.FILE_BLOCK_SIZE];
            reader.Read(buffer, 0, buffer.Length);

            // files
            for (int i = 0; i < DatConstants.MAX_FILES; i++)
            {
                // each file is 8 DWORDS
                int thisFile = i * 8 * sizeof(uint);
                DatFile df = DatFile.FromDirectoryBuffer(buffer, thisFile);

                if (df != null)
                {
                    Files.Add(df);
                    allFiles.Add(df);
                }
                else
                    break; // null file means end of listing
            }

            Directories.ForEach(d => d.Parse(reader, allFiles));
        }

        public List<DatDirectory> Directories { get; set; } = new List<DatDirectory>();

        public List<DatFile> Files { get; set; } = new List<DatFile>();
    }
}
