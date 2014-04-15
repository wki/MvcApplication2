using System;
using System.IO;
using FileStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileStorage.Tests
{
    [TestClass]
    public class FileStorageUsageTest : TempDirContainer
    {
        public FileStorage Storage { get; set; }

        [TestInitialize]
        public void InitializeFileStorage()
        {
            CreateTempDirectory();
            Storage = new FileStorage(Dir);

            TestContext.WriteLine("Storage Created, DIR: " + Dir);
        }

        [TestCleanup]
        public void CleanupFileStorage()
        {
            TestContext.WriteLine("About to Remove Storage");
            RemoveTempDirectory();
        }

        [TestMethod]
        public void DirIsRoot()
        {
            Assert.AreEqual(Dir, Storage.Dir);
        }

        [TestMethod]
        public void PathName1()
        {
            Assert.AreEqual(Path.Combine(Dir, @"47\2347\00012347"), Storage.GetPath(12347));
        }

        [TestMethod]
        public void PathName2()
        {
            Assert.AreEqual(Path.Combine(Dir, @"04\0004\00030004"), Storage.GetPath(30004));
        }

        [TestMethod]
        public void PathName3()
        {
            Assert.AreEqual(Path.Combine(Dir, @"01\0001\00000001"), Storage.GetPath(1));
        }

        [TestMethod]
        public void PathNameWithFile()
        {
            Assert.AreEqual(Path.Combine(Dir, @"47\2347\00012347\foo.txt"), Storage.GetPath(12347, "foo.txt"));
        }

        [TestMethod]
        public void PathExists()
        {
            Assert.IsTrue(Directory.Exists(Storage.GetPath(42)));
        }

        [TestMethod]
        public void FileDoesNotExist()
        {
            Assert.IsFalse(Storage.HasFile(32, "hello.txt"));
        }

        [TestMethod]
        public void FileExists()
        {
            var path = Storage.GetPath(9876, "hello.txt");
            using (var f = new StreamWriter(path))
            {
                f.WriteLine("Hello my File");
            }

            Assert.IsTrue(Storage.HasFile(9876, "hello.txt"));
        }
    }
}