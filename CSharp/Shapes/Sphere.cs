using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Sphere : RTObject
    {
        private readonly BoundingBox bounds;

        public Sphere() : base()
        {
            bounds = new BoundingBox(new Point(-1, -1, -1), new Point(1, 1, 1));
        }

        public static Sphere Glass
        {
            get
            {
                var sphere = new Sphere();
                sphere.Material.Transparency = 1.0;
                sphere.Material.RefractiveIndex = 1.5;

                return sphere;
            }
        }

        public override BoundingBox Bounds => bounds;

        protected override Intersections LocalIntersect(Ray ray)
        {
            var result = new Intersections();

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

            return result;
        }

        protected override Vector LocalNormalAt(Point p, Intersection i)
        {
            var point = p - Point.Zero;
            return new Vector(point.x, point.y, point.z);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Sphere;
            return (other != null) && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Sphere", Material, Transform);
        }

        public override string ToString()
        {
            return $"Sphere(0, 0): {Material} {Transform}";
        }
    }
}
