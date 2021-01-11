using CSharp;
using System;

namespace TextureMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a texture mapping scene!");

            var sphere = new Sphere();
            sphere.Transform = Matrix.Translation(-1.5, 1, 0);
            var uvmap = new TextureMap(new CheckersPattern(20, 10, Color.White, new Color(0, 0.5, 0)), Mapping.Spherical);
            sphere.Material.Pattern = uvmap;
            sphere.Material.Ambient = 0.1;
            sphere.Material.Specular = 0.4;
            sphere.Material.Shininess = 10;
            sphere.Material.Diffuse = 0.6;

            var plane = new Plane();
            uvmap = new TextureMap(new UVAlignCheck(Color.White, Color.Red, Color.Yellow, Color.Green, Color.Cyan), Mapping.Planar);
            plane.Material.Pattern = uvmap;
            plane.Material.Ambient = 0.1;
            plane.Material.Diffuse = 0.8;

            var cylinder = new Cylinder();
            cylinder.Minimum = 0.0;
            cylinder.Maximum = 1.0;
            cylinder.Transform = Matrix.Translation(1.5, 0, 0);
            uvmap = new TextureMap(new CheckersPattern(16, 8, Color.White, new Color(0.5, 0, 0)), Mapping.Cylindrical);
            cylinder.Material.Pattern = uvmap;
            cylinder.Material.Ambient = 0.1;
            cylinder.Material.Specular = 0.6;
            cylinder.Material.Shininess = 15;
            cylinder.Material.Diffuse = 0.8;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-10, 10, -10), Color.White));
            world.Objects.Add(sphere);
            world.Objects.Add(plane);
            world.Objects.Add(cylinder);

            var camera = new Camera(400, 400, 0.5);
            camera.Transform = Matrix.ViewTransform(new Point(1, 2, -10),
                                                    Point.Zero,
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("uvmapping.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
