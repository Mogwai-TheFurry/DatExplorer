using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class DatConstants
    {
        public const uint MAX_FILES = 61;
        public const uint MAX_DIRS = 62;
        public const uint FILE_BLOCK_SIZE = 8 * sizeof(uint) * MAX_FILES;
        public const uint DIR_BLOCK_SIZE = 2 * sizeof(uint) * MAX_DIRS;

    }
}
