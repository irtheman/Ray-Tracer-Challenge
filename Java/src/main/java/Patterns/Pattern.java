package RTC.Patterns;

import RTC.Math.*;
import RTC.Material.*;
import RTC.Shapes.*;

public abstract class Pattern {
    public static int PatternID = 0;;

    protected int id;
    protected Matrix transform;
    protected Matrix inverseTransform;

    public Pattern() {
        id = PatternID++;
        transform = Matrix.Identity();
        inverseTransform = transform;
    }

    public Matrix getTransform() {
        return transform;
    }

    public void setTransform(Matrix m) {
        transform = m;
        inverseTransform = m.inverse();
    }

    public Color patternAtShape(Shape shape, Point worldPoint) {
        Tuple objectPoint = shape.getInverseTransform().mul(worldPoint);
        Tuple patternPoint = inverseTransform.mul(objectPoint);

        return patternAt(new Point(patternPoint));
    }

    public abstract Color patternAt(Point patternPoint);
}
