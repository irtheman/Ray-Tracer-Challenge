using System;

namespace CSharp
{
    public class Triangle :RTObject
    {
        public Triangle(Point p1, Point p2, Point p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            E1 = new Vector(p2 - p1);
            E2 = new Vector(p3 - p1);
            Normal = E2.Cross(E1).Normalize;
        }

        public Point P1 { get; }
        public Point P2 { get; }
        public Point P3 { get; }
        public Vector E1 { get; }
        public Vector E2 { get; }
        public Vector Normal { get; }

        public override BoundingBox Bounds => throw new System.NotImplementedException();

        protected override Intersections LocalIntersect(Ray ray)
        {
            var results = new Intersections();

            var dirCrossE2 = ray.Direction.Cross(E2);
            var det = E1.Dot(dirCrossE2);

            if (Math.Abs(det) < MathHelper.Epsilon)
            {
                return results;
            }

            var f = 1.0 / det;
            var p1ToOrigin = new Vector(ray.Origin - P1);
            var u = f * p1ToOrigin.Dot(dirCrossE2);
            if ((u < 0) || (u > 1))
            {
                return results;
            }

            var originCrossE1 = p1ToOrigin.Cross(E1);
            var v = f * ray.Direction.Dot(originCrossE1);
            if ((v < 0) || (u + v > 1))
            {
                return results;
            }

            var t = f * E2.Dot(originCrossE1);
            results.Add(new Intersection(t, this, u, v));

            return results;
        }

        protected override Vector LocalNormalAt(Point p, Intersection i)
        {
            return Normal;
        }
    }
}
