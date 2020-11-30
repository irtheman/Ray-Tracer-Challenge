using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Computations
    {
        public Computations(double time, RTObject obj, Point p, Vector ev, Vector nv, Vector rv, double n1, double n2, bool inside)
        {
            t = time;
            Object = obj;
            Point = p;
            EyeVector = ev;
            NormalVector = nv;
            ReflectVector = rv;
            N1 = n1;
            N2 = n2;
            Inside = inside;

            OverPoint = new Point(Point + NormalVector * MathHelper.Epsilon);
            UnderPoint = new Point(Point - NormalVector * MathHelper.Epsilon);
        }

        public double t { get; }
        public RTObject Object { get; }
        public Point Point { get; }
        public Point OverPoint { get; }
        public Point UnderPoint { get; }
        public Vector EyeVector { get; }
        public Vector NormalVector { get; }
        public Vector ReflectVector { get; }
        public double N1 { get; }
        public double N2 { get; }
        public bool Inside { get; }

    }
}
