package RTC.Math;

import RTC.Shapes.*;

public class Intersection implements Comparable<Intersection> {
    private double t;
    private Shape shape;

    public Intersection(double distance, Shape obj) {
        t = distance;
        shape = obj;
    }

    public double getT() {
        return t;
    }

    public Shape getObject() {
        return shape;
    }

    public Computations prepareComputations(Ray ray) {
        Point p = ray.Position(t);
        Vector eyeV = new Vector(ray.getDirection().neg());
        Vector normV = shape.normalAt(p);
        boolean inside = false;

        if (normV.dot(eyeV) < 0.0) {
            inside = true;
            normV = new Vector(normV.neg());
        }

        return new Computations(t, shape, p, eyeV, normV, inside);
    }

    @Override
    public int compareTo(Intersection i) {
        double test = t - i.getT();
        return Helper.IsEqual(test, 0.0) ? 0 : test < 0 ? -1 : 1;
    }

    @Override
    public String toString() {
        return "Intersect " + t + " " + shape;
    }
}
