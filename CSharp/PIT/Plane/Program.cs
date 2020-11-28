using CSharp;
using System;

namespace Plane
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a plane!");

            // Floor of scene
            var floor = new CSharp.Plane();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;

            // Ceiling
            //var ceiling = new CSharp.Plane();
            //ceiling.Transform = Matrix.Translation(0, 2, 0);
            //ceiling.Material = floor.Material;

            // Wall
            //var wall = new CSharp.Plane();
            //wall.Transform = Matrix.Translation(0, 0, 5) *
            //                 Matrix.RotationX(Math.PI / 2);
            //wall.Material = floor.Material;

            var w1 = new CSharp.Plane();
            w1.Transform = Matrix.Translation(0, 0, 3) *
                           Matrix.RotationX(Math.PI / 2);
            w1.Material = floor.Material;

            var w2 = new CSharp.Plane();
            w2.Transform = Matrix.Translation(-2, 0, 3) *
                           Matrix.RotationY(-Math.PI / 4) *
                           Matrix.RotationX(Math.PI / 2);
            w2.Material = floor.Material;

            var w3 = new CSharp.Plane();
            w3.Transform = Matrix.Translation(2, 0, 3) *
                           Matrix.RotationY(Math.PI / 4) *
                           Matrix.RotationX(Math.PI / 2);
            w3.Material = floor.Material;

            var w4 = new CSharp.Plane();
            w4.Transform = Matrix.Translation(0, 0, -3) *
                           Matrix.RotationX(Math.PI / 2);
            w4.Material = floor.Material;

            var w5 = new CSharp.Plane();
            w5.Transform = Matrix.Translation(2, 0, -3) *
                           Matrix.RotationY(-Math.PI / 4) *
                           Matrix.RotationX(Math.PI / 2);
            w5.Material = floor.Material;

            var w6 = new CSharp.Plane();
            w6.Transform = Matrix.Translation(-2, 0, -3) *
                           Matrix.RotationY(Math.PI / 4) *
                           Matrix.RotationX(Math.PI / 2);
            w6.Material = floor.Material;

            // Middle Sphere
            var middle = new Sphere();
            middle.Transform = Matrix.Translation(-0.5, 1, 0.5);
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;

            // Right Sphere
            var right = new Sphere();
            right.Transform = Matrix.Translation(1.5, 0.5, -0.5) *
                              Matrix.Scaling(0.5, 0.5, 0.5);
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;

            // Left Sphere
            var left = new Sphere();
            left.Transform = Matrix.Translation(-1.5, 0.33, -0.75) *
                              Matrix.Scaling(0.33, 0.33, 0.33);
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;

            var world = new World();
            // Ceiling
            //world.Lights.Add(new PointLight(new Point(-10, 1.5, -10), Color.White));
            world.Lights.Add(new PointLight(new Point(0, 10, 0), Color.White));
            world.Objects.Add(floor);
            //world.Objects.Add(wall);
            //world.Objects.Add(ceiling);
            world.Objects.Add(w1);
            world.Objects.Add(w2);
            world.Objects.Add(w3);
            world.Objects.Add(w4);
            world.Objects.Add(w5);
            world.Objects.Add(w6);
            world.Objects.Add(middle);
            world.Objects.Add(right);
            world.Objects.Add(left);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 15, 0),
                                                    Point.Zero,
                                                    Vector.VectorX);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("plane.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
