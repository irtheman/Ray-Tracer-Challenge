import RTC.Lights.*;
import RTC.Material.*;
import RTC.Shapes.*;
import RTC.Math.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class MaterialTest {
    private Material m = new Material();
    private Point position = Point.Zero;

    @Test
    public void TestMaterialLightingBehindEye() {
        Vector eyev = new Vector(0, 0, -1);
        Vector normalv = new Vector(0, 0, -1);
        PointLight light = new PointLight(new Point(0, 0, -10), Color.White);
        Color result = m.lighting(new Sphere(), light, position, eyev, normalv, false);

        Assertions.assertEquals(result, new Color(1.9, 1.9, 1.9));
    }

    @Test
    public void TestMaterialLightingBehindEyeOffset45() {
        double value = Helper.SQRT2 / 2.0;
        Vector eyev = new Vector(0, value, -value);
        Vector normalv = new Vector(0, 0, -1);
        PointLight light = new PointLight(new Point(0, 0, -10), Color.White);
        Color result = m.lighting(new Sphere(), light, position, eyev, normalv, false);

        Assertions.assertEquals(result, Color.White);
    }

    @Test
    public void TestMaterialLightingOffset45() {
        Vector eyev = new Vector(0, 0, -1);
        Vector normalv = new Vector(0, 0, -1);
        PointLight light = new PointLight(new Point(0, 10, -10), Color.White);
        Color result = m.lighting(new Sphere(), light, position, eyev, normalv, false);

        Assertions.assertEquals(result, new Color(0.7364, 0.7364, 0.7364));
    }

    @Test
    public void TestMaterialLightingEyeInlineReflection() {
        double value = Helper.SQRT2 / 2.0;
        Vector eyev = new Vector(0, -value, -value);
        Vector normalv = new Vector(0, 0, -1);
        PointLight light = new PointLight(new Point(0, 10, -10), Color.White);
        Color result = m.lighting(new Sphere(), light, position, eyev, normalv, false);

        Assertions.assertEquals(result, new Color(1.6364, 1.6364, 1.6364));
    }

    @Test
    public void TestMaterialLightingBehindSurface() {
        Vector eyev = new Vector(0, 0, -1);
        Vector normalv = new Vector(0, 0, -1);
        PointLight light = new PointLight(new Point(0, 0, 10), Color.White);
        Color result = m.lighting(new Sphere(), light, position, eyev, normalv, false);

        Assertions.assertEquals(result, new Color(0.1, 0.1, 0.1));
    }    
}
