﻿using System;

namespace CSharp
{
    public class Color
    {
        public static readonly Color White = new Color(1, 1, 1);
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color Red = new Color(1, 0, 0);
        public static readonly Color Green = new Color(0, 1, 0);
        public static readonly Color Blue = new Color(0, 0, 1);
        public static readonly Color Cyan = new Color(0, 1, 1);
        public static readonly Color Yellow = new Color(1, 1, 0);
        public static readonly Color Brown = new Color(1, 0.5, 0);
        public static readonly Color Purple = new Color(1, 0, 1);
        public static readonly Color DarkGrey = new Color(0.25, 0.25, 0.25);
        public static readonly Color Grey = new Color(0.5, 0.5, 0.5);
        public static readonly Color LiteGrey = new Color(0.75, 0.75, 0.75);
        public static readonly Color DarkGreen = new Color(0, 0.5, 0);
        public static readonly Color DarkRed = new Color(0.5, 0, 0);
        public static readonly Color LimeGreen = new Color(199, 234, 70);
        public static readonly Color HotPink = new Color(255, 105, 180);

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
