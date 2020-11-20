using System;
using System.Collections.Generic;

namespace CSharp
{
    public class World
    {
        public World(bool isTest = false)
        {
            Objects = new List<RTObject>();
            Lights = new List<PointLight>();

            if (isTest)
            {
                var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));

                var s1 = new Sphere();
                s1.Material.Color = new Color(0.8, 1, 0.6);
                s1.Material.Diffuse = 0.7;
                s1.Material.Specular = 0.2;

                var s2 = new Sphere();
                s2.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

                Lights.Add(light);
                Objects.Add(s1);
                Objects.Add(s2);
            }
        }

        public List<RTObject> Objects { get; set; }
        public List<PointLight> Lights { get; set; }

        public Intersections Intersect(Ray ray)
        {
            Intersections xs = new Intersections();

            foreach (var obj in Objects)
            {
                var ret = obj.Intersect(ray);
                xs.Add(ret);
            }

            return xs;
        }

        public Color ShadeHit(Computations comps)
        {
            var totalColor = new Color(0, 0, 0);
            var shadowed = IsShadowed(comps.OverPoint);

            totalColor = totalColor +
                            comps.Object.Material.Lighting(Lights[0],
                                                        comps.Point,
                                                        comps.EyeVector,
                                                        comps.NormalVector,
                                                        shadowed);

            return totalColor;
        }

        public Color ColorAt(Ray r)
        {
            var i = Intersect(r);
            var hit = i.Hit;
            Color result;

            if (hit == null)
            {
                result = new Color(0, 0, 0);
            }
            else
            {
                var comps = hit.PrepareComputations(r);
                result = ShadeHit(comps);
            }

            return result;
        }

        public bool IsShadowed(Point point)
        {
            var v = new Vector(Lights[0].Position - point);
            var distance = v.Magnitude;
            var direction = v.Normalize;

            var r = new Ray(point, direction);
            var intersections = Intersect(r);

            var h = intersections.Hit;
            return ((h != null) && (h.t < distance));
        }
    }
}
