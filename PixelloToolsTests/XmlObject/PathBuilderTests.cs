using Microsoft.VisualStudio.TestTools.UnitTesting;
using PixelloTools.XmlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelloTools.XmlObject.Tests
{
    [TestClass()]
    public class PathBuilderTests
    {
     
        [TestMethod()]
        public void FilePathTest()
        {
            var pathBuilder = new PathBuilder("Data","Settings",".Test");
            Assert.AreEqual(pathBuilder.FilePath(),"Data\\Settings.Test");
        }

        [TestMethod()]
        public void CustomFilePathTest()
        {
            var pathBuilder = new PathBuilder("Data", "Settings", ".Test");
            Assert.AreEqual(pathBuilder.FilePath("1234Test"), "Data\\1234Test.Test");
        }
    }
}