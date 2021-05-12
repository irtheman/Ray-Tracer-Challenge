package RTC.Math;

import java.util.Objects;

public class Ray {
    public static int RayId = 0;

    private int id;
    private Point origin;
    private Vector direction;

    public Ray(Point Origin, Vector Direction) {
        id = RayId++;

        origin = Origin;
        direction = Direction;
    }

    public Point getOrigin() {
        return origin;
    }

    public Vector getDirection() {
        return direction;
    }

    public Point Position(double t) {
        Tuple d = direction.mul(t);
        d = origin.add(d);

        return new Point(d);
    }

    public Ray transform(Matrix m) {
        Tuple o = m.mul(origin);
        Tuple d = m.mul(direction);

        return new Ray(new Point(o), new Vector(d));
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Ray)) {
            return false;
        }

        Ray r = (Ray) o;
        return origin.equals(r.getOrigin()) &&
               direction.equals(r.getDirection());
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, origin.hashCode(), direction.hashCode());
    }

    @Override
    public String toString() {
        return String.format("Ray [%s, %s] Id: %d", origin.toString(), direction.toString(), id);
    }
}
