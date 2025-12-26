using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskTracker.Interpritator;
using Xunit;

namespace TaskTrackerTester
{
    public class DispatcherTester
    {
        [Fact]
        public void DispatchTest()
        {
            using (Dispatcher<string>dispatcher = new Dispatcher<string>())
            {
                dispatcher.Dispatch(@"add -description ""Hello world 1");
                dispatcher.Dispatch(@"update -id 1 -description ""Hello world""");
                dispatcher.Dispatch(@"update -description ""Hello world"" -id 1");
            }

            using (Dispatcher<List<FileRecord>>dispatcher = new Dispatcher<List<FileRecord>>())
            {
                dispatcher.Dispatch("list");
            }
        }
    }
}
