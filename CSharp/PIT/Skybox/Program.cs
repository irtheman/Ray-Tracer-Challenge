using System;
using CSharp;

namespace Skybox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a skybox scene!");

            var s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\negx.ppm");
            var left = Canvas.SetPPM(s);

            s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\posx.ppm");
            var right = Canvas.SetPPM(s);

            s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\posz.ppm");
            var front = Canvas.SetPPM(s);

            s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\negz.ppm");
            var back = Canvas.SetPPM(s);

            s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\posy.ppm");
            var up = Canvas.SetPPM(s);

            s = System.IO.File.ReadAllText(@"..\..\..\..\..\..\Scenes\negy.ppm");
            var down = Canvas.SetPPM(s);

            if ((left is null) || (right is null) || (front is null) || (back is null) || (up is null) || (down is null))
            {
                Console.WriteLine("Oh no! Skybox image(s) not loaded!");
                return;
            }

            var negx = new UVImage(left);
            var posx = new UVImage(right);
            var posz = new UVImage(front);
            var negz = new UVImage(back);
            var posy = new UVImage(up);
            var negy = new UVImage(down);

            var sphere = new Sphere();
            sphere.Transform = Matrix.Translation(0, 0, 5) *
                               Matrix.Scaling(0.75, 0.75, 0.75);
            sphere.Material.Color = Color.DarkGrey;
            sphere.Material.Diffuse = 0.4;
            sphere.Material.Specular = 0.9; // 0.6;
            sphere.Material.Shininess = 96; // 20.0
            sphere.Material.Reflective = 1;
            sphere.Material.Ambient = 0;

            var cube = new Cube();
            cube.Transform = Matrix.Scaling(1000, 1000, 1000);
            cube.Material.Pattern = new CubeMapPattern(negx, posz, posx, negz, posy, negy);
            sphere.Material.Diffuse = 0;
            sphere.Material.Specular = 0;
            sphere.Material.Ambient = 1;


            var world = new World();
            world.Lights.Add(new PointLight(new Point(0, 100, 0), Color.White));
            world.Objects.Add(cube);
            world.Objects.Add(sphere);

            var camera = new Camera(400, 200, 1.2);
            camera.Transform = Matrix.ViewTransform(Point.Zero,
                                                    new Point(0, 0, 5),
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("skybox.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
