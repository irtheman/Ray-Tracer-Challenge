using System;
using CSharp;

namespace Csg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a CSG!");

            var cube = new Cube();
            cube.Material.Color = Color.Yellow;

            var sphere = new Sphere();
            sphere.Transform = Matrix.Translation(-0.5, 0.5, 0.5);
            sphere.Material.Color = Color.Red;

            var csg = new CSG(CsgOperation.Difference, cube, sphere);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(10, 10, 10), Color.White));
            world.Objects.Add(csg);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(-1.5, 2, 5),
                                                    Point.Zero,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("Csg.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
