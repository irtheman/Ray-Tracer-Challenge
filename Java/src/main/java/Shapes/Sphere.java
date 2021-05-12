package RTC.Shapes;

import RTC.Math.*;
import RTC.Material.*;
import java.util.Objects;

public class Sphere extends Shape {
    public Sphere() {
        super();
    }

    public Vector localNormalAt(Point localPoint) {
        return new Vector(localPoint.sub(Point.Zero));
    }

    public Intersections localIntersect(Ray r) {
        Intersections result = new Intersections();
        Tuple sphereToRay = r.getOrigin().sub(Point.Zero);
        Vector dir = r.getDirection();
        double a = dir.dot(dir);
        double b = 2.0 * dir.dot(sphereToRay);
        double c = sphereToRay.dot(sphereToRay) - 1.0;

        double discriminant = (b * b) - 4.0 * a * c;

        if (discriminant >= 0.0) {
            result.add(new Intersection((-b - Math.sqrt(discriminant)) / (2 * a), this));
            result.add(new Intersection((-b + Math.sqrt(discriminant)) / (2 * a), this));
        }

        return result;
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Sphere)) {
            return false;
        }

        Sphere r = (Sphere) o;
        return true;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public String toString() {
        return String.format("Sphere Id: %d", id);
    }
}

