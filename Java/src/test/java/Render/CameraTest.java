import RTC.Math.*;
import RTC.Render.*;
import RTC.Material.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class CameraTest {
    private final double epsilon = 0.00001;

    @Test
    public void CameraCreate() {
        int hsize = 160;
        int vsize = 120;
        double fieldOfView = Math.PI / 2;
        Camera c = new Camera(hsize, vsize, fieldOfView);

        Assertions.assertEquals(c.getHSize(), 160);
        Assertions.assertEquals(c.getVSize(), 120);
        Assertions.assertEquals(c.getFOV(), Math.PI / 2);
    }

    @Test
    public void CameraPixelSizeHorizontal() {
        Camera c = new Camera(200, 125, Math.PI / 2);

        Assertions.assertEquals(c.getPixelSize(), 0.01, epsilon);
    }

    @Test
    public void CameraPixelSizeVertical() {
        Camera c = new Camera(125, 200, Math.PI / 2);

        Assertions.assertEquals(c.getPixelSize(), 0.01, epsilon);
    }

    @Test
    public void CameraIntersectCenter()
    {
        Camera c = new Camera(201, 101, Math.PI / 2);
        Ray r = c.rayForPixel(100, 50);

        Assertions.assertEquals(r.getOrigin(), Point.Zero);
        Assertions.assertEquals(r.getDirection(), new Vector(0, 0, -1));
    }

    @Test
    public void CameraIntersectCorner()
    {
        Camera c = new Camera(201, 101, Math.PI / 2);
        Ray r = c.rayForPixel(0, 0);

        Assertions.assertEquals(r.getOrigin(), Point.Zero);
        Assertions.assertEquals(r.getDirection(), new Vector(0.66519, 0.33259, -0.66851));
    }

    @Test
    public void CameraIntersectTransformed()
    {
        Camera c = new Camera(201, 101, Math.PI / 2);
        c.transform(Matrix.RotationY(Math.PI / 4).mul(Matrix.Translation(0, -2, 5)));
        Ray r = c.rayForPixel(100, 50);
        double value = Helper.SQRT2 / 2;

        Assertions.assertEquals(r.getOrigin(), new Point(0, 2, -5));
        Assertions.assertEquals(r.getDirection(), new Vector(value, 0, -value));
    }

    @Test
    public void CameraRenderWorld()
    {
        World w = World.getDefault();
        Camera c = new Camera(11, 11, Math.PI / 2);
        Point from = new Point(0, 0, -5);
        Point to = Point.Zero;
        Vector up = Vector.VectorY;
        c.transform(Matrix.ViewTransform(from, to, up));
        Canvas image = c.render(w);

        Assertions.assertEquals(image.pixelAt(5, 5), new Color(0.38066, 0.47583, 0.2855));
    }
}
