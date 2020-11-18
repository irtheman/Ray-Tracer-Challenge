using CSharp;
using System;

namespace Sphere
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a sphere!");

            var rayOrigin = new Point(0, 0, -5);
            var wallZ = 10.0;
            var canvasPixels = 100;
            var wallSize = 7.0;
            var pixelSize = wallSize / canvasPixels;
            var half = wallSize / 2;

            var c = new Canvas(canvasPixels, canvasPixels);
            var color = new Color(1, 0, 0);

            var lightPosition = new Point(-10, 10, -10);
            
            // Lower light height
            //var lightPosition = new Point(-10, 0, -10);

            var lightColor = new Color(1, 1, 1);
            
            // Yellow light
            //var lightColor = new Color(1, 1, 0);

            var light = new PointLight(lightPosition, lightColor);

            // Normal sphere
            var shape = new CSharp.Sphere();
            shape.Material.Color = new Color(1, 0.2, 1);

            // White sphere
            //shape.Material.Color = new Color(1, 1, 1);

            // Make it blue-ish
            //shape.Material.Color = new Color(0.2, 1, 1);

            // Lower Diffuse and Specular
            //shape.Material.Diffuse = 0.5;
            //shape.Material.Specular = 0.5;

            // Brighter ambient
            //shape.Material.Ambient = 0.5;

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
                        var point = r.Position(xs.Hit.t);
                        var normal = xs.Hit.Object.Normal(point);
                        var eye = -r.Direction;

                        color = xs.Hit.Object.Material.Lighting(light, point, eye, normal);
                        c[x, y] = color;
                    }
                }
            }

            System.IO.File.WriteAllText("sphere.ppm", c.GetPPM());

            Console.WriteLine("Done!");
        }
    }
}
