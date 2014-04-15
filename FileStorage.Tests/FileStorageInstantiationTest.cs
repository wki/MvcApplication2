using System;
using System.IO;
using FileStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileStorage.Tests
{
    [TestClass]
    public class FileStorageInstantiationTest : TempDirContainer
    {
        [TestMethod]
        [ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
        public void MissingRootDir()
        {
            var f = new FileStorage(@"C:\Non\Sense\Dir");
        }

        [TestMethod]
        public void ExistingRootDir()
        {
            CreateTempDirectory();

            var f = new FileStorage(Dir);
            Assert.IsTrue(true);

            RemoveTempDirectory();
        }
    }
}
