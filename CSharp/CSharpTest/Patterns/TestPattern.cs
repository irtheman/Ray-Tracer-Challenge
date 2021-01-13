using CSharp;

namespace CSharpTest
{
    public class TestPattern : RTPattern
    {
        public TestPattern() : base(Color.White, Color.Black)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return new Color(p.x, p.y, p.z);
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            return Color.Black;
        }
    }
}
