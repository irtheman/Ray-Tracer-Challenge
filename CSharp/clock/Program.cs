using CSharp;
using System;

namespace clock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a clock!");

            var c = new Canvas(500, 500);
            var color = new Color(1, 1, 1);
            var radius = (int)(c.Width * 3.0 / 8.0);

            var center = Matrix.Translation(c.Height / 2, c.Width / 2, 0);
            var scale = Matrix.Scaling(radius, radius, 0);
            var twelve = new Point(0, -1, 0);

            for (int i = 0; i < 12; i++)
            {
                var rot = Matrix.RotationZ(i * Math.PI / 6);
                var pos = center * scale * rot * twelve;

                c[(int)pos.x, (int)pos.y] = color;
            }

            System.IO.File.WriteAllText("clock.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
