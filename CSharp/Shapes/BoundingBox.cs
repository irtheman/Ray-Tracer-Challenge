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
            // Min
            if (p.x < Min.x)
            {
                Min.SetX(p.x);
            }
            if (p.y < Min.y)
            {
                Min.SetY(p.y);
            }
            if (p.z < Min.z)
            {
                Min.SetZ(p.z);
            }

            // Max
            if (p.x > Max.x)
            {
                Max.SetX(p.x);
            }
            if (p.y > Max.y)
            {
                Max.SetY(p.y);
            }
            if (p.z > Max.z)
            {
                Max.SetZ(p.z);
            }
        }

        public void Add(BoundingBox bb)
        {
            Add(bb.Min);
            Add(bb.Max);
        }

        public bool Contains(Point p)
        {
            return Min.x <= p.x && p.x <= Max.x &&
                   Min.y <= p.y && p.y <= Max.y &&
                   Min.z <= p.z && p.z <= Max.z;
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
            CheckAxis(ray.Origin.z, ray.Direction.z, Min.z, Max.z, out double ztmin, out double ztmax);

            var tmin = Math.Max(xtmin, Math.Max(ytmin, ztmin));
            var tmax = Math.Min(xtmax, Math.Min(ytmax, ztmax));

            if (tmin > tmax)
            {
                return false;
            }

            return true;
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
            else
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
            return $"BB Min: {Min} Max: {Max}";
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
