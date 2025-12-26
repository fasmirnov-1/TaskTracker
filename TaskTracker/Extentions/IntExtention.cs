using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Extentions
{
    public static class IntExtention
    {
        public static T ConvertToT<T>(this int value)
        {
            return (T)(object)value;
        }
    }
}
