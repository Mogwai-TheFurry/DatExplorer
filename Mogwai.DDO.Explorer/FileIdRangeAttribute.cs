using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FileIdRangeAttribute : Attribute
    {
        public uint BeginRange { get; set; }

        public uint EndRange { get; set; }

        public FileIdRangeAttribute(uint beginRange, uint endRange)
        {
            this.BeginRange = beginRange;
            this.EndRange = endRange;
        }
    }
}
