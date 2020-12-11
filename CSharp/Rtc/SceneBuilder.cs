using CSharp;
using System;
using System.Collections.Generic;

namespace rtc
{
    internal class SceneBuilder
    {
        private Values yaml;
        private Camera camera;
        private World world;

        public SceneBuilder(Values root)
        {
            this.yaml = root;
            world = new World();
        }

        public bool Build()
        {
            if (!yaml.HasList)
            {
                return false;
            }

            foreach (var item in yaml)
            {
                if (item.Key.IsEqual("add"))
                {
                    BuildAdd(item);
                }
            }

            return true;
        }

        public bool Render(string output)
        {
            try
            {
                var c = camera.Render(world);
                System.IO.File.WriteAllText(output, c.GetPPM());
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void BuildAdd(Values item)
        {
            switch(item.String.ToLower())
            {
                case "camera":
                    BuildAddCamera(item);
                    break;

                case "light":
                    BuildAddLight(item);
                    break;

                case "sphere":
                    BuildAddSphere(item);
                    break;
            }
        }

        private void BuildAddSphere(Values item)
        {
            var sphere = new Sphere();
            BuildShape(sphere, item);
            world.Objects.Add(sphere);
        }

        private void BuildShape(RTObject shape, Values item)
        {
            var material = item["material"];
            var transform = item["transform"];

            if (!transform.IsEmpty)
            {
                shape.Transform = BuildTransform(transform);
            }

            if (!material.IsEmpty)
            {
                shape.Material = BuildMaterial(material);
            }
        }

        private void BuildAddLight(Values item)
        {
            var at = ConvertToPoint(item["at"]);
            var intensity = ConvertToColor(item["intensity"]);

            var light = new PointLight(at, intensity);
            world.Lights.Add(light);
        }

        private void BuildAddCamera(Values item)
        {
            var width = item["width"].ToInt();
            var height = item["height"].ToInt();
            var fov = item["field-of-view"].ToDouble();
            var from = ConvertToPoint(item["from"]);
            var to = ConvertToPoint(item["to"]);
            var up = ConvertToVector(item["up"]);

            camera = new Camera(width, height, fov);
            camera.Transform = Matrix.ViewTransform(from, to, up);
        }

        private Matrix BuildTransform(Values transform)
        {
            var stack = new Stack<Matrix>();

            foreach (var item in transform)
            {
                stack.Push(ConvertToTransform(item));
            }

            Matrix results = stack.Pop();
            while (stack.Count > 0)
            {
                results = results * stack.Pop();
            }

            return results;
        }

        private Material BuildMaterial(Values material)
        {
            var m = new Material();

            foreach (var item in material)
            {
                switch (item.Key.ToLower())
                {
                    case "ambient":
                        m.Ambient = item.ToDouble();
                        break;

                    case "color":
                        m.Color = ConvertToColor(item);
                        break;

                    case "diffuse":
                        m.Diffuse = item.ToDouble();
                        break;

                    case "pattern":
                        m.Pattern = BuildPattern(item);
                        break;

                    case "reflective":
                        m.Reflective = item.ToDouble();
                        break;

                    case "refractiveIndex":
                        m.RefractiveIndex = item.ToDouble();
                        break;

                    case "shininess":
                        m.Shininess = item.ToDouble();
                        break;

                    case "specular":
                        m.Specular = item.ToDouble();
                        break;

                    case "transparency":
                        m.Transparency = item.ToDouble();
                        break;
                }
            }

            return m;
        }

        private RTPattern BuildPattern(Values item)
        {
            RTPattern p = null;
            var type = item["type"];
            var colors = item["colors"];
            var transform = item["transform"];

            var a = ConvertToColor(colors[0]);
            var b = ConvertToColor(colors[1]);

            switch (type.String.ToLower())
            {
                case "stripes":
                    p = new StripePattern(a, b);
                    break;

                //BlendedPattern
                //CheckersPattern
                //GradientPattern
                //NestedPattern
                //PerlinNoisePattern
                //RadialGradientPattern
                //RingPattern
                //SolidColorPattern
            }

            if (!transform.IsEmpty)
            {
                p.Transform = BuildTransform(transform);
            }

            return p;
        }

        private Matrix ConvertToTransform(Values item)
        {
            Matrix result = Matrix.Identity;
            double x, y, z;

            switch (item[0].String.ToLower())
            {
                case "translate":
                    x = item[1].ToDouble();
                    y = item[2].ToDouble();
                    z = item[3].ToDouble();
                    result = Matrix.Translation(x, y, z);
                    break;

                case "scale":
                    x = item[1].ToDouble();
                    y = item[2].ToDouble();
                    z = item[3].ToDouble();
                    result = Matrix.Scaling(x, y, z);
                    break;

                case "shear":
                    x = item[1].ToDouble();
                    y = item[2].ToDouble();
                    z = item[3].ToDouble();
                    var a = item[4].ToDouble();
                    var b = item[5].ToDouble();
                    var c = item[6].ToDouble();

                    result = Matrix.Shearing(x, y, z, a, b, c);
                    break;

                case "rotate-x":
                    x = item[1].ToDouble();
                    result = Matrix.RotationX(x);
                    break;

                case "rotate-y":
                    y = item[1].ToDouble();
                    result = Matrix.RotationY(y);
                    break;

                case "rotate-z":
                    z = item[1].ToDouble();
                    result = Matrix.RotationZ(z);
                    break;
            }

            return result;
        }

        private Vector ConvertToVector(Values values)
        {
            var x = values[0].ToDouble();
            var y = values[1].ToDouble();
            var z = values[2].ToDouble();

            return new Vector(x, y, z);
        }

        private Point ConvertToPoint(Values values)
        {
            var x = values[0].ToDouble();
            var y = values[1].ToDouble();
            var z = values[2].ToDouble();

            return new Point(x, y, z);
        }

        private Color ConvertToColor(Values values)
        {
            var x = values[0].ToDouble();
            var y = values[1].ToDouble();
            var z = values[2].ToDouble();

            return new Color(x, y, z);
        }
    }
}