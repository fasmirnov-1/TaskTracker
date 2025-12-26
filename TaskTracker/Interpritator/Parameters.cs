using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Extentions;

namespace TaskTracker.Interpritator
{
    public class Parameters<T, P>: IDisposable
    {
        private string commandName = null;
        private T parameter1;
        private P parameter2;

        public void InitLine(string commandLine)
        {
            commandName = commandLine.Split(' ')[0];

            string positionName = commandLine.Split(' ')[1];

            if (positionName.ToLower() == "-description")
            {
                if (typeof(T).Name == typeof(int).Name)
                {
                    throw new Exception("Positional parameter does not match with parameter type...");
                }
                parameter1 = commandLine.Split('"')[1].ConvertToT<T>();
            }

            if (positionName.ToLower() == "-id")
            {
                if (typeof(T) == typeof(string))
                {
                    throw new Exception("Positional parameter does not match with parameter type...");
                }

                parameter1 = commandLine.Split(' ')[2].Split(',')[0].ConvertToT<T>();
            }
            if(positionName.ToLower() == "-status")
            {
                if (typeof(T) == typeof(int))
                {
                    throw new Exception("Positional parameter does not match with parameter type...");
                }

                parameter1 = commandLine.Split(' ')[2].ConvertToT<T>();
            }


            if (commandLine.Split(' ').Length > 4)
            {
                positionName = commandLine.Split(' ')[4];

                if (positionName.ToLower() == "-description")
                {
                    if (typeof(P) == typeof(int))
                    {
                        throw new Exception("Positional parameter does not match with parameter type...");
                    }
                    parameter2 = commandLine.Split('"')[1].ConvertToT<P>();
                }

                if (positionName.ToLower() == "-id")
                {
                    if (typeof(P) == typeof(string))
                    {
                        throw new Exception("Positional parameter does not match with parameter type...");
                    }

                    parameter2 = commandLine.Split(' ')[5].ConvertToT<P>();
                }
            }
        }

        public string GetCommandName()
        {
            return commandName;
        }

        public T GetParamater1()
        {
            return parameter1;
        }

        public P GetParameter2()
        {
            return parameter2;
        }

        public void Dispose()
        {
            commandName = null;
            try
            {
                parameter1 = (T)(object)null;
            }
            catch { };

            try
            {
                parameter2 = (P)(object)null;
            }
            catch { };

            GC.SuppressFinalize(this);
        }
    }
}
