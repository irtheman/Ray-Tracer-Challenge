﻿using System;

namespace CSharp
{
    public class SolidColorPattern : RTPattern
    {
        public SolidColorPattern(Color a) : base(a, Color.Black)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return a;
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }
}