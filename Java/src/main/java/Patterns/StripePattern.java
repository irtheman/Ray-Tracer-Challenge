package RTC.Patterns;

import RTC.Math.*;
import RTC.Material.*;

public class StripePattern extends Pattern {
    private Color a, b;

    public StripePattern(Color A, Color B) {
        super();

        a = A;
        b = B;
    }

    public Color patternAt(Point point) {
        if (Math.floor(point.getX()) % 2 == 0) {
            return a;
        } else {
            return b;
        }
    }
}