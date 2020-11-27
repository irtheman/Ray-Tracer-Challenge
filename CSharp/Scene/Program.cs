using CSharp;
using System;

namespace Scene
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a scene!");

            // Floor of scene
            var floor = new Sphere();
            floor.Transform = Matrix.Scaling(10, 0.01, 10);
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;

            // Left Wall
            var leftWall = new Sphere();
            leftWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(-Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2) *
                                 Matrix.Scaling(10, 0.01, 10);
            leftWall.Material = floor.Material;

            // Right Wall
            var rightWall = new Sphere();
            rightWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2) *
                                 Matrix.Scaling(10, 0.01, 10);
            rightWall.Material = floor.Material;

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
            world.Lights.Add(new PointLight(new Point(-10, 10, -10), Color.White));
            world.Objects.Add(floor);
            world.Objects.Add(leftWall);
            world.Objects.Add(rightWall);
            world.Objects.Add(middle);
            world.Objects.Add(right);
            world.Objects.Add(left);

            var camera = new Camera(100, 50, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1.5, -5),
                                                    Point.PointY,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("scene.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
