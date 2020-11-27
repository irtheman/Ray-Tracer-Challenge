using System;

namespace CSharp
{
    public class Color
    {
        public Color(double red, double green, double blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public double red { get; protected set; }
        public double green { get; protected set; }
        public double blue { get; protected set; }

        public static Color operator+(Color lhs, Color rhs)
        {
            return new Color(lhs.red + rhs.red,
                             lhs.green + rhs.green,
                             lhs.blue + rhs.blue);
        }

        public static Color operator-(Color lhs, Color rhs)
        {
            return new Color(lhs.red - rhs.red,
                             lhs.green - rhs.green,
                             lhs.blue - rhs.blue);
        }

        public static Color operator *(Color lhs, double rhs)
        {
            return new Color(lhs.red * rhs,
                             lhs.green * rhs,
                             lhs.blue * rhs);
        }

        public static Color operator *(Color lhs, Color rhs)
        {
            return new Color(lhs.red * rhs.red,
                             lhs.green * rhs.green,
                             lhs.blue * rhs.blue);
        }

        public static Color operator/(Color lhs, double rhs)
        {
            return new Color(lhs.red / rhs,
                             lhs.green / rhs,
                             lhs.blue / rhs);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Color;
            return ((other != null) &&
                    (red.IsEqual(other.red)) &&
                    (green.IsEqual(other.green)) &&
                    (blue.IsEqual(other.blue)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(red, green, blue);
        }

        public override string ToString()
        {
            return $"({red},{green},{blue})";
        }
    }
}
