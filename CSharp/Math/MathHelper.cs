using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public static class MathHelper
    {
        public const double Epsilon = 0.00001;
        public const int MaxRecursion = 20;

        public static bool IsEqual(this double a, double b)
        {
            return Math.Abs(a - b) < Epsilon;
        }
    }
}
