using CSharp;
using System;

namespace UnderWater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating an under water scene!");

            // Floor of scene
            var floor = new Plane();
            floor.Material.Pattern = new CheckersPattern(new Color (0, 0.4, 0.4), new Color(0, 0.8, 0.8));
            floor.Material.Pattern.Transform = Matrix.Scaling(0.2, 0.2, 0.2);
            floor.Material.Color = Color.Blue;
            floor.Material.Specular = 0;

            // Water of scene
            var water = new Plane();
            water.HasShadow = false;
            water.Transform = Matrix.Translation(0, 2, 0);
            water.Material.Color = new Color(0, .25, 0.5);
            water.Material.Ambient = 0.5;
            water.Material.Diffuse = 0.1;
            water.Material.Specular = 1;
            water.Material.Shininess = 300;
            water.Material.Reflective = 1;
            water.Material.Transparency = 0.3;
            water.Material.RefractiveIndex = 1.333;

            // Middle Sphere
            var middle = new Sphere();
            middle.Transform = Matrix.Translation(-0.5, 1, 0.5);
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            middle.Material.Reflective = 1;

            // Right Sphere
            var right = new Sphere();
            right.Transform = Matrix.Translation(1.5, 0.5, -0.5) *
                              Matrix.Scaling(0.5, 0.5, 0.5);
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;

            // Left Sphere
            var left = new Sphere();
            left.Transform = Matrix.Translation(-1.5, 0.33, -0.75) *
                              Matrix.Scaling(0.33, 0.33, 0.33);
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-10, 4.9, -10), Color.White));
            world.Objects.Add(floor);
            world.Objects.Add(water);
            world.Objects.Add(middle);
            world.Objects.Add(right);
            world.Objects.Add(left);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 0.5, 10),
                                                    Point.PointY,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("underwater.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
