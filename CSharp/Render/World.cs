﻿using System;
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

        public Color ShadeHit(RTObject obj, Computations comps, int remaining = 5)
        {
            var surface = Color.Black;
            var shadowed = IsShadowed(comps.OverPoint);

            foreach (var light in Lights)
            {
                surface = surface +
                          comps.Object.Material.Lighting(
                                                         obj,
                                                         light,
                                                         comps.OverPoint,
                                                         comps.EyeVector,
                                                         comps.NormalVector,
                                                         shadowed);
            }

            var reflected = ReflectedColor(comps, remaining);
            var refracted = RefractedColor(comps, remaining);

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