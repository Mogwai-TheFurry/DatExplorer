using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class DatFile
    {
        public uint FileOffset { get; set; }

        public uint FileId { get; set; }

        public uint Size1 { get; set; }

        public uint Size2 { get; set; }

        public uint Timestamp { get; set; }

        public DateTime FileDate
        {
            get
            {
                return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(Timestamp);
            }
        }

        public uint Version { get; set; }

        public uint Unknown1 { get; set; }

        public uint Unknown2 { get; set; }

        public uint FileType { get; set; }

        public string UserDefinedName { get; set; }

        public CompressionType CompressionLevel
        {
            get { return (CompressionType)(FileType & 0x00FF); }
        }

        public static DatFile FromDirectoryBuffer(byte[] buffer, int index)
        {
            DatFile df = new DatFile();
            df.Unknown1 = BitConverter.ToUInt32(buffer, index);
            df.FileType = BitConverter.ToUInt32(buffer, index + 4);
            df.FileId = BitConverter.ToUInt32(buffer, index + 8);
            df.FileOffset = BitConverter.ToUInt32(buffer, index + 12);
            df.Size1 = BitConverter.ToUInt32(buffer, index + 16);
            df.Timestamp = BitConverter.ToUInt32(buffer, index + 20);
            df.Unknown2 = BitConverter.ToUInt32(buffer, index + 24);
            df.Size2 = BitConverter.ToUInt32(buffer, index + 28);

            if (df.FileId > 0 && df.FileOffset > 0)
                return df;
            else
                return null;
        }

        public static KnownFileType GetActualFileType(byte[] fileBuffer)
        {
            uint dword1 = BitConverter.ToUInt32(fileBuffer, 0);
            ushort word1 = BitConverter.ToUInt16(fileBuffer, 0);
            uint dword2 = BitConverter.ToUInt32(fileBuffer, 4);
            uint dword3 = BitConverter.ToUInt32(fileBuffer, 8);
            // uint dword4 = BitConverter.ToUInt32(fileBuffer, 12);
            // uint dword5 = BitConverter.ToUInt32(fileBuffer, 16);

            switch (dword1)
            {
                case 1179011410:
                    // "RIFF"
                    if (dword3 == 1163280727)
                        return KnownFileType.Wave;
                    break;
                case 1399285583:
                    // "OggS"
                    if (dword2 == 512 && dword3 == 0)
                        return KnownFileType.Ogg;
                    break;
                case 827611204:
                    return KnownFileType.DXT1;
                case 827611206:
                    return KnownFileType.DXT3;
                case 827611208:
                    return KnownFileType.DXT5;
            }

            switch(dword3)
            {
                case 827611204:
                    return KnownFileType.DXT1;
                case 861165636:
                    return KnownFileType.DXT3;
                case 894720068:
                    return KnownFileType.DXT5;
            }
            
            switch(word1)
            {
                case 30876:
                case 30874:
                    return KnownFileType.Zlib;
            }

            return KnownFileType.Unknown;
        }

        public static byte[] CreateBitmap(KnownFileType fileType, byte[] data, int width, int height)
        {
            byte[] result = null;

            switch(fileType)
            {
                case KnownFileType.DXT1:
                    result = DxtUtil.DecompressDxt1(data, width, height);
                    break;
                case KnownFileType.DXT3:
                    result = DxtUtil.DecompressDxt3(data, width, height);
                    break;
                case KnownFileType.DXT5:
                    result = DxtUtil.DecompressDxt5(data, width, height);
                    break;
            }

            return result;
        }
    }
}
