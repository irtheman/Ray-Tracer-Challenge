using System;
using System.Collections.Generic;

namespace CSharp
{
    public class World
    {
        public World()
        {
            Objects = new List<RTObject>();
            Lights = new List<PointLight>();
        }

        public static World Default
        {
            get
            {
                var w = new World();

                var light = new PointLight(new Point(-10, 10, -10), Color.White);

                var s1 = new Sphere();
                s1.Material.Color = new Color(0.8, 1, 0.6);
                s1.Material.Diffuse = 0.7;
                s1.Material.Specular = 0.2;

                var s2 = new Sphere();
                s2.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

                w.Lights.Add(light);
                w.Objects.Add(s1);
                w.Objects.Add(s2);

                return w;
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

        public Color ShadeHit(RTObject obj, Computations comps)
        {
            var totalColor = Color.Black;
            var shadowed = IsShadowed(comps.OverPoint);

            foreach (var light in Lights)
            {
                totalColor = totalColor +
                             comps.Object.Material.Lighting(
                                                            obj,
                                                            light,
                                                            comps.OverPoint,
                                                            comps.EyeVector,
                                                            comps.NormalVector,
                                                            shadowed);
            }

            return totalColor;
        }

        public Color ColorAt(Ray r)
        {
            var i = Intersect(r);
            var hit = i.Hit;
            Color result;

            if (hit == null)
            {
                result = Color.Black;
            }
            else
            {
                var comps = hit.PrepareComputations(r);
                result = ShadeHit(hit.Object, comps);
            }

            return result;
        }

        public bool IsShadowed(Point point)
        {
            bool isShadowed = false;

            foreach (var light in Lights)
            {
                var v = new Vector(light.Position - point);
                var distance = v.Magnitude;
                var direction = v.Normalize;

                var r = new Ray(point, direction);
                var intersections = Intersect(r);

                var h = intersections.Hit;
                if ((h != null) && (h.t < distance))
                {
                    isShadowed = true;
                    break;
                }
            }

            return isShadowed;
        }
    }
}
