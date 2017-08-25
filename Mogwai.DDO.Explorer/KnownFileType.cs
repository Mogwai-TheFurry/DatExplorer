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
        Wave
    }
}
