using System;

namespace CSharp
{
    public class UVAlignCheck : RTPattern
    {
        private Color c;
        private Color d;
        private Color e;

        public UVAlignCheck(Color main, Color ul) : base(main, ul)
        {
            // Nothing to do here
        }

        public UVAlignCheck(Color main, Color ul, Color ur, Color bl, Color br) : base(main, ul)
        {
            c = ur;
            d = bl;
            e = br;
        }

        public override Color PatternAt(Point p)
        {
            return Color.Black;
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            if (v > 0.8)
            {
                if (u < 0.2) return b;
                if (u > 0.8) return c;
            }
            else if (v < 0.2)
            {
                if (u < 0.2) return d;
                if (u > 0.8) return e;
            }

            return a;
        }
    }
}
