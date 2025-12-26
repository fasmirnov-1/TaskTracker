using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Operations
{
    public class Task
    {
        public string Description { get; set; }
        public _Status Status { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public Task()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Task(DateTime taskDt, bool TaskUpdated)
        {
            if(TaskUpdated)
            {
                this.UpdatedAt = taskDt;
            }
            else
            {
                this.CreatedAt = taskDt;
            }
        }
    }
}
