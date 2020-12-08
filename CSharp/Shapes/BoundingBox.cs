using System;

namespace CSharp
{
    public class BoundingBox
    {
        public BoundingBox() : this(
                                    new Point(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity),
                                    new Point(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)
                                   )
        {
            // Nothing to do here
        }

        public BoundingBox(Point min, Point max)
        {
            Min = min;
            Max = max;
        }

        public Point Min { get; protected set; }
        public Point Max { get; protected set; }

        public void Add(Point p)
        {
            if (p.x < Min.x || p.y < Min.y || p.z < Min.z)
            {
                var minx = Math.Min(Min.x, p.x);
                var miny = Math.Min(Min.y, p.y);
                var minz = Math.Min(Min.z, p.z);

                Min = new Point(minx, miny, minz);
            }

            if (p.x > Max.x || p.y > Max.y || p.z > Max.z)
            {
                var maxx = Math.Max(Max.x, p.x);
                var maxy = Math.Max(Max.y, p.y);
                var maxz = Math.Max(Max.z, p.z);

                Max = new Point(maxx, maxy, maxz);
            }
        }

        public void Add(BoundingBox bb)
        {
            Add(bb.Min);
            Add(bb.Max);
        }

        public bool Contains(Point p)
        {
            return Min.x <= p.x && Max.x >= p.x &&
                   Min.y <= p.y && Max.y >= p.y &&
                   Min.z <= p.z && Max.z >= p.z;
        }

        public bool Contains(BoundingBox bb)
        {
            return Contains(bb.Min) &&
                   Contains(bb.Max);
        }

        public BoundingBox Transform(Matrix matrix)
        {
            var box = new BoundingBox();
            var p1 = Min;
            var p2 = new Point(Min.x, Min.y, Max.z);
            var p3 = new Point(Min.x, Max.y, Min.z);
            var p4 = new Point(Min.x, Max.y, Max.z);
            var p5 = new Point(Max.x, Min.y, Min.z);
            var p6 = new Point(Max.x, Min.y, Max.z);
            var p7 = new Point(Max.x, Max.y, Min.z);
            var p8 = Max;

            box.Add(matrix * p1);
            box.Add(matrix * p2);
            box.Add(matrix * p3);
            box.Add(matrix * p4);
            box.Add(matrix * p5);
            box.Add(matrix * p6);
            box.Add(matrix * p7);
            box.Add(matrix * p8);

            return box;
        }

        public bool Intersects(Ray ray)
        {
            CheckAxis(ray.Origin.x, ray.Direction.x, Min.x, Max.x, out double xtmin, out double xtmax);
            CheckAxis(ray.Origin.y, ray.Direction.y, Min.y, Max.y, out double ytmin, out double ytmax);

            if ((xtmin > ytmax) || (ytmin > xtmax))
                return false;

            CheckAxis(ray.Origin.z, ray.Direction.z, Min.z, Max.z, out double ztmin, out double ztmax);

            var tmin = Math.Max(xtmin, Math.Max(ytmin, ztmin));
            var tmax = Math.Min(xtmax, Math.Min(ytmax, ztmax));

            if (tmin <= tmax)
            {
                return true;
            }

            return false;
        }

        public void SplitBounds(out BoundingBox left, out BoundingBox right)
        {
            var dx = Max.x - Min.x;
            var dy = Max.y - Min.y;
            var dz = Max.z - Min.z;

            var greatest = Math.Max(dx, Math.Max(dy, dz));

            var x0 = Min.x;
            var y0 = Min.y;
            var z0 = Min.z;
            var x1 = Max.x;
            var y1 = Max.y;
            var z1 = Max.z;

            if (greatest.IsEqual(dx))
            {
                x0 = x1 = x0 + dx / 2.0;
            }
            else if (greatest.IsEqual(dy))
            {
                y0 = y1 = y0 + dy / 2.0;
            }
            else if (greatest.IsEqual(dz))
            {
                z0 = z1 = z0 + dz / 2.0;
            }

            var midMin = new Point(x0, y0, z0);
            var midMax = new Point(x1, y1, z1);

            left = new BoundingBox(Min, midMax);
            right = new BoundingBox(midMin, Max);
        }

        public override string ToString()
        {
            return $"Min: {Min} Max: {Max}";
        }

        private void CheckAxis(double origin, double direction, double min, double max, out double tmin, out double tmax)
        {
            var tmin_numerator = (min - origin);
            var tmax_numerator = (max - origin);

            tmin = tmin_numerator / direction;
            tmax = tmax_numerator / direction;

            if (tmin > tmax)
            {
                var temp = tmin;
                tmin = tmax;
                tmax = temp;
            }
        }
    }
}
