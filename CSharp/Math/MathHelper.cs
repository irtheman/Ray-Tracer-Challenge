using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public static class MathHelper
    {
        public const double Epsilon = 0.0001; // Adjusted per author in forums. 0.00001 is for production;
        public const int MaxRecursion = 20;

        public static readonly double SQRT2 = Math.Sqrt(2);

        public static bool IsEqual(this double a, double b)
        {
            return Math.Abs(a - b) < Epsilon;
        }
    }
}
