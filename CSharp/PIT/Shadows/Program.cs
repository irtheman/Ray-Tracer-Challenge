using CSharp;
using System;

namespace Shadows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a shadow scene!");

            // Wall of scene
            var wall = new Sphere();
            wall.Transform = Matrix.Translation(0, 0, 20) *
                             Matrix.RotationX(Math.PI / 2) *
                             Matrix.Scaling(20, 0.01, 20);
            wall.Material.Color = new Color(1, 0.9, 0.9);
            wall.Material.Specular = 0;

            // Middle Sphere
            var middle = new Sphere();
            middle.Transform = Matrix.Translation(-2, 0, 0);
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;

            // Nose Sphere
            var nose = new Sphere();
            nose.Transform = Matrix.Translation(-1.5, 0, 0) *
                             Matrix.Scaling(1, 0.7, 1);
            nose.Material.Color = new Color(1, 0.1, 0.5);
            nose.Material.Diffuse = 0.7;
            nose.Material.Specular = 0.3;

            // Ear 1 Sphere
            var ear1 = new Sphere();
            ear1.Transform = Matrix.Translation(-2.5, 1, 0) *
                             Matrix.Scaling(0.08, 1, 1);
            ear1.Material.Color = new Color(1, 0.1, 0.5);
            ear1.Material.Diffuse = 0.7;
            ear1.Material.Specular = 0.3;

            // Ear 2 Sphere
            var ear2 = new Sphere();
            ear2.Transform = Matrix.Translation(-2, 1, 0) *
                             Matrix.Shearing(1, 0, 0, 0, 0, 0) *
                             Matrix.Scaling(0.08, 1, 1);
            ear2.Material.Color = new Color(1, 0.1, 0.5);
            ear2.Material.Diffuse = 0.7;
            ear2.Material.Specular = 0.3;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-5, 0, -10), Color.White));
            world.Objects.Add(wall);
            world.Objects.Add(middle);
            world.Objects.Add(nose);
            world.Objects.Add(ear1);
            world.Objects.Add(ear2);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1, -5),
                                                    Point.PointY,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("shadow.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
