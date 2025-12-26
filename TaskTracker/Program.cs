using System;
using System.Collections.Generic;
using TaskTracker.Interpritator;
using TaskTracker.IO;

namespace TaskTracker
{
    public static class TaskTracker
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Command Line Teask Tracker, version 1.0");

            string commandLine = string.Empty;

            while(commandLine.ToLower() != "quit")
            {
                Console.Write(". ");
                commandLine = Console.ReadLine();

                if(commandLine.ToLower() == "clear")
                {
                    Console.Clear();
                }
             
                if (commandLine.ToLower() == "list" || commandLine.Contains("get-tasks"))
                {
                    using (Dispatcher<List<FileRecord>>dispatcher = new Dispatcher<List<FileRecord>>())
                    {
                        List<FileRecord> tasks = null;

                        try
                        {
                            tasks = dispatcher.Dispatch(commandLine);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        if (tasks != null)
                        {
                            tasks.ForEach(x =>
                            {
                                Console.WriteLine();
                                Console.WriteLine(" ID: " + x.ID);
                                Console.WriteLine(" Description: " + x.Description);
                                Console.WriteLine(" Status: " + x.Status);
                                Console.WriteLine(" Created at: " + x.CreatedAt);
                                Console.WriteLine(" Updated at: " + x.UpdatedAt);
                                Console.WriteLine();
                            });
                        }
                    }
                }
                else
                {
                    using (Dispatcher<string>dispatcher = new Dispatcher<string>())
                    {
                        try
                        {
                            dispatcher.Dispatch(commandLine);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}