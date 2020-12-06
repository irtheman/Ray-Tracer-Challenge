using CSharp;
using System;

namespace Cone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a scene with a cone!");

            // Floor of scene
            var floor = new Plane();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            floor.Material.Pattern = new CheckersPattern(Color.White, Color.Black);

            // Left Wall
            var leftWall = new Plane();
            leftWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(-Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2);
            leftWall.Material = floor.Material;

            // Right Wall
            var rightWall = new Plane();
            rightWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2);
            rightWall.Material = floor.Material;

            // Middle Cylinder
            var middle = new Cylinder();
            middle.Minimum = -1;
            middle.Maximum = 1;
            middle.Closed = true;
            middle.Transform = Matrix.Translation(-0.5, 1, 0.5);
            middle.Material.Color = Color.White; // new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            middle.Material.Reflective = 1;

            // Right Cube
            var right = new Cube();
            right.Transform = Matrix.Translation(1.5, 0.5, -0.5) *
                              Matrix.Scaling(0.5, 0.5, 0.5);
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            right.Material.Reflective = 0.2;

            // Left Cone
            var left = new CSharp.Cone();
            left.Minimum = 0;
            left.Maximum = 1;
            left.Closed = true;
            left.Transform = Matrix.Translation(-1.5, 0, -0.75) *
                              Matrix.Scaling(0.75, 0.75, 0.75);
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

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1.5, -5),
                                                    Point.PointY,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("cone.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
