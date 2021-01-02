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

        protected override Vector LocalNormal(Point p, Intersection hit)
        {
            return new Vector(N2 * hit.u + N3 * hit.v + N1 * (1 - hit.u - hit.v));
        }

        public override bool Equals(object obj)
        {
            var other = obj as SmoothTriangle;
            return base.Equals(other) &&
                   N1.Equals(other.N1) &&
                   N2.Equals(other.N2) &&
                   N3.Equals(other.N3);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Smooth Triangle", base.GetHashCode(), N1, N2, N3);
        }

        public override string ToString()
        {
            return $"Smooth Triangle: {P1} {P2} {P3} {N1} {N2} {N3}";
        }
    }
}
