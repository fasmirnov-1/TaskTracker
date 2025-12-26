using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.IO;

namespace TaskTracker.Operations
{
    public class Operations: IDisposable
    {
        public void AddTask(Task task)
        {
            List<FileRecord> fileContents = AddNewRecord(task);

            using (Disk disk = new Disk())
            {
                disk.WriteToFile(fileContents);
            }
        }

        private List<FileRecord> AddNewRecord(Task task)
        {
            List<FileRecord> fileContents = null;

            using (Disk disk = new Disk())
            {

                fileContents = disk.ReadFromFile();

                if (fileContents == null)
                {
                    fileContents = new List<FileRecord>();
                    fileContents.Add(new FileRecord()
                    {
                        ID = 1,
                        Description = task.Description,
                        Status = task.Status,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                }
                else
                {
                    int id = fileContents[fileContents.Count() - 1].ID + 1;

                    fileContents.Add(new FileRecord()
                    {
                        ID = id,
                        Description = task.Description,
                        Status = task.Status,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                }
            }

            return fileContents;
        }

        public void UpdateTask(Task task, int id)
        {
            using (Disk disk = new Disk())
            {
                List<FileRecord>records = disk.ReadFromFile();
                if(records == null)
                {
                    throw new Exception("Database file not exists...");
                }

                FileRecord record = records.Where(x => x.ID == id).FirstOrDefault();
                record.Description = task.Description;
                record.Status = task.Status;
                int index = records.FindIndex(x => x.ID == id);

                records.RemoveAt(index);
                records.Insert(index, record);

                disk.WriteToFile(records);
            }
        }

        public void DeleteTask(int id)
        {
            List<FileRecord> fileRecords = null;

            using (Disk disk = new Disk())
            {
                fileRecords = disk.ReadFromFile();

                int index = fileRecords.FindLastIndex(x => x.ID == id);

                fileRecords.RemoveAt(index);

                int newId = 1;

                fileRecords = fileRecords.Select(x => {
                    x.ID = newId;
                    newId ++;
                    return x;
                }).ToList();

                disk.WriteToFile(fileRecords);
            }
        }

        public List<FileRecord> GetTasks()
        {
            List<FileRecord> tasks = null;

            using (Disk disk = new Disk())
            {
                tasks = disk.ReadFromFile();

                if(tasks == null)
                {
                    throw new Exception("Database file not exists...");
                }
            }

            return tasks;
        }

        public List<FileRecord> GetTasksByStatus(_Status status)
        {
            List<FileRecord> tasks = null;

            using (Disk disk = new Disk())
            {
                tasks = disk.ReadFromFile();

                if(tasks == null)
                {
                    throw new Exception("Database file not exists...");
                }
            }

            return tasks.Where(x => x.Status == status).ToList();
        }

        public FileRecord GetTaskById(int id)
        {
            List<FileRecord> tasks = GetTasks();

            return tasks.Where(x => x.ID == id).FirstOrDefault();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
