using CSharp;
using System;

namespace ObjFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating an ObjFile Rendering!");

            var dt = DateTime.Now;
            Console.WriteLine($"Starting: {dt}");

            var parser = new ObjFileParser(@"..\..\..\..\..\..\Scenes\bunny.obj");
            var group = parser.ObjToGroup;
            group.Transform = Matrix.Translation(0, -1, 0);
            group.Divide(5);

            var time = DateTime.Now - dt;
            Console.WriteLine($"Loading: {time.Hours}:{time.Minutes}:{time.Seconds}");

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

            dt = DateTime.Now;
            var c = camera.Render(world);

            time = DateTime.Now - dt;
            Console.WriteLine($"Rendering: {time.Hours}:{time.Minutes}:{time.Seconds}");
            Console.WriteLine($"Finished: {DateTime.Now}");

            System.IO.File.WriteAllText("ObjFile.ppm", c.GetPPM());

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
