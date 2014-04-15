using System;
using System.IO;

namespace FileStorage
{
    public class FileStorage
    {
        public string Dir { get; private set; }

        public FileStorage(string dir)
        {
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException();
            this.Dir = dir;
        }

        public string GetPath(int id)
        {
            var path = Path.Combine(
                Dir,
                String.Format(@"{0:D2}\{1:D4}\{2:D8}", id % 100, id % 10000, id)
            );

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return path;
        }

        // in MVC: uploadFile.SaveAs(storage.GetPath(id, "name.ext"));
        public string GetPath(int id, string filename)
        {
            return Path.Combine(GetPath(id), filename);
        }

        public bool HasFile(int id, string filename) {
            return File.Exists(GetPath(id, filename));
        }
    }
}
