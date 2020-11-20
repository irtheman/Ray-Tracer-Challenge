using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Sphere : RTObject
    {
        public override Intersections Intersect(Ray r)
        {
            var ray = r.Transform(Transform.Inverse);

            var result = new Intersections();
            var sphereToRay = new Vector(ray.Origin - new Point(0, 0, 0));
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

        public override Vector Normal(Point p)
        {
            var inverse = Transform.Inverse;
            var objectPoint = inverse * p;
            var objectNormal = objectPoint - new Point(0, 0, 0);
            var worldNormal = new Vector(inverse.Transpose * objectNormal);

            return worldNormal.Normalize;
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
