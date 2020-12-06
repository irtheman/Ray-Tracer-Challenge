using CSharp;
using System;

namespace Hexagon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a hexagon scene!");

            var hex = GenerateHexagon();
            hex.Material.Pattern = new CheckersPattern(new Color(1, 0, 0), new Color(0, 0, 1));
            hex.Material.Pattern.Transform = Matrix.Scaling(0.25, 0.25, 0.25);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(-10, 10, -10), Color.White));
            world.Objects.Add(hex);

            var camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0.5, 1.25, -3),
                                                    Point.Zero,
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("hexagon.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }

        private static Group GenerateHexagon()
        {
            var hex = new Group();

            for (int n = 0; n < 6; n++)
            {
                var side = HexagonSide();
                side.Transform = Matrix.RotationY(n * Math.PI / 3);
                hex.Add(side);
            }

            return hex;
        }

        private static Group HexagonSide()
        {
            var side = new Group();

            side.Add(HexagonCorner());
            side.Add(HexagonEdge());

            return side;
        }

        private static Cylinder HexagonEdge()
        {
            var edge = new Cylinder(0, 1);
            edge.Transform = Matrix.Translation(0, 0, -1) *
                             Matrix.RotationY(-Math.PI / 6) *
                             Matrix.RotationZ(-Math.PI / 2) *
                             Matrix.Scaling(0.25, 1, 0.25);

            return edge;
        }

        private static Sphere HexagonCorner()
        {
            var corner = new Sphere();
            corner.Transform = Matrix.Translation(0, 0, -1) *
                               Matrix.Scaling(0.25, 0.25, 0.25);

            return corner;
        }
    }
}
