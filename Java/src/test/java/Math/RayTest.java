import RTC.Math.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class RayTest {
    @Test
    public void RayToPoint()
    {
        Ray r = new Ray(new Point(2, 3, 4), Vector.VectorX);

        Assertions.assertEquals(r.Position(0), new Point(2, 3, 4));
        Assertions.assertEquals(r.Position(1), new Point(3, 3, 4));
        Assertions.assertEquals(r.Position(-1), new Point(1, 3, 4));
        Assertions.assertEquals(r.Position(2.5), new Point(4.5, 3, 4));
    }

    @Test
    public void TestRayTranslation()
    {
        Ray r = new Ray(new Point(1, 2, 3), Vector.VectorY);
        Matrix m = Matrix.Translation(3, 4, 5);
        Ray r2 = r.transform(m);

        Assertions.assertEquals(r2.getOrigin(), new Point(4, 6, 8));
        Assertions.assertEquals(r2.getDirection(), Vector.VectorY);
    }

    @Test
    public void TestRayScaling()
    {
        Ray r = new Ray(new Point(1, 2, 3), Vector.VectorY);
        Matrix m = Matrix.Scaling(2, 3, 4);
        Ray r2 = r.transform(m);

        Assertions.assertEquals(r2.getOrigin(), new Point(2, 6, 12));
        Assertions.assertEquals(r2.getDirection(), new Vector(0, 3, 0));
    }

}
