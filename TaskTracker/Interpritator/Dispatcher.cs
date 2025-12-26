using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Extentions;
using TaskTracker.Operations;

namespace TaskTracker.Interpritator
{
    public class Dispatcher<T>: IDisposable
    {
        public T Dispatch(string commandLine)
        {
            bool syntaxErrors = false;

            using (SyntaxAnalyzer analyzer = new SyntaxAnalyzer())
            {
                analyzer.CheckCommandLine(commandLine);
            }

            switch(commandLine.Split(' ')[0].ToLower())
            {
                case "list":
                    {
                        return ListTasks().ConvertToT<T>();
                    }
                case "add":
                    {
                        string description = string.Empty;

                        using (Parameters<string, string> parameters = new Parameters<string, string>())
                        {
                            parameters.InitLine(commandLine);
                            description = parameters.GetParamater1();
                        }

                        using (Operations.Operations operations = new Operations.Operations())
                        {
                            operations.AddTask( new Operations.Task() {
                                Description = description,
                                Status = _Status.todo,
                            });
                        }

                        return description.ConvertToT<T>();
                    }
                case "update":
                    {
                        string parameterValue = commandLine.Split(" ")[2];

                        if(parameterValue.IsStringDigit() == true)
                        {
                            string secondParameterName = commandLine.Split(' ')[3];

                            if (secondParameterName.ToLower() == "-description")
                            {
                                FileRecord task = new FileRecord();
                                string secondParameterValue = commandLine.Split('"')[1].Split('"')[0];

                                using (Operations.Operations operations = new Operations.Operations())
                                {
                                    task = operations.GetTaskById(Convert.ToInt32(parameterValue));
                                    task.Description = secondParameterValue;

                                    Update(Convert.ToInt32(parameterValue), new Operations.Task(DateTime.Now, true)
                                    {
                                        Description = task.Description,
                                        Status = task.Status
                                    });
                                }
                            }

                            if(secondParameterName.ToLower() == "-status")
                            {
                                FileRecord task = new FileRecord();
                                string secondParameterValue = commandLine.Split('-')[2].Split(' ')[1];

                                using (Operations.Operations operations = new Operations.Operations())
                                {
                                    task = operations.GetTaskById(Convert.ToInt32(parameterValue));
                                    
                                    switch(secondParameterValue.ToLower())
                                    {
                                        case "todo":
                                            {
                                                task.Status = _Status.todo;

                                                break;
                                            }
                                        case "progress":
                                            {
                                                task.Status = _Status.progress;

                                                break;
                                            }
                                        case "done":
                                            {
                                                task.Status = _Status.done;
                                                break;
                                            }
                                    }

                                    Update(Convert.ToInt32(parameterValue), new Operations.Task(DateTime.Now, true)
                                    {
                                        Description = task.Description,
                                        Status = task.Status
                                    });
                                }
                            }
                        }
                        else
                        {
                            string parameterName = commandLine.Split(' ')[1];
                            string secondParameterValue = string.Empty;
                            FileRecord task = new FileRecord();

                            if (parameterName == "-status")
                            {
                                parameterValue = commandLine.Split(' ')[2];
                                secondParameterValue = commandLine.Split(' ')[4];

                                using (Operations.Operations operations = new Operations.Operations())
                                {
                                    task = operations.GetTaskById(Convert.ToInt32(secondParameterValue));
                                    
                                    switch(parameterValue)
                                    {
                                        case "todo":
                                            {
                                                task.Status = _Status.todo;

                                                break;
                                            }
                                        case "progress":
                                            {
                                                task.Status = _Status.progress;
                                                break;
                                            }
                                        case "done":
                                            {
                                                task.Status = _Status.done;
                                                break;
                                            }
                                    }
                                }

                                Update(Convert.ToInt32(secondParameterValue), new Operations.Task(DateTime.Now, true)
                                {
                                    Description = task.Description,
                                    Status = task.Status
                                });

                                return (T)(object)null;
                            }

                            parameterValue = commandLine.Split('"')[1].Split('"')[0];
                            secondParameterValue = commandLine.Split('-')[2].Split(' ')[0];

                            if(secondParameterValue.ToLower() == "id")
                            {
                                task = new FileRecord();
                                string secondParameterValue2 = commandLine.Split('-')[2].Split(' ')[1];

                                using (Operations.Operations operations = new Operations.Operations())
                                {
                                    task = operations.GetTaskById(Convert.ToInt32(secondParameterValue2));
                                    task.Description = parameterValue;

                                    Update(Convert.ToInt32(secondParameterValue2), new Operations.Task(DateTime.Now, true) 
                                    {
                                        Description = task.Description,
                                        Status = task.Status
                                    });
                                }
                            }
                        }

                        break;
                    }
                case "get-tasks":
                    {
                        string parameterNane = commandLine.Split(' ')[1];

                        if (parameterNane != "-status")
                        {
                            throw new Exception("Parameter -status required...");
                        }

                        string parameterValue = commandLine.Split(' ')[2];
                        List<FileRecord> tasks = null;

                        using (Operations.Operations operations = new Operations.Operations())
                        {
                            switch(parameterValue)
                            {
                                case "todo":
                                    {
                                        tasks = operations.GetTasksByStatus(_Status.todo);

                                        break;
                                    }
                                case "progress":
                                    {
                                        tasks = operations.GetTasksByStatus(_Status.progress);

                                        break;
                                    }
                                case "done":
                                    {
                                        tasks = operations.GetTasksByStatus(_Status.done);

                                        break;
                                    }
                            }
                        }

                        return tasks.ConvertToT<T>();
                    }
                case "delete":
                    {
                        string parameterName = commandLine.Split(" ")[1];

                        if (parameterName != "-id")
                        {
                            throw new Exception("Parameter -id required...");
                        }

                        string parameterValue = commandLine.Split(' ')[2];

                        using (Operations.Operations operations = new Operations.Operations())
                        {
                            operations.DeleteTask(Convert.ToInt32(parameterValue));
                        }

                        break;
                    }
            }

            return (T)(object)null;
        }

        private List<FileRecord>ListTasks()
        {
            List<FileRecord> tasks = null;

            using (Operations.Operations operations = new Operations.Operations())
            {
                tasks = operations.GetTasks();
            }

            return tasks;
        }

        private void Update(int id, Operations.Task task)
        {
            using (Operations.Operations operations = new Operations.Operations())
            {
                operations.UpdateTask(task, id);
            }
        }

        private List<FileRecord> GetTasksByStatus(_Status status)
        {
            List<FileRecord>tasks = null;

            using (Operations.Operations operations = new Operations.Operations())
            {
                tasks = operations.GetTasksByStatus(status);
            }

            return tasks;
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
