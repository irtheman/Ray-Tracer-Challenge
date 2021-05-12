package RTC.Math;

public class Helper {
    public static final double EPSILON = 0.00001;
    public static final double SQRT2 = Math.sqrt(2);

    public static boolean IsEqual(double a, double b) {
        return Double.compare(a, b) == 0 ? true : Math.abs(a - b) < EPSILON;
    }
}
