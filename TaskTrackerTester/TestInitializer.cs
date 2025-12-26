using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerTester
{
    public class TestInitializer
    {
        public List<FileRecord> Init()
        {
            List<FileRecord> contents = new List<FileRecord>();

            contents.Add(new FileRecord()
            {
                ID = 1,
                Description = "description",
                Status = _Status.progress,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            contents.Add(new FileRecord()
            {
                ID = 2,
                Description = "description",
                Status = _Status.progress,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            return contents;
        }
    }
}
