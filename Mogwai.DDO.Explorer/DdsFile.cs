using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpImageLibrary;
using static CSharpImageLibrary.ImageFormats;

namespace Mogwai.DDO.Explorer
{
    public class DdsFile
    {
        public static byte[] ConvertFromDxt(KnownFileType dxtType, byte[] data, uint width, uint height)
        {
            byte[] dds = new byte[128 + data.Length];
            dds[0] = 0x44; // "D"
            dds[1] = 0x44; // "D"
            dds[2] = 0x53; // "S"
            dds[3] = 0x20; // " "

            dds[4] = 124; // size of remaining header

            // header flags
            dds[8] = 0x07;
            dds[9] = 0x10;
            dds[10] = 0x0A;


            var widthBytes = BitConverter.GetBytes(width);
            var heightBytes = BitConverter.GetBytes(height);
            var pitchBytes = BitConverter.GetBytes((uint)data.Length);

            Array.Copy(widthBytes, 0, dds, 12, 4);
            Array.Copy(heightBytes, 0, dds, 16, 4);
            Array.Copy(pitchBytes, 0, dds, 20, 4);

            dds[28] = 1; // mipmap count
            dds[76] = 32; // size of sub-struct, static
            dds[80] = 4; // DDPF_FOURCC

            dds[84] = 68; // "D"
            dds[85] = 88; // "X"
            dds[86] = 84; // "T"

            switch (dxtType)
            {
                case KnownFileType.DXT1:
                    dds[87] = 49; // "1"
                    break;
                case KnownFileType.DXT3:
                    dds[87] = 51; // "3"
                    break;
                case KnownFileType.DXT5:
                    dds[87] = 53; // "5"
                    break;
            }

            // DDSCAPS flags
            dds[108] = 8; // DDSCAPS_COMPLEX
            dds[109] = 16; // DDSCAPS_TEXTURE
            dds[110] = 64; // DDSCAPS_MIPMAP

            Array.Copy(data, 0, dds, 128, data.Length);
            return dds;
        }


        public static Bitmap DdsToBmp(int width, int height, byte[] rawData, bool preserveAlpha = true)
        {
            ImageEngineImage img = new ImageEngineImage(rawData);
            var newBytes = img.Save(new ImageEngineFormatDetails(ImageEngineFormat.BMP), MipHandling.Default);
            
            Bitmap bmImage = new Bitmap(new MemoryStream(newBytes));
            return bmImage;

            var pxFormat = PixelFormat.Format32bppRgb;

            if (preserveAlpha)
                pxFormat = PixelFormat.Format32bppArgb;

            Bitmap bitmap = new Bitmap(width, height);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, pxFormat);
            IntPtr scan = data.Scan0;
            int size = bitmap.Width * bitmap.Height * 4;

            unsafe
            {
                byte* p = (byte*)scan;
                for (int i = 0; i < size; i += 4)
                {
                    // iterate through bytes.
                    // Bitmap stores it's data in RGBA order.
                    // DDS stores it's data in BGRA order.
                    p[i] = rawData[i + 2]; // blue
                    p[i + 1] = rawData[i + 1]; // green
                    p[i + 2] = rawData[i];   // red
                    p[i + 3] = rawData[i + 3]; // alpha
                }
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
