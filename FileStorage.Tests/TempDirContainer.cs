using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileStorage.Tests
{
    public class TempDirContainer
    {
        // automatically set by Test Framework when public
        public TestContext TestContext { get; set; }

        protected string Dir { get; set; }
 
        protected void CreateTempDirectory()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);

            TestContext.WriteLine(String.Format("Temp Dir: {0}", path));
            
            Dir = path;
        }

        protected void RemoveTempDirectory()
        {
            // be safe that we have something inside Dir.
            if (Dir.Length > 10)
                Directory.Delete(Dir, true);
        }
    }
}
