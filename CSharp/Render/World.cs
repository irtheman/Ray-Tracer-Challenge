using System;
using System.Collections.Generic;

namespace CSharp
{
    public class World
    {
        public World()
        {
            Objects = new List<RTObject>();
            Lights = new List<ILight>();
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
        public List<ILight> Lights { get; set; }

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

        public Color ShadeHit(RTObject obj, Computations comps, int remaining = 5)
        {
            var surface = Color.Black;
            double intensity = 0.0;
            int count = 0;

            foreach (var light in Lights)
            {
                count++;
                intensity = light.IntensityAt(obj, comps, this);

                surface = surface +
                          comps.Object.Material.Lighting(
                                                          obj,
                                                          light,
                                                          comps.OverPoint,
                                                          comps.EyeVector,
                                                          comps.NormalVector,
                                                          intensity);
            }

            if (count > 0)
            {
                surface = surface / count;
            }

            var reflected = ReflectedColor(comps, remaining);
            var refracted = RefractedColor(comps, remaining);

            var material = comps.Object.Material;
            if ((material.Reflective > 0) && (material.Transparency > 0))
            {
                var reflectance = comps.Schlick;
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }

            return surface + reflected + refracted;
        }

        public Color ColorAt(Ray r, int remaining = 5)
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
                var comps = hit.PrepareComputations(r, i);
                result = ShadeHit(hit.Object, comps, remaining);
            }

            return result;
        }

        public bool IsShadowed(RTObject obj, Computations comps, Point lightPosition, Point point)
        {
            bool isShadowed = false;

            var v = new Vector(lightPosition - point);
            var distance = v.Magnitude;
            var direction = v.Normalize;

            var r = new Ray(point, direction);
            var intersections = Intersect(r);

            var h = intersections.Hit;
            if ((h != null) && (h.t < distance) && (h.t > MathHelper.Epsilon))
            {
                var dN = obj is Triangle ? direction.Dot(comps.NormalVector) : -1;

                if (!obj.Equals(h.Object) && h.Object.HasShadow && (dN <= 0))
                {
                    isShadowed = true;
                }
            }

            return isShadowed;
        }

        // Keeping for the unit test
        public bool IsShadowed(Point lightPosition, Point point)
        {
            bool isShadowed = false;

            var v = new Vector(lightPosition - point);
            var distance = v.Magnitude;
            var direction = v.Normalize;

            var r = new Ray(point, direction);
            var intersections = Intersect(r);

            var h = intersections.Hit;
            if ((h != null) && (h.t < distance) && (h.Object.HasShadow))
            {
                isShadowed = true;
            }

            return isShadowed;
        }

        public Color ReflectedColor(Computations comps, int remaining = 5)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (comps.Object.Material.Reflective.IsEqual(0.0))
            {
                return Color.Black;
            }

            var reflectRay = new Ray(comps.OverPoint, comps.ReflectVector);
            var color = ColorAt(reflectRay, remaining - 1);

            return color * comps.Object.Material.Reflective;
        }

        public Color RefractedColor(Computations comps, int remaining)
        {
            if ((remaining <= 0) || comps.Object.Material.Transparency.IsEqual(0.0))
            {
                return Color.Black;
            }

            var nRatio = comps.N1 / comps.N2;
            var cosI = comps.EyeVector.Dot(comps.NormalVector);
            var sin2T = nRatio * nRatio * (1 - cosI * cosI);
            if (sin2T > 1.0)
            {
                return Color.Black;
            }

            var cosT = Math.Sqrt(1.0 - sin2T);
            var direction = new Vector(comps.NormalVector * (nRatio * cosI - cosT) - comps.EyeVector * nRatio);
            var refractRay = new Ray(comps.UnderPoint, direction);
            var color = ColorAt(refractRay, remaining - 1) * comps.Object.Material.Transparency;

            return color;
        }
    }
}
