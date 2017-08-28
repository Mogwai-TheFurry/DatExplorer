using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public enum KnownFileType
    {
        Unknown,

        [FileExtension("ogg")]
        Ogg,

        [FileExtension("wav")]
        Wave,

        [FileExtension("zlib")]
        Zlib,

        [FileExtension("dxt1")]
        DXT1,

        [FileExtension("dxt3")]
        DXT3,

        [FileExtension("dxt5")]
        DXT5
    }
}
