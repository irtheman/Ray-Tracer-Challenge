using CSharp;
using System;

namespace CSharpTest
{
    public class TestShape : RTObject
    {
        public Ray SavedRay { get; set; }

        public override BoundingBox Bounds => new BoundingBox(new Point(-1, -1, -1), new Point(1, 1, 1));

        protected override Intersections LocalIntersect(Ray ray)
        {
            var result = new Intersections();

            if (Bounds.Intersects(ray))
            {
                SavedRay = ray;

                var sphereToRay = new Vector(ray.Origin - Point.Zero);
                var a = ray.Direction.Dot(ray.Direction);
                var b = 2.0 * ray.Direction.Dot(sphereToRay);
                var c = sphereToRay.Dot(sphereToRay) - 1;
                var discriminant = b * b - 4 * a * c;

                if (discriminant < 0.0)
                {
                    return result;
                }
                else
                {
                    result.Add(new Intersection((-b - Math.Sqrt(discriminant)) / (2 * a), this));
                    result.Add(new Intersection((-b + Math.Sqrt(discriminant)) / (2 * a), this));
                }
            }

            return result;
        }

        protected override Vector LocalNormal(Point p)
        {
            return new Vector(p.x, p.y, p.z);
        }
    }
}
