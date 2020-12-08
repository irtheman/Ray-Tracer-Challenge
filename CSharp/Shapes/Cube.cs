using System;

namespace CSharp
{
    public class Cube : RTObject
    {
        private readonly BoundingBox bounds;

        public Cube() : base()
        {
            bounds = new BoundingBox(new Point(-1, -1, -1), new Point(1, 1, 1));
        }

        public override BoundingBox Bounds => bounds;

        protected override Intersections LocalIntersect(Ray ray)
        {
            var intersections = new Intersections();

            if (Bounds.Intersects(ray))
            {
                CheckAxis(ray.Origin.x, ray.Direction.x, out double xtmin, out double xtmax);
                CheckAxis(ray.Origin.y, ray.Direction.y, out double ytmin, out double ytmax);

                if ((xtmin > ytmax) || (ytmin > xtmax))
                    return intersections;

                CheckAxis(ray.Origin.z, ray.Direction.z, out double ztmin, out double ztmax);

                var tmin = Math.Max(xtmin, Math.Max(ytmin, ztmin));
                var tmax = Math.Min(xtmax, Math.Min(ytmax, ztmax));

                if (tmin <= tmax)
                {
                    intersections.Add(new Intersection(tmin, this));
                    intersections.Add(new Intersection(tmax, this));
                }
            }

            return intersections;
        }

        protected override Vector LocalNormalAt(Point p, Intersection i)
        {
            var maxc = Math.Max(Math.Abs(p.x), Math.Max(Math.Abs(p.y), Math.Abs(p.z)));

            if (maxc.IsEqual(Math.Abs(p.x)))
            {
                return new Vector(p.x, 0, 0);
            }
            else if (maxc.IsEqual(Math.Abs(p.y)))
            {
                return new Vector(0, p.y, 0);
            }

            return new Vector(0, 0, p.z);
        }

        private void CheckAxis(double origin, double direction, out double tmin, out double tmax)
        {
            var tmin_numerator = (-1 - origin);
            var tmax_numerator = (1 - origin);

            tmin = tmin_numerator / direction;
            tmax = tmax_numerator / direction;

            if (tmin > tmax)
            {
                var temp = tmin;
                tmin = tmax;
                tmax = temp;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cube;
            return (other != null) && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Cube", Material, Transform);
        }

        public override string ToString()
        {
            return $"Cube(0, 0, 0): {Material} {Transform}";
        }
    }
}
