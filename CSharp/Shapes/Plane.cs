using System;

namespace CSharp
{
    public class Plane : RTObject
    {
        private BoundingBox _bounds;

        public Plane()
        {
            _bounds = new BoundingBox(
                                        new Point(double.NegativeInfinity, 0, double.NegativeInfinity),
                                        new Point(double.PositiveInfinity, 0, double.PositiveInfinity)
                                      );
        }

        public  override BoundingBox BoundsOf => _bounds;

        protected override Intersections LocalIntersect(Ray ray)
        {
            var i = new Intersections();

            if (Math.Abs(ray.Direction.y) < MathHelper.Epsilon)
            {
                return i;
            }

            var t = -ray.Origin.y / ray.Direction.y;
            i.Add(new Intersection(t, this));

            return i;
        }

        protected override Vector LocalNormal(Point p, Intersection hit)
        {
            return Vector.VectorY;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Plane;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Plane", base.GetHashCode());
        }

        public override string ToString()
        {
            return $"Plane ({ID}): {Material} {Transform}";
        }
    }
}
