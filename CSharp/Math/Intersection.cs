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

        public Computations PrepareComputations(Ray r)
        {
            var p = r.Position(t);
            var eyev = -r.Direction;
            var normv = Object.Normal(p);
            var inside = false;

            if (normv.Dot(eyev) < 0)
            {
                inside = true;
                normv = -normv;
            }

            return new Computations(t,
                                    Object,
                                    p,
                                    eyev,
                                    normv,
                                    inside);
        }
    }
}
