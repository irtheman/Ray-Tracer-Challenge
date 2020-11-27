using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Computations
    {
        public Computations(double time, RTObject obj, Point p, Vector ev, Vector nv, bool inside)
        {
            t = time;
            Object = obj;
            Point = p;
            EyeVector = ev;
            NormalVector = nv;
            Inside = inside;

            OverPoint = new Point(Point + NormalVector * MathHelper.Epsilon);
        }

        public double t { get; }
        public RTObject Object { get; }
        public Point Point { get; }
        public Point OverPoint { get; }
        public Vector EyeVector { get; }
        public Vector NormalVector { get; }
        public bool Inside { get; }

    }
}
