using System;

namespace CSharp
{
    public class Plane : RTObject
    {
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

        protected override Vector LocalNormal(Point p)
        {
            return Vector.VectorY;
        }
    }
}
