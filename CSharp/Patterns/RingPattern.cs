using System;

namespace CSharp
{
    public class RingPattern : RTPattern
    {
        public RingPattern(Color a, Color b) : base(a, b)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return (int) Math.Floor(Math.Sqrt(p.x * p.x + p.z * p.z)) % 2 == 0 ? a : b;
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}
