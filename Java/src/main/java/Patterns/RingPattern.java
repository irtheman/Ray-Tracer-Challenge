package RTC.Patterns;

import RTC.Math.*;
import RTC.Material.*;

public class RingPattern extends Pattern {
    private Color a, b;

    public RingPattern(Color A, Color B) {
        super();

        a = A;
        b = B;
    }

    public Color patternAt(Point point) {
        double value = Math.sqrt(point.getX() * point.getX() + point.getZ() * point.getZ());
        if (Math.floor(value) % 2 == 0) {
            return a;
        } else {
            return b;
        }
    }
}