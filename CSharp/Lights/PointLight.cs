using System;
using System.Collections.Generic;

namespace CSharp
{
    public class PointLight : ILight
    {
        private List<Point> positions;

        public PointLight(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
            Samples = 1;
            JitterBy = null;

            positions = new List<Point>() { Position };
        }

        public Point Position { get; }
        public Color Intensity { get; }
        public int Samples { get; }
        public Sequence JitterBy { get; set; }

        public double IntensityAt(RTObject obj, Computations comps, World w)
        {
            if (w.IsShadowed(obj, comps, Position, comps.OverPoint))
                return 0.0;

            return 1.0;
        }

        public double IntensityAt(Point pt, World w)
        {
            if (w.IsShadowed(Position, pt))
                return 0.0;

            return 1.0;
        }

        public List<Point> SamplePoints()
        {
            return positions;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PointLight;
            return ((other != null) &&
                    (Position.Equals(other.Position)) &&
                    (Intensity.Equals(other.Intensity)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Intensity);
        }

        public override string ToString()
        {
            return $"Point Light: Position {Position} Intensity {Intensity}";
        }
    }
}
