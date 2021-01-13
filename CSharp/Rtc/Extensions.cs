using System;
using System.Collections.Generic;
using System.Text;

namespace rtc
{
    public static class Extensions
    {
        public static bool IsEqual(this string str, string other)
        {
            return str.Equals(other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
