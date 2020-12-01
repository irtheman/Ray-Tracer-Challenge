using CSharp;
using System;

namespace Transparency
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a transparency!");

            // Floor of scene
            var floor = new Plane();
            floor.Transform = Matrix.RotationY(0.31415);
            floor.Material.Reflective = 0.1;
            floor.Material.Specular = 0.8;
            floor.Material.Diffuse = 0.4;
            floor.Material.Ambient = 0.5;
            floor.Material.Pattern = new CheckersPattern(Color.Black, new Color(0.75, 0.75, 0.75));

            // Ceiling of scene
            var ceiling = new Plane();
            ceiling.Transform = Matrix.Translation(0, 5, 0);
            ceiling.Material.Specular = 0;
            ceiling.Material.Ambient = 0.5;
            ceiling.Material.Pattern = new CheckersPattern(new Color(0.85, 0.85, 0.85), new Color(1, 1, 1));
            ceiling.Material.Pattern.Transform = Matrix.Scaling(0.2, 0.2, 0.2);

            // West wall
            var wwall = new Plane();
            wwall.Transform = Matrix.Translation(-5, 0, 0) *
                              Matrix.RotationZ(1.5708) *
                              Matrix.RotationY(1.5708);
            wwall.Material.Specular = 0;
            wwall.Material.Pattern = new CheckersPattern(Color.Black, new Color(0.75, 0.75, 0.75));
            wwall.Material.Pattern.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

            // East wall
            var ewall = new Plane();
            ewall.Transform = Matrix.Translation(5, 0, 0) *
                              Matrix.RotationZ(1.5708) *
                              Matrix.RotationY(1.5708);
            ewall.Material.Specular = 0;
            ewall.Material.Pattern = new CheckersPattern(Color.Black, new Color(0.75, 0.75, 0.75));
            ewall.Material.Pattern.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

            // North wall
            var nwall = new Plane();
            nwall.Transform = Matrix.Translation(0, 0, 5) *
                              Matrix.RotationX(1.5708);
            nwall.Material.Specular = 0;
            nwall.Material.Pattern = new CheckersPattern(Color.Black, new Color(0.75, 0.75, 0.75));
            nwall.Material.Pattern.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

            // South wall
            var swall = new Plane();
            swall.Transform = Matrix.Translation(0, 0, -5) *
                              Matrix.RotationX(1.5708);
            swall.Material.Specular = 0;
            swall.Material.Pattern = new CheckersPattern(Color.Black, new Color(0.75, 0.75, 0.75));
            swall.Material.Pattern.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

            // Background Ball 1
            var bball1 = new Sphere();
            bball1.Transform = Matrix.Translation(4, 1, 4);
            bball1.Material.Color = new Color(0.8, 0.1, 0.3);
            bball1.Material.Specular = 0;

            // Background Ball 2
            var bball2 = new Sphere();
            bball2.Transform = Matrix.Translation(4.6, 0.4, 2.9) *
                               Matrix.Scaling(0.4, 0.4, 0.4);
            bball2.Material.Color = new Color(0.1, 0.8, 0.2);
            bball2.Material.Shininess = 200;

            // Background Ball 3
            var bball3 = new Sphere();
            bball3.Transform = Matrix.Translation(2.6, 0.6, 4.4) *
                               Matrix.Scaling(0.6, 0.6, 0.6);
            bball3.Material.Color = new Color(0.2, 0.1, 0.8);
            bball3.Material.Shininess = 10;
            bball3.Material.Specular = 0.4;

            // Glass Ball
            var gball = new Sphere();
            gball.Transform = Matrix.Translation(0.25, 1, 0) *
                              Matrix.Scaling(1, 1, 1);
            gball.Material.Color = new Color(0.8, 0.8, 0.9);
            gball.Material.Ambient = 0;
            gball.Material.Diffuse = 0.2;
            gball.Material.Specular = 0.9;
            gball.Material.Shininess = 300;
            gball.Material.Transparency = 0.8;
            gball.Material.RefractiveIndex = 1.57;

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-4.9, 4.9, 1), Color.White));
            world.Objects.Add(floor);
            world.Objects.Add(ceiling);
            world.Objects.Add(wwall);
            world.Objects.Add(ewall);
            world.Objects.Add(nwall);
            world.Objects.Add(swall);
            world.Objects.Add(bball1);
            world.Objects.Add(bball2);
            world.Objects.Add(bball3);
            world.Objects.Add(gball);

            var camera = new Camera(400, 400, 0.5);
            camera.Transform = Matrix.ViewTransform(new Point(-4.5, 0.85, -4),
                                                    new Point(0, 0.85, 0),
                                                    Vector.VectorY);

            var c = camera.Render(world);

            System.IO.File.WriteAllText("transparency.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
