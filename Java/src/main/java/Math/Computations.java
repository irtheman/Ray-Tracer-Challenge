package RTC.Math;

import RTC.Shapes.*;

public class Computations {
    private double t;
    private Shape object;
    private Point point;
    private Point overPoint;
    private Vector eyeVector;
    private Vector normVector;
    private boolean inside;

    public Computations(double T, Shape obj, Point p, Vector eyeV, Vector normV, boolean isInside) {
        t = T;
        object = obj;
        point = p;
        eyeVector = eyeV;
        normVector = normV;
        inside = isInside;

        overPoint = new Point(point.add(normVector.mul(Helper.EPSILON)));
    }

    public double getT() {
        return t;
    }

    public Shape getObject() {
        return object;
    }

    public Point getPoint() {
        return point;
    }

    public Point getOverPoint() {
        return overPoint;
    }

    public Vector getEyeVector() {
        return eyeVector;
    }

    public Vector getNormalVector() {
        return normVector;
    }

    public boolean IsInside() {
        return inside;
    }
}
