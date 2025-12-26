using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Extentions
{
    public static class StringExtentions
    {
        public static string[] ToContentParts(this string content)
        {
            string lineContent = content.Split('[')[1].Split(']')[0];
            List<string> lineParts = lineContent.Split('{').ToList();
            lineParts.RemoveAt(0);

            return lineParts.Select(x =>
            {
                if (x.Substring(x.Length - 1) == ",")
                {
                    return x.Replace("}", "").Substring(0, x.Length - 2);
                }

                return x.Replace("}", "").Substring(0, x.Length - 1);
            }).ToArray();
        }

        public static bool IsStringDigit(this string contents)
        {
            bool isDigit = true;

            contents.ToList().ForEach(x =>
            {
                isDigit = isDigit && char.IsDigit(x);
            });

            return isDigit;
        }

        public static T ConvertToT<T>(this string value)
        {
            bool digit = true;

            value.ToList().ForEach(x =>
            {
                digit = digit && char.IsDigit(x);
            });

            if(digit == true)
            {
                return (T)(object)Convert.ToInt32(value);
            }

            return (T)(object)value;
        }
    }
}
