package RTC.Patterns;

import RTC.Math.*;
import RTC.Material.*;

public class CheckerPattern extends Pattern {
    private Color a, b;

    public CheckerPattern(Color A, Color B) {
        super();

        a = A;
        b = B;
    }

    public Color patternAt(Point point) {
        int value = (int) Math.floor(point.getX()) + (int) Math.floor(point.getY()) + (int) Math.floor(point.getZ());
        if (value % 2 == 0) {
            return a;
        } else {
            return b;
        }
    }
}