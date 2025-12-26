using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskTracker.Extentions
{
    public static class ListExtentions
    {
        public static string ToJson(this List<FileRecord> content)
        {
            string resultContent = "[";
            resultContent += string.Concat(content.Select(x => @"{""ID"":" + x.ID +
                                                  @",""Description"":""" + x.Description +
                                                  @",""Status"":" + (int)x.Status +
                                                  @",""CreatedAt"":" + x.CreatedAt.ToString() +
                                                  @", ""UnpdatedAt"":" + x.UpdatedAt.ToString() + "},").ToArray());
            resultContent = resultContent.Substring(0, resultContent.Length - 1);
            resultContent += "]";

            return resultContent;
        }

        public static T ConvertToT<T>(this List<FileRecord> content)
        {
            return (T)(object)content;
        }
    }
}
