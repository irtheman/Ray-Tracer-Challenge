using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public static class MathHelper
    {
#if DEBUG
        public const double Epsilon = 0.0001;
#else
        public const double Epsilon = 0.0001;
#endif
        public const int MaxRecursion = 20;

        public static readonly double SQRT2 = Math.Sqrt(2);

        public static bool IsEqual(this double a, double b)
        {
            if (a == b) return true;
            return Math.Abs(a - b) < Epsilon;
        }
    }
}
