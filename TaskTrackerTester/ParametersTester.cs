using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using TaskTracker.Interpritator;
using TaskTracker.Extentions;
using System.Reflection.Metadata;

namespace TaskTrackerTester
{
    public class ParametersTester
    {
        [Fact]
        public void GetCommandNameTest()
        {
            Parameters<string, string>lineParameters = new Parameters<string, string>();
            lineParameters.InitLine(@"add -Description ""Hello world""");

            string command = lineParameters.GetCommandName();

            Assert.NotEqual(command, "");
        }

        [Fact]
        public void GetParameterTest()
        {
            string commandName = string.Empty;
            string parameter = string.Empty;

            using (Parameters<string, string>lineParameter = new Parameters<string, string>())
            {
                lineParameter.InitLine(@"add -Description ""Hello world""");

                commandName = lineParameter.GetCommandName();
                parameter = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "add");
            Assert.Equal(parameter, "Hello world");

            int parameter2 = 0;

            using (Parameters<int, string>lineParameter = new Parameters<int, string>())
            {
                lineParameter.InitLine(@"update -Id 1, -Description ""Hello world""");

                commandName = lineParameter.GetCommandName();
                parameter2 = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "update");
            Assert.Equal(parameter2, 1);

            parameter2 = 0;
            parameter = string.Empty;

            using (Parameters<string, int>lineParameter = new Parameters<string, int>())
            {
                lineParameter.InitLine(@"update -Description ""Hello world"" -Id 1");
                commandName = lineParameter.GetCommandName();
                parameter = lineParameter.GetParamater1();
                parameter2 = lineParameter.GetParameter2();
            }

            Assert.Equal(commandName, "update");
            Assert.Equal(parameter2, 1);
            Assert.Equal(parameter, "Hello world");

            commandName = string.Empty;
            parameter2 = 0;

            using (Parameters<int, int>lineParameter = new Parameters<int, int>())
            {
                lineParameter.InitLine("delete -Id 1");
                commandName= lineParameter.GetCommandName();
                parameter2 = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "delete");
            Assert.Equal(parameter2, 1);

            commandName = string.Empty;
            parameter = string.Empty;

            using (Parameters<string, string>lineParameter = new Parameters<string, string>())
            {
                lineParameter.InitLine("get-tasks -status done");
                commandName= lineParameter.GetCommandName();
                parameter = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "get-tasks");
            Assert.Equal(parameter, "done");

            commandName = string.Empty;
            parameter = string.Empty;

            using (Parameters<string, string>lineParameter = new Parameters<string, string>())
            {
                lineParameter.InitLine("get-tasks -status not_done");
                commandName= lineParameter.GetCommandName();
                parameter = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "get-tasks");
            Assert.Equal(parameter, "not_done");

            commandName = string.Empty;
            parameter = string.Empty;

            using (Parameters<string, string>lineParameter = new Parameters<string, string>())
            {
                lineParameter.InitLine("get-tasks -status progress");
                commandName= lineParameter.GetCommandName();
                parameter = lineParameter.GetParamater1();
            }

            Assert.Equal(commandName, "get-tasks");
            Assert.Equal(parameter, "progress");
        }
    }
}
