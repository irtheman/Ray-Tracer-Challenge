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
    }
}
