using System;

namespace CSharp
{
    public class PointLight
    {
        public PointLight(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Point Position { get; }
        public Color Intensity { get; }

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
