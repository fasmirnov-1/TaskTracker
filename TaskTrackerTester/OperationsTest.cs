using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using TaskTracker;
using TaskTracker.IO;
using TaskTracker.Operations;
using System.ComponentModel;

namespace TaskTrackerTester
{
    public class OperationsTest
    {
        [Fact]
        public void AddTaskTest()
        {
            List<FileRecord> records = null;

            Assert.True(File.Exists(Directory.GetCurrentDirectory() + @"\DataBase.txt"));

            long fileSize = new FileInfo(Directory.GetCurrentDirectory() + @"\DataBase.txt").Length;

            using (Operations operations = new Operations())
            {
                operations.AddTask(new TaskTracker.Operations.Task()
                {
                    Description = "test",
                    Status = _Status.done,
                });
            }

            long modifiedFileSize = new FileInfo(Directory.GetCurrentDirectory() + @"\DataBase.txt").Length;
        }

        [Fact]
        public void UpdateTaskTest()
        {
            Assert.True(File.Exists(Directory.GetCurrentDirectory() + @"\DataBase.txt"));

            FileRecord preRecord = new FileRecord();

            using (Disk disk = new Disk())
            {
                List<FileRecord> preRecords = disk.ReadFromFile();

                Assert.NotNull(preRecords);

                preRecord = preRecords.Where(x => x.ID == 1).FirstOrDefault();

                using (Operations operations = new Operations())
                {
                    TaskTracker.Operations.Task task = new TaskTracker.Operations.Task(DateTime.Now, true);
                    task.Description = "test";
                    task.Status = preRecord.Status;

                    operations.UpdateTask(task, 1);
                };

                List<FileRecord>postUpdateRecords = disk.ReadFromFile();
                FileRecord postUpdateRecord = postUpdateRecords.Where(x => x.ID == 1).FirstOrDefault();

                //Assert.NotEqual(preRecord.Description, postUpdateRecord.Description);
            }
        }

        [Fact]
        public void DeleteTaskTest()
        {
            Assert.True(File.Exists(Directory.GetCurrentDirectory() + @"\DataBase.txt"));

            long preFileSize = 0;

            using (Operations operations = new Operations())
            {
                operations.AddTask(new TaskTracker.Operations.Task() { Description = "test"});

                preFileSize = new FileInfo(Directory.GetCurrentDirectory() + @"\DataBase.txt").Length;

                operations.DeleteTask(1);
            }

            long postFileSize = new FileInfo(Directory.GetCurrentDirectory() + @"\DataBase.txt").Length;

            Assert.True(preFileSize > postFileSize);
        }
    }
}
