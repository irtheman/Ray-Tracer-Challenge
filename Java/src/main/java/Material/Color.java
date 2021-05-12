package RTC.Material;

import RTC.Math.*;
import java.util.Objects;

public class Color {
    public static final Color Black = new Color(0, 0, 0);
    public static final Color White = new Color(1, 1, 1);
    public static final Color Red = new Color(1, 0, 0);
    public static final Color Green = new Color(0, 1, 0);
    public static final Color Blue = new Color(0, 0, 1);
    public static final Color Cyan = new Color(0, 1, 1);
    public static final Color Yellow = new Color(1, 1, 0);
    public static final Color Brown = new Color(1, 0.5, 0);
    public static final Color Purple = new Color(1, 0, 1);
    public static final Color DarkGrey = new Color(0.25, 0.25, 0.25);
    public static final Color Grey = new Color(0.5, 0.5, 0.5);
    public static final Color LiteGrey = new Color(0.75, 0.75, 0.75);
    public static final Color DarkGreen = new Color(0, 0.5, 0);
    public static final Color DarkRed = new Color(0.5, 0, 0);
    public static final Color LimeGreen = new Color(199, 234, 70);
    public static final Color HotPink = new Color(255, 105, 180);

    public static int ColorId = 0;

    private int id;
    private double r, g, b;

    public Color(double R, double G, double B) {
        id = ColorId++;

        r = R;
        g = G;
        b = B;
    }

    public double getR() {
        return r;
    }

    public double getG() {
        return g;
    }

    public double getB() {
        return b;
    }
    
    public Color add(Color c) {
        return new Color(r + c.getR(),
                         g + c.getG(),
                         b + c.getB());
    }

    public Color sub(Color c) {
        return new Color(r - c.getR(),
                         g - c.getG(),
                         b - c.getB());
    }

    public Color mul(double a) {
        return new Color(r * a, g * a, b * a);
    }

    public Color mul(Color c) {
        return new Color(r * c.getR(),
                         g * c.getG(),
                         b * c.getB());
    }

    public Color div(double a) {
        return new Color(r / a, g / a, b / a);
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Color)) {
            return false;
        }

        Color t = (Color) o;
        return Helper.IsEqual(r, t.getR())
        && Helper.IsEqual(g, t.getG())
        && Helper.IsEqual(b, t.getB());
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, r, g, b);
    }

    @Override
    public String toString() {
        return String.format("Color (%f,%f,%f) Id: %d", r, g, b, id);
    }
}
