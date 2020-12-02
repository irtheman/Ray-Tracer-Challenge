using CSharp;
using System;

namespace Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a pattern scene!");

            // Floor of scene
            var floor = new Plane();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            floor.Material.Pattern = new PerlinNoisePattern(new StripePattern(Color.White, Color.Red));

            // Left Wall
            var leftWall = new Plane();
            leftWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(-Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2);
            leftWall.Material.Color = new Color(1, 0.9, 0.9);
            leftWall.Material.Specular = 0;

            var p1 = new StripePattern(Color.DarkGreen, Color.Grey);
            p1.Transform = Matrix.Scaling(0.5, 0.5, 0.5);
            var p2 = new StripePattern(Color.DarkGreen, Color.Grey);
            p2.Transform = Matrix.Scaling(0.5, 0.5, 0.5) * Matrix.RotationY(Math.PI / 2);
            leftWall.Material.Pattern = new BlendedPattern(p1, p2);

            // Right Wall
            var rightWall = new Plane();
            rightWall.Transform = Matrix.Translation(0, 0, 5) *
                                 Matrix.RotationY(Math.PI / 4) *
                                 Matrix.RotationX(Math.PI / 2);
            rightWall.Material.Color = new Color(1, 0.9, 0.9);
            rightWall.Material.Specular = 0;

            // Rotated stripes
            //rightWall.Material.Pattern = new StripePattern(Color.White, Color.Red);
            //rightWall.Material.Pattern.Transform = Matrix.RotationY(Math.PI / 2);

            // Blended Pattern
            //var p1 = new StripePattern(Color.DarkGreen, Color.Grey);
            //p1.Transform = Matrix.Scaling(0.5, 0.5, 0.5);
            //var p2 = new StripePattern(Color.DarkGreen, Color.Grey);
            //p2.Transform = Matrix.Scaling(0.5, 0.5, 0.5) * Matrix.RotationY(Math.PI / 2);
            //rightWall.Material.Pattern = new BlendedPattern(p1, p2);

            // Nested Pattern
            //var p1 = new CheckersPattern(Color.White, Color.Black);
            //p1.Transform = Matrix.Scaling(0.5, 0.5, 0.5);
            //var p2 = new RingPattern(Color.Red, Color.Blue);
            //p2.Transform = Matrix.Scaling(0.5, 0.5, 0.5) * Matrix.RotationY(Math.PI / 2);
            //rightWall.Material.Pattern = new NestedPattern(p1, p2);

            // Nested Pattern 2
            var p3 = new StripePattern(Color.Red, Color.DarkRed);
            p3.Transform = Matrix.RotationY(Math.PI / 3) * Matrix.Scaling(0.25, 0.25, 0.25);
            var p4 = new StripePattern(Color.Grey, Color.Black);
            p4.Transform = Matrix.RotationY(-Math.PI / 3) * Matrix.Scaling(0.25, 0.25, 0.25);
            rightWall.Material.Pattern = new NestedPattern(p3, p4);

            // Other patterns
            //rightWall.Material.Pattern = new StripePattern(Color.White, Color.Red);
            //rightWall.Material.Pattern = new GradientPattern(Color.Red, Color.Blue);
            //rightWall.Material.Pattern = new RingPattern(Color.Red, Color.White);
            //rightWall.Material.Pattern = new CheckersPattern(Color.White, Color.Black);
            //rightWall.Material.Pattern = new RadialGradientPattern(Color.Blue, Color.Black);
            //rightWall.Material.Pattern = new PerlinNoisePattern(new StripePattern(Color.White, Color.Red));

            // Middle Sphere
            var middle = new Sphere();
            middle.Transform = Matrix.Translation(-0.1, 1, 0.5);
            middle.Material.Color = Color.White;
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            middle.Material.Pattern = new GradientPattern(Color.Red, Color.Blue);
            middle.Material.Pattern.Transform = Matrix.Scaling(2, 2, 2) * Matrix.Translation(-0.5, 0, 0);

            // Right Sphere
            var right = new Sphere();
            right.Transform = Matrix.Translation(1.9, 0.5, -0.5) *
                              Matrix.Scaling(0.5, 0.5, 0.5);
            right.Material.Color = Color.White;
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            right.Material.Pattern = new RingPattern(Color.White, Color.Red);
            right.Material.Pattern.Transform = Matrix.RotationZ(Math.PI / 2) * Matrix.Scaling(0.15, 0.15, 0.15);

            // Left Sphere
            var left = new Sphere();
            left.Transform = Matrix.Translation(-1.1, 0.33, -0.75) *
                              Matrix.Scaling(0.33, 0.33, 0.33);
            left.Material.Color = Color.White;
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            left.Material.Pattern = new RadialGradientPattern(Color.White, Color.Red);
            left.Material.Pattern.Transform = Matrix.Scaling(1.5, 1.5, 1.5);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-5, 5, -10), Color.White));
            //world.Lights.Add(new PointLight(new Point(5, 5, -10), Color.White));
            world.Objects.Add(floor);
            world.Objects.Add(leftWall);
            world.Objects.Add(rightWall);
            world.Objects.Add(middle);
            world.Objects.Add(right);
            world.Objects.Add(left);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1, -5),
                                                    Point.PointY,
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("patterns.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
