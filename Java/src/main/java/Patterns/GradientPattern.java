package RTC.Patterns;

import RTC.Math.*;
import RTC.Material.*;

public class GradientPattern extends Pattern {
    private Color a, b;

    public GradientPattern(Color A, Color B) {
        super();

        a = A;
        b = B;
    }

    public Color patternAt(Point point) {
        Color distance = b.sub(a);
        double fraction = point.getX() - Math.floor(point.getX());

        return a.add(distance.mul(fraction));
    }

}
