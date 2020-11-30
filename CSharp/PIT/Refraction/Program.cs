using CSharp;
using System;

namespace Refraction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a refraction!");

            // Floor of scene
            var wall = new Plane();
            wall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationX(Math.PI / 2);
            wall.Material.Color = new Color(1, 0.9, 0.9);
            wall.Material.Specular = 0;
            wall.Material.Pattern = new CheckersPattern(Color.White, Color.Black);

            // Outer Sphere
            var outer = Sphere.Glass;

            // Inner Sphere
            var inner = Sphere.Glass;
            inner.Material.RefractiveIndex = 1.00029;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-10, 10, -10), Color.White));
            world.Objects.Add(wall);
            world.Objects.Add(outer);
            world.Objects.Add(inner);

            var camera = new Camera(100, 50, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 0, -5),
                                                    Point.Zero,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("refraction.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
