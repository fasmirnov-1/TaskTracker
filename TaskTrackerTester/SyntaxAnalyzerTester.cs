using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using TaskTracker.Interpritator;

namespace TaskTrackerTester
{
    public class SyntaxAnalyzerTester
    {
        [Fact]
        public void CheckCommandLineTest()
        {
            using (SyntaxAnalyzer analyzer = new SyntaxAnalyzer())
            {
                analyzer.CheckCommandLine(@"add -Description ""Hello world""");
                analyzer.CheckCommandLine(@"update -description ""Hello world"" -id 1");
                analyzer.CheckCommandLine("delete -id 1");
                analyzer.CheckCommandLine("get-tasks -status progress");
            }
        }
    }
}
