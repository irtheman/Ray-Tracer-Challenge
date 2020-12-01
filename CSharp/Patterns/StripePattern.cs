﻿using System;

namespace CSharp
{
    public class StripePattern : RTPattern
    {
        public StripePattern(Color a, Color b) : base(a, b)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return (int) Math.Floor(p.x) % 2 == 0 ? a : b;
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}