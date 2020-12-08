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
            var group = parser.ObjToGroup;
            group.Transform = Matrix.Translation(0, -1, 0);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(10, 10, 10), Color.White));
            world.Objects.Add(group);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0.5, 2, 6),
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
