using System;
using CSharp;

namespace MengerSponge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a Menger Sponge scene!");

            var csg = MengerSponge(4);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-1, 5, -5), Color.White));
            world.Lights.Add(new PointLight(new Point(-5, 5, -1), Color.White));
            world.Lights.Add(new PointLight(new Point(5, 5, -1), Color.White));
            world.Objects.Add(csg);

            var camera = new Camera(400, 200, 1.2);
            camera.Transform = Matrix.ViewTransform(new Point(2, 2, -5),
                                                    new Point(0, 0, -1),
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("skybox.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }

        private static CSG MengerSponge(int m)
        {
            var thing1 = MakeCubes(-1.5, -1.5, 1.5, 1.5, 0, m);
            var thing2 = MakeCubes(-1.5, -1.5, 1.5, 1.5, 0, m);
            var thing3 = MakeCubes(-1.5, -1.5, 1.5, 1.5, 0, m);
            var c1 = thing1;
            var c2 = thing2;
            c2.Transform = Matrix.RotationX(Math.PI / 2);
            var c3 = thing3;
            c3.Transform = Matrix.RotationY(Math.PI / 2);
            var u1 = new CSG(CsgOperation.Union, c1, c2);
            var u2 = new CSG(CsgOperation.Union, u1, c3);
            var cube = new Cube();
            cube.Transform = Matrix.Scaling(1.5, 1.5, 1.5);
            var ret = new CSG(CsgOperation.Difference, cube, u2);

            return ret;
        }

        private static Group MakeCubes(double x1, double y1, double x2, double y2, int n, int max)
        {
            var g = new Group();

            var deltaX = (x2 - x1) / 3;
            var deltaY = (x2 - x1) / 3;

            var sx = deltaX / 2;
            var sy = deltaY / 2;
            var tx = x1 + (x2 - x1) / 2;
            var ty = y1 + (y2 - y1) / 2;
            var cube = new Cube();
            cube.Transform = Matrix.Translation(tx, ty, 0.5) *
                             Matrix.Scaling(sx, sy, 2.1);
            g.Add(cube);

            if (n < max)
            {
                // left col
                var g1 = MakeCubes(x1 + 0 * deltaX, y1 + 0 * deltaY, x1 + 1 * deltaX, y1 + 1 * deltaY, n + 1, max);
                var g2 = MakeCubes(x1 + 0 * deltaX, y1 + 1 * deltaY, x1 + 1 * deltaX, y1 + 2 * deltaY, n + 1, max);
                var g3 = MakeCubes(x1 + 0 * deltaX, y1 + 2 * deltaY, x1 + 1 * deltaX, y1 + 3 * deltaY, n + 1, max);

                // center col
                var g4 = MakeCubes(x1 + 1 * deltaX, y1 + 0 * deltaY, x1 + 2 * deltaX, y1 + 1 * deltaY, n + 1, max);
                //var g5 = MakeCubes(x1 + 1*deltaX, y1+1*deltaY, x1 + 2*deltaX, y1 + 2*deltaY, n + 1, max);
                var g6 = MakeCubes(x1 + 1 * deltaX, y1 + 2 * deltaY, x1 + 2 * deltaX, y1 + 3 * deltaY, n + 1, max);

                // right col
                var g7 = MakeCubes(x1 + 2 * deltaX, y1 + 0 * deltaY, x1 + 3 * deltaX, y1 + 1 * deltaY, n + 1, max);
                var g8 = MakeCubes(x1 + 2 * deltaX, y1 + 1 * deltaY, x1 + 3 * deltaX, y1 + 2 * deltaY, n + 1, max);
                var g9 = MakeCubes(x1 + 2 * deltaX, y1 + 2 * deltaY, x1 + 3 * deltaX, y1 + 3 * deltaY, n + 1, max);

                g.Add(g1);
                g.Add(g2);
                g.Add(g3);
                g.Add(g4);
                g.Add(g6);
                g.Add(g7);
                g.Add(g8);
                g.Add(g9);
            }

            return g;
        }
    }
}
