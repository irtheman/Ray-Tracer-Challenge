﻿using System;

namespace CSharp
{
    public class RadialGradientPattern : RTPattern
    {
        public RadialGradientPattern(Color a, Color b) : base(a, b)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            var v = new Vector(p.x, p.y, p.z);
            var m = v.Magnitude;

            return a + (b - a) * (m - Math.Floor(m));
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            return Color.Black;
        }
    }
}
