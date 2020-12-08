using System;

namespace CSharp
{
    public class SmoothTriangle : Triangle
    {
        public SmoothTriangle(Point p1, Point p2, Point p3, Vector n1, Vector n2, Vector n3) : base(p1, p2, p3)
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
        }

        public Vector N1 { get; }
        public Vector N2 { get; }
        public Vector N3 { get; }

        protected override Vector LocalNormalAt(Point p, Intersection hit)
        {
            return new Vector(N2 * hit.U + N3 * hit.V + N1 * (1 - hit.U - hit.V));
        }
    }
}
