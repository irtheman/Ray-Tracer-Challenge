package RTC.Math;

import RTC.Math.Tuple;

public class Point extends Tuple {
    public static final Point Zero = new Point(0, 0, 0);
    public static final Point One = new Point(1, 1, 1);
    public static final Point PointX = new Point(1, 0, 0);
    public static final Point PointY = new Point(0, 1, 0);
    public static final Point PointZ = new Point(0, 0, 1);

    public Point(double x, double y, double z) {
        super(x, y, z, 1.0);
    }

    public Point(Tuple t) {
        super(t.getX(), t.getY(), t.getZ(), 1.0);
    }
}
