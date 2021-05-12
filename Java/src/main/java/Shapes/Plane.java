package RTC.Shapes;

import RTC.Math.*;
import RTC.Shapes.Shape;
import RTC.Material.*;
import java.util.Objects;

public class Plane extends Shape {
    public Plane() {
        super();
    }

    public Vector localNormalAt(Point localPoint) {
        return Vector.VectorY;
    }

    public Intersections localIntersect(Ray r) {
        Intersections result = new Intersections();

        if (Math.abs(r.getDirection().getY()) < Helper.EPSILON) {
            return result;
        }

        double t = -r.getOrigin().getY() / r.getDirection().getY();
        result.add(new Intersection(t, this));

        return result;
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Plane)) {
            return false;
        }

        Plane r = (Plane) o;
        return true;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public String toString() {
        return String.format("Plane Id: %d", id);
    }
}
