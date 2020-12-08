using CSharp;
using System;

namespace ObjFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating an ObjFile Rendering!");

            var parser = new ObjFileParser(@"..\..\..\..\..\..\Scenes\bunny.obj");

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-10, 10, -10), Color.White));
            world.Objects.Add(parser.ObjToGroup);

            var camera = new Camera(100, 50, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1, 6),
                                                    Point.Zero,
                                                    Vector.VectorY);

            Console.WriteLine();
            Console.WriteLine($"Minimum: {parser.Min}");
            Console.WriteLine($"Maximum: {parser.Max}");
            Console.WriteLine($"Center: {parser.Center}");
            Console.WriteLine();

            var c = camera.Render(world);

            System.IO.File.WriteAllText("ObjFile.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
