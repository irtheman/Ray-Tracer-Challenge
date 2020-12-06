using System;

namespace CSharp
{
    public class Cone : RTObject
    {
        private BoundingBox bounds;

        public Cone() : this(double.NegativeInfinity, double.PositiveInfinity, false)
        {
            // Nothing to do
        }

        public Cone(double min, double max, bool closed = false)
        {
            Minimum = min;
            Maximum = max;
            Closed = closed;

            bounds = null;
        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Closed { get; set; }

        public override BoundingBox Bounds
        {
            get
            {
                var a = Math.Abs(Minimum);
                var b = Math.Abs(Maximum);
                var limit = Math.Max(a, b);

                bounds = new BoundingBox(new Point(-limit, Minimum, -limit), new Point(limit, Maximum, limit));

                return bounds;
            }
        }

        protected override Intersections LocalIntersect(Ray ray)
        {
            var result = new Intersections();

            if (Bounds.Intersects(ray))
            {
                IntersectCaps(ray, result);

                var a = ray.Direction.x * ray.Direction.x - ray.Direction.y * ray.Direction.y + ray.Direction.z * ray.Direction.z;
                var b = 2.0 * ray.Origin.x * ray.Direction.x -
                        2.0 * ray.Origin.y * ray.Direction.y +
                        2.0 * ray.Origin.z * ray.Direction.z;
                var c = ray.Origin.x * ray.Origin.x - ray.Origin.y * ray.Origin.y + ray.Origin.z * ray.Origin.z;

                if (a.IsEqual(0.0))
                {
                    if (!b.IsEqual(0.0))
                    {
                        var t = -c / (2 * b);
                        result.Add(new Intersection(t, this));
                    }

                    return result;
                }

                var disc = b * b - 4 * a * c;

                if (disc < 0.0)
                {
                    return result;
                }

                var t0 = (-b - Math.Sqrt(disc)) / (2 * a);
                var t1 = (-b + Math.Sqrt(disc)) / (2 * a);
                if (t0 > t1)
                {
                    var temp = t0;
                    t0 = t1;
                    t1 = temp;
                }

                var y0 = ray.Origin.y + t0 * ray.Direction.y;
                if ((Minimum < y0) && (y0 < Maximum))
                {
                    result.Add(new Intersection(t0, this));
                }

                var y1 = ray.Origin.y + t1 * ray.Direction.y;
                if ((Minimum < y1) && (y1 < Maximum))
                {
                    result.Add(new Intersection(t1, this));
                }
            }

            return result;
        }

        protected override Vector LocalNormal(Point p)
        {
            var dist = p.x * p.x + p.z * p.z;

            if ((dist < 1) && (p.y >= Maximum - MathHelper.Epsilon))
            {
                return new Vector(0, 1, 0);
            }
            else if ((dist < 1) && (p.y <= Minimum + MathHelper.Epsilon))
            {
                return new Vector(0, -1, 0);
            }

            var y = Math.Sqrt(dist);
            if (y > 0)
            {
                y = -y;
            }

            return new Vector(p.x, y, p.z);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cylinder;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Cylinder", Material, Transform);
        }

        public override string ToString()
        {
            return $"Cylinder(0, 0): {Material} {Transform}";
        }

        private bool CheckCap(Ray ray, double t)
        {
            var x = ray.Origin.x + t * ray.Direction.x;
            var y = ray.Origin.y + t * ray.Direction.y;
            var z = ray.Origin.z + t * ray.Direction.z;

            return (x * x + z * z) <= (y * y);
        }

        private void IntersectCaps(Ray ray, Intersections xs)
        {
            if (!Closed || ray.Direction.y.IsEqual(0))
            {
                return;
            }

            var t = (Minimum - ray.Origin.y) / ray.Direction.y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }

            t = (Maximum - ray.Origin.y) / ray.Direction.y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }
        }
    }
}
