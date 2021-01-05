using System.Collections.Generic;

namespace CSharp
{
    public interface ILight
    {
        public Point Position { get; }
        public Color Intensity { get; }
        public int Samples { get; }
        public double IntensityAt(RTObject obj, Computations comps, World w);
        public double IntensityAt(Point pt, World w);
        public Sequence JitterBy { get; }

        List<Point> SamplePoints();
    }
}
