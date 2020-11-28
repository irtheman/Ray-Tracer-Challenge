using CSharp;
using System;

namespace Circle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a circle!");

            var rayOrigin = new Point(0, 0, -5);
            var wallZ = 10.0;
            var canvasPixels = 100;
            var wallSize = 7.0;
            var pixelSize = wallSize / canvasPixels;
            var half = wallSize / 2;

            var c = new Canvas(canvasPixels, canvasPixels);
            var color = Color.Red;
            
            // Normal sphere
            var shape = new Sphere();

            // Shrink on Y axis
            //shape.Transform = Matrix.Scaling(1, 0.5, 1);

            // Shrink on X axis
            //shape.Transform = Matrix.Scaling(0.5, 1, 1);

            // Shrink and rotate
            //shape.Transform = Matrix.RotationZ(Math.PI / 4) * Matrix.Scaling(0.5, 1, 1);

            // Shrink and skew
            //shape.Transform = Matrix.Shearing(1, 0, 0, 0, 0, 0) * Matrix.Scaling(0.5, 1, 1);

            for (int y = 0; y < canvasPixels; y++)
            {
                var worldY = half - pixelSize * y;

                for (int x = 0; x < canvasPixels; x++)
                {
                    var worldX = -half + pixelSize * x;
                    var position = new Point(worldX, worldY, wallZ);

                    var r = new Ray(rayOrigin, (new Vector(position - rayOrigin)).Normalize);
                    var xs = shape.Intersect(r);

                    if (xs.Hit != null)
                    {
                        c[x, y] = color;
                    }
                }
            }

            System.IO.File.WriteAllText("circle.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
