using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Extentions;

namespace TaskTracker.Interpritator
{
    public class SyntaxAnalyzer: IDisposable
    {
        public string[] commandNames = null;
        public SyntaxAnalyzer() {
            commandNames = new string[] {
                "add",
                "update",
                "delete",
                "get-tasks",
                "list"
            };
        }

        public void CheckCommandLine(string commandLine)
        {
            string commandName = commandLine.Split(' ')[0];

            bool commandRunned = false;

            switch (commandName.ToLower())
            {
                case "add":
                    {
                        string parameterName = string.Empty;

                        try
                        {
                            parameterName = commandLine.Split(" ")[1];
                        }
                        catch
                        {
                            throw new Exception("No parameter found...");
                        }

                        string parameterValue = string.Empty;

                        try
                        {
                            parameterValue = commandLine.Split('"')[1].Split('"')[0];
                        }
                        catch
                        {
                            throw new Exception("No parameter found...");
                        }

                        if (parameterName.ToLower() != "-description")
                        {
                            throw new Exception("No description...");
                        }

                        if (parameterValue.IsStringDigit() == true)
                        {
                            throw new Exception("Not a number...");
                        }

                        commandRunned = true;

                        break;
                    }
                case "update":
                    {
                        string firstParameter = string.Empty;
                        string secondParameter = string.Empty;

                        try
                        {
                            firstParameter = commandLine.Split(' ')[1]; ;
                            secondParameter = commandLine.Split(' ')[4];
                        }
                        catch
                        {
                            throw new Exception("No parameter found...");
                        }

                        if (firstParameter == "-description")
                        {
                            int quotesCount = commandLine.Where(x => x == '"').ToList().Count();

                            if (quotesCount != 2)
                            {
                                throw new Exception("Two parameters required...");
                            }

                            string parameter = commandLine.Split('"')[0].Split('-')[1];

                            if (parameter.Contains("description") == false && parameter.Contains("id") == false)
                            {
                                throw new Exception("No parameters found...");
                            }
                        }

                        commandRunned = true;

                        break;
                    }
                case "delete":
                    {
                        string parameterName = commandLine.Split(' ')[1];

                        if (parameterName != "-id")
                        {
                            throw new Exception("No id...");
                        }

                        string parameterValue = commandLine.Split(" ")[2];

                        if (parameterValue.IsStringDigit() == false)
                        {
                            throw new Exception("Nunber required...");
                        }

                        commandRunned = true;

                        break;
                    }
                case "get-tasks":
                    {
                        string parameterName = commandLine.Split(' ')[1];

                        if (parameterName != "-status")
                        {
                            throw new Exception("Status required...");
                        }

                        string parameterValue = commandLine.Split(' ')[2];

                        switch (parameterValue)
                        {
                            case "todo":
                                {
                                    return;
                                }
                            case "progress":
                                {
                                    return;
                                }
                            case "done":
                                {
                                    return;
                                }
                        }

                        throw new Exception("Status not found...");
                    }
                case "clear":
                    {
                        commandRunned = true;

                        break;
                    }
                case "list":
                    {
                        commandRunned = true;
                        break;
                    }
                case "quit":
                    {
                        commandRunned = true;

                        break;
                    }
            }

            if (commandRunned == false)
            {
                throw new Exception("Unrecognized command...");
            }
        }
        public void Dispose() {
            commandNames = null;

            GC.SuppressFinalize(this);
        }
    }
}
