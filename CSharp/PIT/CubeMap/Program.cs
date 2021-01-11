using System;
using CSharp;

namespace CubeMapped
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a cube mapping scene!");

            var cube1 = MappedCube();
            cube1.Transform = Matrix.Translation(-6, 2, 0) *
                              Matrix.RotationX(0.7854) *
                              Matrix.RotationY(0.7854);

            var cube2 = MappedCube();
            cube2.Transform = Matrix.Translation(-2, 2, 0) *
                              Matrix.RotationX(0.7854) *
                              Matrix.RotationY(2.3562);

            var cube3 = MappedCube();
            cube3.Transform = Matrix.Translation(2, 2, 0) *
                              Matrix.RotationX(0.7854) *
                              Matrix.RotationY(3.927);

            var cube4 = MappedCube();
            cube4.Transform = Matrix.Translation(6, 2, 0) *
                              Matrix.RotationX(0.7854) *
                              Matrix.RotationY(5.4978);

            var cube5 = MappedCube();
            cube5.Transform = Matrix.Translation(-6, -2, 0) *
                              Matrix.RotationX(-0.7854) *
                              Matrix.RotationY(0.7854);

            var cube6 = MappedCube();
            cube6.Transform = Matrix.Translation(-2, -2, 0) *
                              Matrix.RotationX(-0.7854) *
                              Matrix.RotationY(2.3562);

            var cube7 = MappedCube();
            cube7.Transform = Matrix.Translation(2, -2, 0) *
                              Matrix.RotationX(-0.7854) *
                              Matrix.RotationY(3.927);

            var cube8 = MappedCube();
            cube8.Transform = Matrix.Translation(6, -2, 0) *
                              Matrix.RotationX(-0.7854) *
                              Matrix.RotationY(5.4978);

            var world = new World();
            world.Lights.Add(new PointLight(new Point(0, 100, -100), Color.White));
            world.Lights.Add(new PointLight(new Point(0, -100, -100), Color.White));
            world.Lights.Add(new PointLight(new Point(-100, 0, -100), Color.White));
            world.Lights.Add(new PointLight(new Point(100, 0, -100), Color.White));
            world.Objects.Add(cube1);
            world.Objects.Add(cube2);
            world.Objects.Add(cube3);
            world.Objects.Add(cube4);
            world.Objects.Add(cube5);
            world.Objects.Add(cube6);
            world.Objects.Add(cube7);
            world.Objects.Add(cube8);

            var camera = new Camera(800, 400, 0.8);
            camera.Transform = Matrix.ViewTransform(new Point(0, 0, -20),
                                                    Point.Zero,
                                                    Vector.VectorY);
            var c = camera.Render(world);

            System.IO.File.WriteAllText("cubemap.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }

        private static Cube MappedCube()
        {
            var left = new UVAlignCheck(Color.Yellow, Color.Cyan, Color.Red, Color.Blue, Color.Brown);
            var front = new UVAlignCheck(Color.Cyan, Color.Red, Color.Yellow, Color.Brown, Color.Green);
            var right = new UVAlignCheck(Color.Red, Color.Yellow, Color.Purple, Color.Green, Color.White);
            var back = new UVAlignCheck(Color.Green, Color.Purple, Color.Cyan, Color.White, Color.Blue);
            var up = new UVAlignCheck(Color.Brown, Color.Cyan, Color.Purple, Color.Red, Color.Yellow);
            var down = new UVAlignCheck(Color.Purple, Color.Brown, Color.Green, Color.Blue, Color.White);

            var cube = new Cube();
            cube.Material.Pattern = new CubeMapPattern(left, front, right, back, up, down);
            cube.Material.Ambient = 0.2;
            cube.Material.Specular = 0;
            cube.Material.Diffuse = 0.8;

            return cube;
        }
    }
}
