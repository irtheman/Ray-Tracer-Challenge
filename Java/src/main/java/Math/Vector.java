package RTC.Math;

import RTC.Math.Tuple;

public class Vector extends Tuple {
    public static final Vector Zero = new Vector(0, 0, 0);
    public static final Vector One = new Vector(1, 1, 1);
    public static final Vector VectorX = new Vector(1, 0, 0);
    public static final Vector VectorY = new Vector(0, 1, 0);
    public static final Vector VectorZ = new Vector(0, 0, 1);

    public Vector(double x, double y, double z) {
        super(x, y, z, 0.0);
    }

    public Vector(Tuple t) {
        super(t.getX(), t.getY(), t.getZ(), 0.0);
    }
}
