using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mogwai.DDO.Explorer.Tests
{
    [TestClass]
    public class DatDirectoryTests
    {
        [TestMethod]
        public void DirectoryLoading()
        {
            DatDatabase dd = new DatDatabase(@"C:\Turbine\DDO\client_sound.dat");
            
        }
    }
}
