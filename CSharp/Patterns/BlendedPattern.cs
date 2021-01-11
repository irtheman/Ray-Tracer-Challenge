using System;

namespace CSharp
{
    public class BlendedPattern : RTPattern
    {
        private RTPattern Pattern1;
        private RTPattern Pattern2;

        public BlendedPattern(RTPattern a, RTPattern b) : base(Color.White, Color.Black)
        {
            Pattern1 = a;
            Pattern2 = b;
        }

        public override Color PatternAt(Point p)
        {
            var p1 = Pattern1.TransformInverse * p;
            var p2 = Pattern2.TransformInverse * p;

            var c1 = Pattern1.PatternAt(p1);
            var c2 = Pattern2.PatternAt(p2);

            return (c1 + c2) * 0.5;
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            return Color.Black;
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}
