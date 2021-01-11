using System;

namespace CSharp
{
    public class UVImage : RTPattern
    {
        private Canvas canvas;

        public UVImage(Canvas c) : base(Color.Black, Color.Black)
        {
            canvas = c;
        }

        public override Color PatternAt(Point p)
        {
            return Color.Black;
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            v = 1 - v;

            var x = u * (canvas.Width - 1);
            var y = v * (canvas.Height - 1);

            return canvas[(int)Math.Round(x), (int)Math.Round(y)];
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}
