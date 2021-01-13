using System;

namespace CSharp
{
    public class CheckersPattern : RTPattern
    {
        public CheckersPattern(Color a, Color b) : base(a, b)
        {
            Width = 0;
            Height = 0;
        }
        public CheckersPattern(int width, int height, Color a, Color b) : base(a, b)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }

        public override Color PatternAt(Point p)
        {
            return (int) (Math.Floor(p.x) + Math.Floor(p.y) + Math.Floor(p.z)) % 2 == 0 ? a : b;
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            var u2 = Math.Floor(u * Width);
            var v2 = Math.Floor(v * Height);

            if ((u2 + v2) % 2 == 0)
                return a;

            return b;
        }

    }
}
