using System;
using CSharp;

namespace Cylinders
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a cylinder scene!");

            // Floor
            var floor = new Plane();
            floor.Material.Pattern = new CheckersPattern(new Color(0.75, 0.75, 0.75), new Color(0.5, 0.5, 0.5));
            floor.Material.Pattern.Transform = Matrix.RotationY(0.3) *
                                               Matrix.Scaling(0.25, 0.25, 0.25);
            floor.Material.Ambient = 0.2;
            floor.Material.Diffuse = 0.9;
            floor.Material.Specular = 0;

            // Cylinder
            var c1 = new Cylinder(0, 0.75, true);
            c1.Transform = Matrix.Translation(-1, 0, 1) *
                           Matrix.Scaling(0.5, 1, 0.5);
            c1.Material.Color = new Color(0, 0, 0.6);
            c1.Material.Diffuse = 0.1;
            c1.Material.Specular = 0.9;
            c1.Material.Shininess = 300;
            c1.Material.Reflective = 0.9;

            // Concentric cylinders
            var c2 = new Cylinder(0, 0.2);
            c2.Transform = Matrix.Translation(1, 0, 0) *
                           Matrix.Scaling(0.8, 1, 0.8);
            c2.Material.Color = new Color(1, 1, 0.3);
            c2.Material.Ambient = 0.1;
            c2.Material.Diffuse = 0.8;
            c2.Material.Specular = 0.9;
            c2.Material.Shininess = 300;

            var c3 = new Cylinder(0, 0.3);
            c3.Transform = Matrix.Translation(1, 0, 0) *
                           Matrix.Scaling(0.6, 1, 0.6);
            c3.Material.Color = new Color(1, 0.9, 0.4);
            c3.Material.Ambient = 0.1;
            c3.Material.Diffuse = 0.8;
            c3.Material.Specular = 0.9;
            c3.Material.Shininess = 300;

            var c4 = new Cylinder(0, 0.4);
            c4.Transform = Matrix.Translation(1, 0, 0) *
                           Matrix.Scaling(0.4, 1, 0.4);
            c4.Material.Color = new Color(1, 0.8, 0.5);
            c4.Material.Ambient = 0.1;
            c4.Material.Diffuse = 0.8;
            c4.Material.Specular = 0.9;
            c4.Material.Shininess = 300;

            var c5 = new Cylinder(0, 0.5, true);
            c5.Transform = Matrix.Translation(1, 0, 0) *
                           Matrix.Scaling(0.2, 1, 0.2);
            c5.Material.Color = new Color(1, 0.7, 0.6);
            c5.Material.Ambient = 0.1;
            c5.Material.Diffuse = 0.8;
            c5.Material.Specular = 0.9;
            c5.Material.Shininess = 300;

            // Decorative cylinders
            var c6 = new Cylinder(0, 0.3, true);
            c6.Transform = Matrix.Translation(0, 0, -0.75) *
                           Matrix.Scaling(0.05, 1, 0.05);
            c6.Material.Color = new Color(1, 0, 0);
            c6.Material.Ambient = 0.1;
            c6.Material.Diffuse = 0.9;
            c6.Material.Specular = 0.9;
            c6.Material.Shininess = 300;

            var c7 = new Cylinder(0, 0.3, true);
            c7.Transform = Matrix.Translation(0, 0, -2.25) *
                           Matrix.RotationY(-0.15) *
                           Matrix.Translation(0, 0, 1.5) *
                           Matrix.Scaling(0.05, 1, 0.05);
            c7.Material.Color = new Color(1, 1, 0);
            c7.Material.Ambient = 0.1;
            c7.Material.Diffuse = 0.9;
            c7.Material.Specular = 0.9;
            c7.Material.Shininess = 300;

            var c8 = new Cylinder(0, 0.3, true);
            c8.Transform = Matrix.Translation(0, 0, -2.25) *
                           Matrix.RotationY(-0.3) *
                           Matrix.Translation(0, 0, 1.5) *
                           Matrix.Scaling(0.05, 1, 0.05);
            c8.Material.Color = new Color(0, 1, 0);
            c8.Material.Ambient = 0.1;
            c8.Material.Diffuse = 0.9;
            c8.Material.Specular = 0.9;
            c8.Material.Shininess = 300;

            var c9 = new Cylinder(0, 0.3, true);
            c9.Transform = Matrix.Translation(0, 0, -2.25) *
                           Matrix.RotationY(-0.45) *
                           Matrix.Translation(0, 0, 1.5) *
                           Matrix.Scaling(0.05, 1, 0.05);
            c9.Material.Color = new Color(0, 1, 1);
            c9.Material.Ambient = 0.1;
            c9.Material.Diffuse = 0.9;
            c9.Material.Specular = 0.9;
            c9.Material.Shininess = 300;

            var glass = new Cylinder(0.0001, 0.5, true);
            glass.Transform = Matrix.Translation(0, 0, -1.5) *
                           Matrix.Scaling(0.33, 1, 0.33);
            glass.Material.Color = new Color(0.25, 0, 0);
            glass.Material.Diffuse = 0.1;
            glass.Material.Specular = 0.9;
            glass.Material.Shininess = 300;
            glass.Material.Reflective = 0.9;
            glass.Material.Transparency = 0.9;
            glass.Material.RefractiveIndex = 1.5;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(1, 6.9, -4.9), new Color(1, 1, 1)));
            world.Objects.Add(floor);
            world.Objects.Add(c1);
            world.Objects.Add(c2);
            world.Objects.Add(c3);
            world.Objects.Add(c4);
            world.Objects.Add(c5);
            world.Objects.Add(c6);
            world.Objects.Add(c7);
            world.Objects.Add(c8);
            world.Objects.Add(c9);
            world.Objects.Add(glass);

            var camera = new Camera(400, 200, 0.314);
            camera.Transform = Matrix.ViewTransform(new Point(8, 3.5, -9),
                                                    new Point(0, 0.3, 0),
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("cylinders.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
