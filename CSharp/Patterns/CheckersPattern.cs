using System;

namespace CSharp
{
    public class CheckersPattern : RTPattern
    {
        public CheckersPattern(Color a, Color b) : base(a, b)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return (int) (Math.Floor(p.x) + Math.Floor(p.y) + Math.Floor(p.z)) % 2 == 0 ? a : b;
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}
