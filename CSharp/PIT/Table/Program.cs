using System;
using CSharp;

namespace Table
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a table scene!");

            // Floor and Ceiling
            var room = new Cube();
            room.Transform = Matrix.Translation(0, 7, 0) *
                             Matrix.Scaling(20, 7, 20);
            room.Material.Pattern = new CheckersPattern(new Color(0.25, 0.25, 0.25), new Color(0, 0, 0));
            room.Material.Pattern.Transform = Matrix.Scaling(0.07, 0.07, 0.07);
            room.Material.Ambient = 0.25;
            room.Material.Diffuse = 0.7;
            room.Material.Specular = 0.9;
            room.Material.Shininess = 300;
            room.Material.Reflective = 0.1;

            // Walls
            var walls = new Cube();
            walls.Transform = Matrix.Scaling(10, 10, 10);
            walls.Material.Pattern = new CheckersPattern(new Color(0.3725, 0.2902, 0.2275), new Color(0.4863, 0.3765, 0.2941));
            walls.Material.Pattern.Transform = Matrix.Scaling(0.05, 20, 0.05);
            walls.Material.Ambient = 0.1;
            walls.Material.Diffuse = 0.7;
            walls.Material.Specular = 0.9;
            walls.Material.Shininess = 300;
            walls.Material.Reflective = 0.1;

            // Table Top
            var top = new Cube();
            top.Transform = Matrix.Translation(0, 3.1, 0) *
                            Matrix.Scaling(3, 0.1, 2);
            top.Material.Pattern = new StripePattern(new Color(0.6588, 0.5098, 0.4000), new Color(0.5529, 0.4235, 0.3255));
            top.Material.Pattern.Transform = Matrix.Scaling(0.05, 0.05, 0.05) *
                                             Matrix.RotationY(0.1);
            top.Material.Ambient = 0.1;
            top.Material.Diffuse = 0.7;
            top.Material.Specular = 0.9;
            top.Material.Shininess = 300;
            top.Material.Reflective = 0.2;

            // Leg 1
            var leg1 = new Cube();
            leg1.Transform = Matrix.Translation(2.7, 1.5, -1.7) *
                            Matrix.Scaling(0.1, 1.5, 0.1);
            leg1.Material.Color = new Color(0.5529, 0.4235, 0.3255);
            leg1.Material.Ambient = 0.2;
            leg1.Material.Diffuse = 0.7;

            // Leg 2
            var leg2 = new Cube();
            leg2.Transform = Matrix.Translation(2.7, 1.5, 1.7) *
                            Matrix.Scaling(0.1, 1.5, 0.1);
            leg2.Material.Color = new Color(0.5529, 0.4235, 0.3255);
            leg2.Material.Ambient = 0.2;
            leg2.Material.Diffuse = 0.7;

            // Leg 3
            var leg3 = new Cube();
            leg3.Transform = Matrix.Translation(-2.7, 1.5, -1.7) *
                            Matrix.Scaling(0.1, 1.5, 0.1);
            leg3.Material.Color = new Color(0.5529, 0.4235, 0.3255);
            leg3.Material.Ambient = 0.2;
            leg3.Material.Diffuse = 0.7;

            // Leg 4
            var leg4 = new Cube();
            leg4.Transform = Matrix.Translation(-2.7, 1.5, 1.7) *
                            Matrix.Scaling(0.1, 1.5, 0.1);
            leg4.Material.Color = new Color(0.5529, 0.4235, 0.3255);
            leg4.Material.Ambient = 0.2;
            leg4.Material.Diffuse = 0.7;

            // Glass cube
            var glass = new Cube();
            glass.HasShadow = false;
            glass.Transform = Matrix.Translation(0, 3.45001, 0) *
                            Matrix.RotationY(0.2) *
                            Matrix.Scaling(0.25, 0.25, 0.25);
            glass.Material.Color = new Color(1, 1, 0.8);
            glass.Material.Ambient = 0;
            glass.Material.Diffuse = 0.3;
            glass.Material.Specular = 0.9;
            glass.Material.Shininess = 300;
            glass.Material.Reflective = 0.7;
            glass.Material.Transparency = 0.7;
            glass.Material.RefractiveIndex = 1.5;

            // Cube 1
            var cube1 = new Cube();
            cube1.Transform = Matrix.Translation(1, 3.35, -0.9) *
                            Matrix.RotationY(-0.4) *
                            Matrix.Scaling(0.15, 0.15, 0.15);
            cube1.Material.Color = new Color(1, 0.5, 0.5);
            cube1.Material.Diffuse = 0.4;
            cube1.Material.Reflective = 0.6;

            // Cube 2
            var cube2 = new Cube();
            cube2.Transform = Matrix.Translation(-1.5, 3.27, 0.3) *
                            Matrix.RotationY(0.4) *
                            Matrix.Scaling(0.15, 0.07, 0.15);
            cube2.Material.Color = new Color(1, 1, 0.5);

            // Cube 3
            var cube3 = new Cube();
            cube3.Transform = Matrix.Translation(0, 3.25, 1) *
                            Matrix.RotationY(0.4) *
                            Matrix.Scaling(0.2, 0.05, 0.05);
            cube3.Material.Color = new Color(0.5, 1, 0.5);

            // Cube 4
            var cube4 = new Cube();
            cube4.Transform = Matrix.Translation(-0.6, 3.4, -1) *
                            Matrix.RotationY(0.8) *
                            Matrix.Scaling(0.05, 0.2, 0.05);
            cube4.Material.Color = new Color(0.5, 0.5, 1);

            // Cube 5
            var cube5 = new Cube();
            cube5.Transform = Matrix.Translation(2, 3.4, 1) *
                            Matrix.RotationY(0.8) *
                            Matrix.Scaling(0.05, 0.2, 0.05);
            cube5.Material.Color = new Color(0.5, 1, 1);

            // Frame 1
            var frame1 = new Cube();
            frame1.Transform = Matrix.Translation(-10, 4, 1) *
                            Matrix.Scaling(0.05, 1, 1);
            frame1.Material.Color = new Color(0.7098, 0.2471, 0.2196);
            frame1.Material.Diffuse = 0.6;

            // Frame 2
            var frame2 = new Cube();
            frame2.Transform = Matrix.Translation(-10, 3.4, 2.7) *
                            Matrix.Scaling(0.05, 0.4, 0.4);
            frame2.Material.Color = new Color(0.2667, 0.2706, 0.6902);
            frame2.Material.Diffuse = 0.6;

            // Frame 3
            var frame3 = new Cube();
            frame3.Transform = Matrix.Translation(-10, 4.6, 2.7) *
                            Matrix.Scaling(0.05, 0.4, 0.4);
            frame3.Material.Color = new Color(0.3098, 0.5961, 0.3098);
            frame3.Material.Diffuse = 0.6;

            // Mirror Frame 4
            var frame4 = new Cube();
            frame4.Transform = Matrix.Translation(-2, 3.5, 9.95) *
                            Matrix.Scaling(5, 1.5, 0.05);
            frame4.Material.Color = new Color(0.3882, 0.2627, 0.1882);
            frame4.Material.Diffuse = 0.7;

            // Mirror
            var mirror = new Cube();
            mirror.HasShadow = false;
            mirror.Transform = Matrix.Translation(-2, 3.5, 9.95) *
                            Matrix.Scaling(4.8, 1.4, 0.06);
            mirror.Material.Color = new Color(0, 0, 0);
            mirror.Material.Ambient = 0;
            mirror.Material.Diffuse = 0;
            mirror.Material.Specular = 1;
            mirror.Material.Shininess = 300;
            mirror.Material.Reflective = 1;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(0, 6.9, -5), new Color(1, 1, 0.9)));
            world.Objects.Add(room);
            world.Objects.Add(walls);
            world.Objects.Add(top);
            world.Objects.Add(leg1);
            world.Objects.Add(leg2);
            world.Objects.Add(leg3);
            world.Objects.Add(leg4);
            world.Objects.Add(glass);
            world.Objects.Add(cube1);
            world.Objects.Add(cube2);
            world.Objects.Add(cube3);
            world.Objects.Add(cube4);
            world.Objects.Add(cube5);
            world.Objects.Add(frame1);
            world.Objects.Add(frame2);
            world.Objects.Add(frame3);
            world.Objects.Add(frame4);
            world.Objects.Add(mirror);

            var camera = new Camera(400, 200, 0.785);
            camera.Transform = Matrix.ViewTransform(new Point(8, 6, -8),
                                                    new Point(0, 3, 0),
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("table.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
