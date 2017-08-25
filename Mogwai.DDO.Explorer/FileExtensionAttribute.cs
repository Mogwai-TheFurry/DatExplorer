using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class FileExtensionAttribute : Attribute
    {
        public FileExtensionAttribute(string extension)
        {
            FileExtension = extension;
        }

        public string FileExtension { get; set; }
    }
}
