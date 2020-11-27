using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Sphere : RTObject
    {
        protected override Intersections LocalIntersect(Ray ray)
        {
            var result = new Intersections();
            var sphereToRay = new Vector(ray.Origin - Point.Zero);
            var a = ray.Direction.Dot(ray.Direction);
            var b = 2.0 * ray.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return result;
            }
            else
            {
                result.Add(new Intersection( (-b - Math.Sqrt(discriminant)) / (2 * a), this));
                result.Add(new Intersection((-b + Math.Sqrt(discriminant)) / (2 * a), this));
            }

            return result;
        }

        protected override Vector LocalNormal(Point p)
        {
            return new Vector(p.x, p.y, p.z);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Sphere;
            return ((other != null) &&
                    (Material.Equals(other.Material)) &&
                    (Transform.Equals(other.Transform)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Material, Transform);
        }

        public override string ToString()
        {
            return $"Sphere(0, 0): {Material} {Transform}";
        }
    }
}
