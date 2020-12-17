using System;
using System.Threading.Tasks;

namespace CSharp
{
    public class Camera
    {
        private Matrix transform;
        private Matrix transformInverse;

        public Camera(int hsize, int vsize, double fieldOfView)
        {
            HSize = hsize;
            VSize = vsize;
            FOV = fieldOfView;
            Transform = Matrix.Identity;

            var halfView = Math.Tan(FOV / 2);
            var aspect = HSize / (double) VSize;

            if (aspect >= 1)
            {
                HalfWidth = halfView;
                HalfHeight = halfView / aspect;
            }
            else
            {
                HalfWidth = halfView * aspect;
                HalfHeight = halfView;
            }

            PixelSize = (HalfWidth * 2) / HSize;
        }

        public int HSize { get; }
        public int VSize { get; }
        public double HalfWidth { get; }
        public double HalfHeight { get; }
        public double FOV { get; }
        public double PixelSize { get; }
        public Matrix Transform
        {
            get
            {
                return transform;
            }

            set
            {
                transform = value;
                transformInverse = value.Inverse;
            }
        }

        public Ray RayForPixel(int x, int y)
        {
            var xOffset = (x + 0.5) * PixelSize;
            var yOffset = (y + 0.5) * PixelSize;
            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;
            var inverse = transformInverse;
            var pixel = inverse * new Point(worldX, worldY, -1);
            var origin = inverse * Point.Zero;
            var direction = (new Vector(pixel - origin)).Normalize;

            return new Ray(origin, direction);
        }

        public Canvas Render(World w)
        {
            var image = new Canvas(HSize, VSize);

            Parallel.For(0, VSize, y =>
            {
                for (int x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var color = w.ColorAt(ray);
                    image[x, y] = color;
                }
            });

            return image;
        }
    }
}