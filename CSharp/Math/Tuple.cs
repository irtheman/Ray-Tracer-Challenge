using System;

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

        public double x { get; protected set; }
        public double y { get; protected set; }
        public double z { get; protected set; }
        public double w { get; protected set; }

        public bool IsPoint => w.IsEqual(1.0);
        public bool IsVector => w.IsEqual(0.0);

        public void SetX(double X) => x = X;

        public void SetY(double Y) => y = Y;

        public void SetZ(double Z) => z = Z;

        public void SetW(double W) => w = W;

        public static Tuple operator+(Tuple lhs, Tuple rhs)
        {
            return new Tuple(lhs.x + rhs.x,
                             lhs.y + rhs.y,
                             lhs.z + rhs.z,
                             lhs.w + rhs.w);
        }

        public static Tuple operator-(Tuple lhs, Tuple rhs)
        {
            return new Tuple(lhs.x - rhs.x,
                             lhs.y - rhs.y,
                             lhs.z - rhs.z,
                             lhs.w - rhs.w);
        }

        public static Tuple operator-(Tuple tuple)
        {
            return new Tuple(-tuple.x,
                             -tuple.y,
                             -tuple.z,
                             -tuple.w);
        }

        public static Tuple operator *(Tuple lhs, double rhs)
        {
            return new Tuple(lhs.x * rhs,
                             lhs.y * rhs,
                             lhs.z * rhs,
                             lhs.w * rhs);
        }

        public static Tuple operator/(Tuple lhs, double rhs)
        {
            return new Tuple(lhs.x / rhs,
                             lhs.y / rhs,
                             lhs.z / rhs,
                             lhs.w / rhs);
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
