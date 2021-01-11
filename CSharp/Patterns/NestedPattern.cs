using System;

namespace CSharp
{
    public class NestedPattern : RTPattern
    {
        private RTPattern Pattern1;
        private RTPattern Pattern2;

        public NestedPattern(RTPattern a, RTPattern b) : base(Color.White, Color.Black)
        {
            Pattern1 = a;
            Pattern2 = b;
        }

        public override Color PatternAt(Point p)
        {
            Color color;

            if ((int) (Math.Floor(p.x) + Math.Floor(p.y) + Math.Floor(p.z)) % 2 == 0)
            {
                p = Pattern1.TransformInverse * p;
                color = Pattern1.PatternAt(p);
            }
            else
            {
                p = Pattern2.TransformInverse * p;
                color = Pattern2.PatternAt(p);
            }

            return color;
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
