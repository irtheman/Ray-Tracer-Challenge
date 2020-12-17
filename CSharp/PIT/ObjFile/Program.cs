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

            // Floor
            var floor = new Plane();
            floor.Material.Color = Color.Red;
            floor.Material.Diffuse = 0.1;
            floor.Material.Specular = 0.9;
            floor.Material.Shininess = 300;
            floor.Material.Reflective = 0.9;

            var parser = new ObjFileParser(@"..\..\..\..\..\..\ObjFiles\humanoid_quad.obj");

            var group = parser.ObjToGroup;
            group.Transform = Matrix.Translation(0, -parser.Min.y, 0)
                              * Matrix.RotationZ(Math.PI / 2)
                              * Matrix.RotationY(Math.PI / 2);
            group.Divide(10);
            
            var time = DateTime.Now - dt;
            Console.WriteLine($"Loading: {time.Hours}:{time.Minutes}:{time.Seconds}");

            var world = new World();
            world.Lights.Add(new PointLight(new Point(10, 10, 10), Color.White));
            world.Objects.Add(group);
            world.Objects.Add(floor);

            var camera = new Camera(100, 50, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1, 5),
                                                    new Point(0, 0.5, 0),
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
