import RTC.Shapes.*;
import RTC.Math.*;
import RTC.Material.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class SphereTest {
    @Test
    public void RayIntersectsSphereTwoPoints() {
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Sphere s = new Sphere();
        Intersections xs = s.intersect(r);
        Assertions.assertEquals(xs.size(), 2);
        Assertions.assertEquals(xs.get(0).getT(), 4.0);
        Assertions.assertEquals(xs.get(1).getT(), 6.0);
    }

    @Test
    public void RayIntersectsSphere() {
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Sphere s = new Sphere();
        Intersections xs = s.intersect(r);
        Assertions.assertEquals(xs.size(), 2);
        Assertions.assertEquals(xs.get(0).getObject(), s);
        Assertions.assertEquals(xs.get(1).getObject(), s);
    }

    @Test
    public void SphereScaled() {
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Sphere s = new Sphere();

        s.setTransform(Matrix.Scaling(2, 2, 2));
        Intersections xs = s.intersect(r);

        Assertions.assertEquals(xs.size(), 2);
        Assertions.assertEquals(xs.get(0).getT(), 3);
        Assertions.assertEquals(xs.get(1).getT(), 7);
    }

    @Test
    public void SphereTranslated() {
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Sphere s = new Sphere();

        s.setTransform(Matrix.Translation(5, 0, 0));
        Intersections xs = s.intersect(r);

        Assertions.assertEquals(xs.size(), 0);
    }

    @Test
    public void SphereTranslatedNormal() {
        Sphere s = new Sphere();
        s.setTransform(Matrix.Translation(0, 1, 0));

        Vector n = s.normalAt(new Point(0, 1.70711, -0.70711));

        Assertions.assertEquals(new Vector(0, 0.70711, -0.70711), n);
    }

    @Test
    public void SphereTransformedNormal() {
        Sphere s = new Sphere();
        Matrix m = Matrix.Scaling(1, 0.5, 1).mul(Matrix.RotationZ(Math.PI / 5));
        s.setTransform(m);

        double value = Helper.SQRT2 / 2.0;
        Vector n = s.normalAt(new Point(0, value, -value));

        Assertions.assertEquals(new Vector(0, 0.97014, -0.24254), n);
    }

    @Test
    public void SphereAssignedMaterial() {
        Sphere s = new Sphere();
        Material m = new Material();

        Assertions.assertEquals(m, s.getMaterial());

        m.setAmbient(1.0);
        s.setMaterial(m);

        Assertions.assertEquals(m, s.getMaterial());
    }

}
