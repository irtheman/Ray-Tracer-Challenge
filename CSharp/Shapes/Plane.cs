using System;

namespace CSharp
{
    public class Plane : RTObject
    {
        private readonly BoundingBox bounds;

        public Plane() : base()
        {
            bounds = new BoundingBox(
                                     new Point(double.NegativeInfinity, 0, double.NegativeInfinity),
                                     new Point(double.PositiveInfinity, 0, double.PositiveInfinity)
                                    );
        }

        public override BoundingBox Bounds => bounds;

        protected override Intersections LocalIntersect(Ray ray)
        {
            var i = new Intersections();

            if (Bounds.Intersects(ray))
            {
                if (Math.Abs(ray.Direction.y) < MathHelper.Epsilon)
                {
                    return i;
                }

                var t = -ray.Origin.y / ray.Direction.y;
                i.Add(new Intersection(t, this));
            }

            return i;
        }

        protected override Vector LocalNormalAt(Point p, Intersection i)
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
            return HashCode.Combine("Plane", Material, Transform);
        }

        public override string ToString()
        {
            return $"Plane(0, 0, 0): {Material} {Transform}";
        }
    }
}
