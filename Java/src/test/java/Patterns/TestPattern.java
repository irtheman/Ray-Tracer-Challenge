import RTC.Math.*;
import RTC.Material.*;
import RTC.Shapes.*;
import RTC.Patterns.*;

public class TestPattern extends Pattern {
    public Color patternAt(Point p) {
        return new Color(p.getX(), p.getY(), p.getZ());
    }
}