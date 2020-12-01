using CSharp;
using System;

namespace Refraction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a refraction!");

            // Wall of scene
            var wall = new Plane();
            wall.Transform = Matrix.Translation(0, 0, 10) *
                             Matrix.RotationX(1.5708);
            wall.Material.Specular = 0;
            wall.Material.Diffuse = 0.2;
            wall.Material.Ambient = 0.8;
            wall.Material.Pattern = new CheckersPattern(new Color(0.85, 0.85, 0.85), new Color(0.15, 0.15, 0.15));

            // Glass Sphere
            var outer = new Sphere();
            outer.Material.Color = Color.White;
            outer.Material.Ambient = 0;
            outer.Material.Diffuse = 0;
            outer.Material.Specular = 0.9;
            outer.Material.Shininess = 300;
            outer.Material.Reflective = 0.9;
            outer.Material.Transparency = 0.9;
            outer.Material.RefractiveIndex = 1.5;

            // Hollow center
            var inner = new Sphere();
            inner.Transform = Matrix.Scaling(0.5, 0.5, 0.5);
            inner.Material.Color = Color.White;
            inner.Material.Ambient = 0;
            inner.Material.Diffuse = 0;
            inner.Material.Specular = 0.9;
            inner.Material.Shininess = 300;
            inner.Material.Reflective = 0.9;
            inner.Material.Transparency = 0.9;
            inner.Material.RefractiveIndex = 1.0000034;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(2, 10, -5), new Color(0.9, 0.9, 0.9)));
            world.Objects.Add(wall);
            world.Objects.Add(outer);
            world.Objects.Add(inner);

            var camera = new Camera(300, 300, 0.45);
            camera.Transform = Matrix.ViewTransform(new Point(0, 0, -5),
                                                    Point.Zero,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("refraction.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
