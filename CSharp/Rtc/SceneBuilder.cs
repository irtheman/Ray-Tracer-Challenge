using CSharp;
using System;
using System.Collections.Generic;

namespace rtc
{
    internal class SceneBuilder
    {
        private Dictionary<string, Values> define;
        private Values yaml;
        private Camera camera;
        private World world;
        private bool ObjFile;

        public SceneBuilder(Values root)
        {
            define = new Dictionary<string, Values>();
            this.yaml = root;
            world = new World();
            ObjFile = false;
        }

        public SceneBuilder(string input, int width, int height, string floor, double rX, double rY, double rZ)
        {
            bool withFloor = !string.IsNullOrWhiteSpace(floor);

            world = new World();
            world.Lights.Add(new PointLight(new Point(0, 0, 10), Color.White));

            var parser = new ObjFileParser(input);
            var obj = parser.ObjToGroup;
            obj.Transform = Matrix.Translation(0, withFloor ? parser.Center.y - parser.Min.y : 0, 0)
                            * Matrix.RotationZ(rZ)
                            * Matrix.RotationX(rX)
                            * Matrix.RotationY(rY);
            world.Objects.Add(obj);

            double fov = Math.PI / 3;
            double fromX, fromY, fromZ;
            double toX, toY, toZ;

            if (withFloor)
            {
                var flr = new Plane();
                flr.Material.Color = GetObjColor(floor);
                flr.Material.Diffuse = 0.1;
                flr.Material.Specular = 0.9;
                flr.Material.Shininess = 300;
                flr.Material.Reflective = 0.5;

                world.Objects.Add(flr);

                fromX = 0.0;
                fromY = 0.1;
                fromZ = 10;
                toX = 0.0;
                toY = 0.5;
                toZ = 0.0;
            }
            else
            {
                var hght = Math.Max(parser.Max.x - parser.Min.x, Math.Max(parser.Max.y - parser.Min.y, parser.Max.z - parser.Min.z));
                var dist = Math.Abs(hght / Math.Sin(fov / 2));
                var center = parser.Center;

                fromX = center.x;
                fromY = center.y;
                fromZ = dist + hght;
                toX = center.x;
                toY = center.y;
                toZ = center.z;
            }

            camera = new Camera(width, height, fov);
            camera.Transform = Matrix.ViewTransform(new Point(fromX, fromY, fromZ),
                                                    new Point(toX, toY, toZ),
                                                    Vector.VectorY);

            ObjFile = true;
        }

        private Color GetObjColor(string floor)
        {
            switch (floor.ToLower())
            {
                case "green":
                    return Color.Green;
                case "blue":
                    return Color.Blue;
                case "white":
                    return Color.White;
                case "black":
                    return Color.Black;
                case "brown":
                    return Color.Brown;
                case "cyan":
                    return Color.Cyan;
                case "Grey":
                    return Color.Grey;
                case "purple":
                    return Color.Purple;
                case "yellow":
                    return Color.Yellow;
                default:
                    return Color.Red;
            }
        }

        public bool Build()
        {
            if (ObjFile) return true;

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
                else if (item.Key.IsEqual("define"))
                {
                    BuildDefine(item);
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

        private void BuildDefine(Values item)
        {
            string key = item.String;
            define.Add(key, item);
        }

        private RTObject BuildAdd(Values item, Group group = null)
        {
            RTObject obj = null;

            switch (item.String.ToLower())
            {
                case "camera":
                    BuildAddCamera(item);
                    break;

                case "light":
                    BuildAddLight(item);
                    break;

                case "group":
                    obj = BuildAddGroup(item);
                    break;

                case "sphere":
                    obj = BuildAddSphere(item);
                    break;

                case "plane":
                    obj = BuildAddPlane(item);
                    break;

                case "cube":
                    obj = BuildAddCube(item);
                    break;

                case "obj":
                    obj = BuildAddObj(item);
                    break;
                
                default:
                    Values value = null;
                    if (define.TryGetValue(item.String, out value))
                    {
                        value = value["value"];
                        obj = BuildAdd(value.List[0]);
                        BuildShape(obj, item);
                        obj = null;
                    }
                    break;
            }

            if (obj != null)
            {
                if (group == null)
                    world.Objects.Add(obj);
                else
                    group.Add(obj);
            }

            return obj;
        }

        private RTObject BuildAddGroup(Values item)
        {
            var group = new Group();
            BuildShape(group, item);

            var children = item["children"];

            foreach (var child in children)
            {
                if (item.Key.IsEqual("add"))
                {
                    BuildAdd(child, group);
                }
            }

            return group;
        }

        private RTObject BuildAddSphere(Values item)
        {
            var sphere = new Sphere();
            BuildShape(sphere, item);
            return sphere;
        }

        private RTObject BuildAddPlane(Values item)
        {
            var plane = new Plane();
            BuildShape(plane, item);
            return plane;
        }

        private RTObject BuildAddCube(Values item)
        {
            var cube = new Cube();
            BuildShape(cube, item);
            return cube;
        }

        private RTObject BuildAddObj(Values item)
        {
            var file = item["file"];
            var parser = new ObjFileParser(file.String);
            var obj = parser.ObjToGroup;
            BuildShape(obj, item);
            return obj;
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
                var m = ConvertToTransform(item);
                if (m == null)
                {
                    var key = item.String;
                    var i = define[key];
                    m = BuildTransform(i.List[0]);
                }

                stack.Push(m);
            }

            Matrix results = stack.Pop();
            while (stack.Count > 0)
            {
                results = results * stack.Pop();
            }

            return results;
        }

        private Material BuildMaterial(Values material, Material mat = null)
        {
            Material m = mat;

            if (!string.IsNullOrWhiteSpace(material.String))
            {
                string key = material.String;
                var mt = define[key];

                var extend = mt["extend"];
                if (!extend.IsEmpty)
                {
                    m = BuildMaterial(extend);
                }

                var value = mt["value"];
                if (!value.IsEmpty)
                {
                    m = BuildMaterial(value, m);
                }
            }
            else if (m == null)
            {
                m = new Material();
            }

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

                case "checkers":
                    p = new CheckersPattern(a, b);
                    break;

                //BlendedPattern
                //CheckersPattern
                //GradientPattern
                //NestedPattern
                //PerlinNoisePattern
                //RadialGradientPattern
                //RingPattern
                //SolidColorPattern
                //CubeMapPattern3D
                //CheckersPattern3D
                //UVImage
                //UVAlignCheck
                //TextureMap
            }

            if (!transform.IsEmpty)
            {
                p.Transform = BuildTransform(transform);
            }

            return p;
        }

        private Matrix ConvertToTransform(Values item)
        {
            Matrix result = null;
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