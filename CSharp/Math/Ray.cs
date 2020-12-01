using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Ray
    {
        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point Origin { get; }
        public Vector Direction { get; }

        public Point Position(double t)
        {
            return new Point(Origin + Direction * t);
        }

        public Ray Transform(Matrix m)
        {
            var origin = m * Origin;
            var direction = m * Direction;

            return new Ray(origin, direction);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Ray;
            return ((other != null) &&
                    Origin.Equals(other.Origin) &&
                    Direction.Equals(other.Direction));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Direction);
        }

        public override string ToString()
        {
            return $"Ray {Origin} {Direction}";
        }
    }
}
