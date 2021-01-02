using CSharp;
using System;

namespace ObjFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // True puts object on floor; False centers camera on object without floor.
            bool withFloor = true;

            Console.WriteLine("Generating an ObjFile Rendering!");

            var dt = DateTime.Now;
            Console.WriteLine($"Starting: {dt}");

            // Floor
            var floor = new Plane();
            floor.Material.Color = Color.Red;
            floor.Material.Diffuse = 0.1;
            floor.Material.Specular = 0.9;
            floor.Material.Shininess = 300;
            floor.Material.Reflective = 0.5;

            var parser = new ObjFileParser(@"..\..\..\..\..\..\ObjFiles\teapot.obj", withFloor);

            var group = parser.ObjToGroup;
            group.Transform = Matrix.Translation(0, withFloor ? parser.Center.y - parser.Min.y : 0, 0)
                              //* Matrix.RotationZ(Math.PI / 2)
                              * Matrix.RotationX(-Math.PI / 2)
                              //* Matrix.RotationY(Math.PI / 2)
                              ;
            //group.Divide(1000);
            
            var time = DateTime.Now - dt;
            Console.WriteLine($"Loading: {time.Hours}:{time.Minutes}:{time.Seconds}");

            var world = new World();
            world.Lights.Add(new PointLight(new Point(0, 0, 10), Color.White));
            world.Objects.Add(group);

            double fov = Math.PI / 3;
            double fromX, fromY, fromZ;
            double toX, toY, toZ;

            if (withFloor)
            {
                world.Objects.Add(floor);

                fromX = 0.0;
                fromY = 0.1;
                fromZ = 10;
                toX = 0.0;
                toY = 0.5;
                toZ = 0.0;
            }
            else
            {
                var height = Math.Max(parser.Max.x - parser.Min.x, Math.Max(parser.Max.y - parser.Min.y, parser.Max.z - parser.Min.z));
                var dist = Math.Abs(height / Math.Sin(fov / 2));
                var center = parser.Center;

                fromX = center.x;
                fromY = center.y;
                fromZ = dist + height;
                toX = center.x;
                toY = center.y;
                toZ = center.z;
            }

            var camera = new Camera(400, 200, fov);
            camera.Transform = Matrix.ViewTransform(new Point(fromX, fromY, fromZ),
                                                    new Point(toX, toY, toZ),
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
