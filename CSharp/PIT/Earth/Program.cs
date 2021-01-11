using System;
using CSharp;

namespace Earth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a earth scene!");

            var earthPPM = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\Earth.ppm");
            var image = Canvas.SetPPM(earthPPM);
            if (image is null)
            {
                Console.WriteLine("Oh no! Earth image not loaded!");
                return;
            }

            var plane = new Plane();
            plane.Material.Color = Color.White;
            plane.Material.Ambient = 0;
            plane.Material.Diffuse = 0.1;
            plane.Material.Specular = 0;
            plane.Material.Reflective = 0.4;

            var cylinder = new Cylinder(0, 0.1, true);
            cylinder.Material.Color = Color.White;
            cylinder.Material.Diffuse = 0.2;
            cylinder.Material.Specular = 0;
            cylinder.Material.Ambient = 0;
            cylinder.Material.Reflective = 0.1;

            var sphere = new Sphere();
            sphere.Transform = Matrix.Translation(0, 1.1, 0) *
                               Matrix.RotationY(1.9);
            sphere.Material.Pattern = new TextureMap(new UVImage(image), Mapping.Spherical);
            sphere.Material.Diffuse = 0.9;
            sphere.Material.Specular = 0.1;
            sphere.Material.Shininess = 10;
            sphere.Material.Ambient = 0.1;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-100, 100, -100), Color.White));
            world.Objects.Add(plane);
            world.Objects.Add(cylinder);
            world.Objects.Add(sphere);

            var camera = new Camera(800, 400, 0.8);
            camera.Transform = Matrix.ViewTransform(new Point(1, 2, -10),
                                                    new Point(0, 1.1, 0),
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("earth.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
