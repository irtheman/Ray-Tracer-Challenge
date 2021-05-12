import RTC.Math.*;
import RTC.Shapes.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class IntersectionsTest {
    @Test
    public void CreateIntersections()
    {
        Sphere s = new Sphere();
        Intersection i1 = new Intersection(1, s);
        Intersection i2 = new Intersection(2, s);
        Intersections xs = new Intersections(i1, i2);

        Assertions.assertEquals(xs.size(), 2);
        Assertions.assertEquals(xs.get(0).getT(), 1);
        Assertions.assertEquals(xs.get(1).getT(), 2);
    }

    @Test
    public void HitsAllPositive()
    {
        Sphere s = new Sphere();
        Intersection i1 = new Intersection(1, s);
        Intersection i2 = new Intersection(2, s);
        Intersections xs = new Intersections(i1, i2);
        Intersection i = xs.hit();

        Assertions.assertEquals(i, i1);
    }

    @Test
    public void HitsSomePositive()
    {
        Sphere s = new Sphere();
        Intersection i1 = new Intersection(-1, s);
        Intersection i2 = new Intersection(1, s);
        Intersections xs = new Intersections(i1, i2);
        Intersection i = xs.hit();

        Assertions.assertEquals(i, i2);
    }

    @Test
    public void HitsAllNegative()
    {
        Sphere s = new Sphere();
        Intersection i1 = new Intersection(-2, s);
        Intersection i2 = new Intersection(-1, s);
        Intersections xs = new Intersections(i1, i2);
        Intersection i = xs.hit();

        Assertions.assertNull(i);
    }

    @Test
    public void HitsBestChoice()
    {
        Sphere s = new Sphere();
        Intersection i1 = new Intersection(5, s);
        Intersection i2 = new Intersection(7, s);
        Intersection i3 = new Intersection(-3, s);
        Intersection i4 = new Intersection(2, s);
        Intersections xs = new Intersections(i1, i2, i3, i4);
        Intersection i = xs.hit();

        Assertions.assertEquals(i, i4);
    }

    @Test
    public void PrepareComputation()
    {
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Sphere shape = new Sphere();
        Intersection i = new Intersection(4, shape);
        Computations comps = i.prepareComputations(r);

        Assertions.assertEquals(comps.getT(), i.getT());
        Assertions.assertEquals(comps.getObject(), i.getObject());
        Assertions.assertEquals(comps.getPoint(), new Point(0, 0, -1));
        Assertions.assertEquals(comps.getEyeVector(), new Vector(0, 0, -1));
        Assertions.assertEquals(comps.getNormalVector(), new Vector(0, 0, -1));
    }

}
