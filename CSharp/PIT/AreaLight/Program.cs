using System;
using CSharp;

namespace AreaLight
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a scene with an area light!");

            // Put the light in the middle of a "shining" cube so that it can show
            // up in reflections as a physical thing. Naturally, the cube must
            // opt out of shadow tests...
            var cube = new Cube();
            cube.HasShadow = false;
            cube.Transform = Matrix.Translation(0, 3, 4) *
                             Matrix.Scaling(1, 1, 0.01);
            cube.Material.Color = new Color(1.5, 1.5, 1.5);
            cube.Material.Ambient = 1.0;
            cube.Material.Diffuse = 0.0;
            cube.Material.Specular = 0.0;

            var plane = new Plane();
            plane.Material.Color = Color.White;
            plane.Material.Ambient = 0.025;
            plane.Material.Diffuse = 0.67;
            plane.Material.Specular = 0;

            var sphere1 = new Sphere();
            sphere1.Transform = Matrix.Translation(0.5, 0.5, 0) *
                                Matrix.Scaling(0.5, 0.5, 0.5);
            sphere1.Material.Color = Color.Red;
            sphere1.Material.Ambient = 0.1;
            sphere1.Material.Specular = 0.0;
            sphere1.Material.Diffuse = 0.6;
            sphere1.Material.Reflective = 0.3;

            var sphere2 = new Sphere();
            sphere2.Transform = Matrix.Translation(-0.25, 0.33, 0) *
                                Matrix.Scaling(0.33, 0.33, 0.33);
            sphere2.Material.Color = new Color(0.5, 0.5, 1);
            sphere2.Material.Ambient = 0.1;
            sphere2.Material.Specular = 0.0;
            sphere2.Material.Diffuse = 0.6;
            sphere2.Material.Reflective = 0.3;

            var world = new World();
            world.Lights.Add(new CSharp.AreaLight(new Point(-1, 2, 4), new Vector(2, 0, 0), 10, new Vector(0, 2, 0), 10, new Color(1.5, 1.5, 1.5)));
            world.Objects.Add(cube);
            world.Objects.Add(plane);
            world.Objects.Add(sphere1);
            world.Objects.Add(sphere2);

            var camera = new Camera(400, 160, 0.7854);
            camera.Transform = Matrix.ViewTransform(new Point(-3, 1, 2.5),
                                                    new Point(0, 0.5, 0),
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("arealight.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
