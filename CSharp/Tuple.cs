using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Tuple
    {
        public Tuple(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public double w { get; set; }

        public bool IsPoint => w.IsEqual(1.0);
        public bool IsVector => w.IsEqual(0.0);

        public static Tuple Point(double x, double y, double z)
        {
            return new Tuple(x, y, z, 1.0);
        }

        public static Tuple Vector(double x, double y, double z)
        {
            return new Tuple(x, y, z, 0.0);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Tuple;
            return ((other != null) &&
                    (x.IsEqual(other.x)) &&
                    (y.IsEqual(other.y)) &&
                    (z.IsEqual(other.z)) &&
                    (w.IsEqual(other.w)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public override string ToString()
        {
            return $"({x},{y},{z},{w})";
        }
    }
}
