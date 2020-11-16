using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Intersection
    {
        public Intersection(double time, RTObject obj)
        {
            t = time;
            Object = obj;
        }

        public double t { get; }
        public RTObject Object { get; }

    }
}
