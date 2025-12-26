using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using TaskTracker.IO;

namespace TaskTrackerTester
{
    public class DiskTest
    {
        [Fact]
        public void WriteToFileTest()
        {
            TestInitializer initializer = new TestInitializer();
            List<FileRecord> contents = initializer.Init();

            using (Disk disk = new Disk())
            {
                disk.WriteToFile(contents);
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\DataBase.txt") == false)
            {
                throw new Exception("File not exists");
            }

            if(new FileInfo(Directory.GetCurrentDirectory() + @"\DataBase.txt").Length == 0)
            {
                throw new Exception("File has zero length");
            }
        }

        [Fact]
        public void ReadFromFileTest()
        {
            List<FileRecord> records = null;

            using (Disk disk = new Disk())
            {
                records = disk.ReadFromFile();
            }

            Assert.NotNull(records);
            Assert.True(records.Count() != 0);

            foreach (FileRecord record in records)
            {
                Assert.NotNull(record);
                Assert.True(record.ID != 0);
                Assert.NotNull(record.Description);
                Assert.NotEmpty(record.Description);
                Assert.NotEqual(record.CreatedAt, DateTime.MinValue);
                Assert.NotEqual(record.UpdatedAt, DateTime.MinValue);
            }
        }
    }
}
