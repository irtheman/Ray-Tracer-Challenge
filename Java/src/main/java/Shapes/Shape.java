package RTC.Shapes;

import RTC.Math.*;
import RTC.Material.*;
import java.util.Objects;

public abstract class Shape {
    public static int ShapeId = 0;

    protected int id;
    protected Matrix transform;
    protected Matrix inverseTransform;
    protected Material material;

    public Shape() {
        id = ShapeId++;

        transform = Matrix.Identity();
        inverseTransform = transform;
        material = new Material();
    }

    public Matrix getTransform() {
        return transform;
    }

    public void setTransform(Matrix m) {
        transform = m;
        inverseTransform = m.inverse();
    }

    public Matrix getInverseTransform() {
        return inverseTransform;
    }

    public Material getMaterial() {
        return material;
    }

    public void setMaterial(Material m) {
        material = m;
    }

    public Vector normalAt(Point worldPoint) {
        Tuple localPoint = inverseTransform.mul(worldPoint);
        Vector localNormal = localNormalAt(new Point(localPoint));
        Tuple worldNormal = inverseTransform.transpose().mul(localNormal);
        worldNormal.setW(0.0);

        return new Vector(worldNormal.norm());
    }

    public abstract Vector localNormalAt(Point localPoint);

    public Intersections intersect(Ray r) {
        Ray localRay = r.transform(inverseTransform);
        return localIntersect(localRay);
    }

    public abstract Intersections localIntersect(Ray localRay);
}

