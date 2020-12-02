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
        
        public double Schlick
        {
            get
            {
                var cos = EyeVector.Dot(NormalVector);

                if (N1 > N2)
                {
                    var n = N1 / N2;
                    var sin2t = n * n * (1.0 - cos * cos);
                    
                    if (sin2t > 1.0)
                    {
                        return 1.0;
                    }

                    var cost = Math.Sqrt(1.0 - sin2t);
                    cos = cost;
                }

                var a = (N1 - N2) / (N1 + N2);
                var r0 = a * a;

                return r0 + (1 - r0) * Math.Pow(1 - cos, 5);
            }
        }
    }
}
