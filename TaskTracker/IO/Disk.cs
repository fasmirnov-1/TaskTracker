using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace TaskTracker.IO
{
    public class Disk: IDisposable
    {
        private readonly string path = null;

        public Disk()
        {
            path = Directory.GetCurrentDirectory() + @"\DataBase.txt";
        }
        public void WriteToFile(List<FileRecord>contents)
        {
            string jsonContent = string.Empty;

            using (JSONConverter converter = new JSONConverter())
            {
                jsonContent = converter.Serialize(contents);
            }

            bool writeCompleted = false;

            while (writeCompleted == false)
            {
                try
                {
                    File.WriteAllText(path, jsonContent);
                    writeCompleted = true;
                }
                catch { };
            }
        }

        public List<FileRecord> ReadFromFile()
        {
            if(!File.Exists(path))
            {
                return null;
            }

            string lineContents = string.Empty;

            while(lineContents == string.Empty)
            {
                try
                {
                    lineContents = File.ReadAllText(path);
                }
                catch { }
            }
            List<FileRecord>contents = new List<FileRecord>();

            using (JSONConverter converter = new JSONConverter())
            {
                contents = converter.Deserialize(lineContents);
            }

            return contents;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
