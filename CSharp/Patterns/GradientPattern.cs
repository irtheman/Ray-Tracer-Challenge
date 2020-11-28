using System;

namespace CSharp
{
    public class GradientPattern : RTPattern
    {
        public GradientPattern(Color a, Color b) : base(a, b)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return a + (b - a) * (p.x - Math.Floor(p.x));
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}
