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

        public Intersection(double time, RTObject obj, double u, double v) : this(time, obj)
        {
            U = u;
            V = v;
        }

        public double t { get; }
        public RTObject Object { get; }
        public double U { get; }
        public double V { get; }

        public Computations PrepareComputations(Ray r, Intersections xs)
        {
            List<RTObject> containers = new List<RTObject>();
            var p = r.Position(t);
            var eyev = -r.Direction;
            var normv = Object.NormalAt(p, this);
            var inside = false;

            if (normv.Dot(eyev) < 0)
            {
                inside = true;
                normv = -normv;
            }

            var reflv = r.Direction.Reflect(normv);
            var n1 = 1.0;
            var n2 = 1.0;

            foreach (var i in xs)
            {
                if (this == i)
                {
                    if (containers.Count == 0)
                    {
                        n1 = 1.0;
                    }
                    else
                    {
                        n1 = containers[containers.Count - 1].Material.RefractiveIndex;
                    }
                }

                if (containers.Contains(i.Object))
                {
                    containers.Remove(i.Object);
                }
                else
                {
                    containers.Add(i.Object);
                }

                if (this == i)
                {
                    if (containers.Count == 0)
                    {
                        n2 = 1.0;
                    }
                    else
                    {
                        n2 = containers[containers.Count - 1].Material.RefractiveIndex;
                    }

                    break;
                }
            }

            return new Computations(t,
                                    Object,
                                    p,
                                    eyev,
                                    normv,
                                    reflv,
                                    n1,
                                    n2,
                                    inside);
        }
    }
}
