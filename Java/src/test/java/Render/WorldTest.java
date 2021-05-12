import RTC.Render.*;
import RTC.Material.*;
import RTC.Shapes.*;
import RTC.Lights.*;
import RTC.Math.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class WorldTest {
    @Test
    public void WorldCreate() {
        PointLight l = new PointLight(new Point(-10, 10, -10), Color.White);

        Sphere s1 = new Sphere();
        Material m = s1.getMaterial();
        m.setColor(new Color(0.8, 1.0, 0.6));
        m.setDiffuse(0.7);
        m.setSpecular(0.2);

        Sphere s2 = new Sphere();
        s2.setTransform(Matrix.Scaling(0.5, 0.5, 0.5));

        World w = World.getDefault();

        Assertions.assertEquals(l, w.getLight());
        Assertions.assertEquals(s1, w.getObject(0));
        Assertions.assertEquals(s2, w.getObject(1));
    }

    @Test
    public void IntersectWorldWithRay() {
        World w = World.getDefault();
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Intersections xs = w.intersectWorld(r);

        Assertions.assertEquals(4, xs.size());
        Assertions.assertEquals(4, xs.get(0).getT());
        Assertions.assertEquals(4.5, xs.get(1).getT());
        Assertions.assertEquals(5.5, xs.get(2).getT());
        Assertions.assertEquals(6, xs.get(3).getT());
    }

    @Test
    public void WorldShadingIntersection()
    {
        World w = World.getDefault();
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Shape shape = w.getObject(0);
        Intersection i = new Intersection(4, shape);
        Computations comps = i.prepareComputations(r);
        Color c = w.shadeHit(shape, comps);

        Assertions.assertEquals(c, new Color(0.38066, 0.47583, 0.2855));
    }

    @Test
    public void WorldShadingIntersectionInside()
    {
        World w = World.getDefault();
        w.setLight(new PointLight(new Point(0, 0.25, 0), Color.White));
        Ray r = new Ray(Point.Zero, Vector.VectorZ);
        Shape shape = w.getObject(1);
        Intersection i = new Intersection(0.5, shape);
        Computations comps = i.prepareComputations(r);
        Color c = w.shadeHit(shape, comps);

        Assertions.assertEquals(c, new Color(0.90498, 0.90498, 0.90498));
    }

    @Test
    public void WorldMissesColor()
    {
        World w = World.getDefault();
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorY);
        Color c = w.colorAt(r);

        Assertions.assertEquals(c, Color.Black);
    }

    @Test
    public void WorldHitsColor()
    {
        World w = World.getDefault();
        Ray r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
        Color c = w.colorAt(r);

        Assertions.assertEquals(c, new Color(0.38066, 0.47583, 0.2855));
    }

    @Test
    public void WorldHitBehindRayColor()
    {
        World w = World.getDefault();
        Shape outer = w.getObject(0);
        outer.getMaterial().setAmbient(1.0);
        Shape inner = w.getObject(1);
        inner.getMaterial().setAmbient(1.0);
        Ray r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
        Color c = w.colorAt(r);

        Assertions.assertEquals(c, inner.getMaterial().getColor());
    }
}
