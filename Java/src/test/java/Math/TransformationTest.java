import RTC.Math.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class TransformationTest {
    @Test
    public void TranslationPoint()
    {
        Matrix transform = Matrix.Translation(5, -3, 2);
        Point p = new Point(-3, 4, 5);
        Point expected = new Point(2, 1, 7);
        Tuple actual = transform.mul(p);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void TranslationPointInverse()
    {
        Matrix transform = Matrix.Translation(5, -3, 2);
        Matrix inv = transform.inverse();
        Point p = new Point(-3, 4, 5);
        Point expected = new Point(-8, 7, 3);
        Tuple actual = inv.mul(p);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void TranslationVector()
    {
        Matrix transform = Matrix.Translation(5, -3, 2);
        Vector expected = new Vector(-3, 4, 5);
        Tuple actual = transform.mul(expected);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void ScalingPoint()
    {
        Matrix transform = Matrix.Scaling(2, 3, 4);
        Point p = new Point(-4, 6, 8);
        Point expected = new Point(-8, 18, 32);
        Tuple actual = transform.mul(p);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void ScalingVector()
    {
        Matrix transform = Matrix.Scaling(2, 3, 4);
        Vector v = new Vector(-4, 6, 8);
        Vector expected = new Vector(-8, 18, 32);
        Tuple actual = transform.mul(v);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void ScalingVectorInverse()
    {
        Matrix transform = Matrix.Scaling(2, 3, 4);
        Matrix inv = transform.inverse();
        Vector v = new Vector(-4, 6, 8);

        Assertions.assertEquals(new Vector(-2, 2, 2), inv.mul(v));
    }

    @Test
    public void ScalingReflection()
    {
        Matrix transform = Matrix.Scaling(-1, 1, 1);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(-2, 3, 4), transform.mul(p));
    }

    @Test
    public void RotationX()
    {
        Point p = Point.PointY;
        Matrix half_quarter = Matrix.RotationX(Math.PI / 4);
        Matrix full_quarter = Matrix.RotationX(Math.PI / 2);

        Assertions.assertEquals(new Point(0, Helper.SQRT2 / 2, Helper.SQRT2 / 2), half_quarter.mul(p));
        Assertions.assertEquals(Point.PointZ, full_quarter.mul(p));
    }

    @Test
    public void RotationXInverse()
    {
        Point p = Point.PointY;
        Matrix half_quarter = Matrix.RotationX(Math.PI / 4);
        Matrix inv = half_quarter.inverse();

        Assertions.assertEquals(new Point(0, Helper.SQRT2 / 2, -Helper.SQRT2 / 2), inv.mul(p));
    }

    @Test
    public void RotationY()
    {
        Point p = Point.PointZ;
        Matrix half_quarter = Matrix.RotationY(Math.PI / 4);
        Matrix full_quarter = Matrix.RotationY(Math.PI / 2);

        Assertions.assertEquals(new Point(Helper.SQRT2 / 2, 0, Helper.SQRT2 / 2), half_quarter.mul(p));
        Assertions.assertEquals(Point.PointX, full_quarter.mul(p));
    }

    @Test
    public void RotationZ()
    {
        Point p = Point.PointY;
        Matrix half_quarter = Matrix.RotationZ(Math.PI / 4);
        Matrix full_quarter = Matrix.RotationZ(Math.PI / 2);

        Assertions.assertEquals(new Point(-Helper.SQRT2 / 2, Helper.SQRT2 / 2, 0), half_quarter.mul(p));
        Assertions.assertEquals(new Point(-1, 0, 0), full_quarter.mul(p));
    }

    @Test
    public void ShearingXY()
    {
        Matrix transform = Matrix.Shearing(1, 0, 0, 0, 0, 0);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(5, 3, 4), transform.mul(p));
    }

    @Test
    public void ShearingXZ()
    {
        Matrix transform = Matrix.Shearing(0, 1, 0, 0, 0, 0);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(6, 3, 4), transform.mul(p));
    }

    @Test
    public void ShearingYX()
    {
        Matrix transform = Matrix.Shearing(0, 0, 1, 0, 0, 0);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(2, 5, 4), transform.mul(p));
    }

    @Test
    public void ShearingYZ()
    {
        Matrix transform = Matrix.Shearing(0, 0, 0, 1, 0, 0);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(2, 7, 4), transform.mul(p));
    }

    @Test
    public void ShearingZX()
    {
        Matrix transform = Matrix.Shearing(0, 0, 0, 0, 1, 0);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(2, 3, 6), transform.mul(p));
    }

    @Test
    public void ShearingZY()
    {
        Matrix transform = Matrix.Shearing(0, 0, 0, 0, 0, 1);
        Point p = new Point(2, 3, 4);

        Assertions.assertEquals(new Point(2, 3, 7), transform.mul(p));
    }

    @Test
    public void Sequence()
    {
        Point p = new Point(1, 0, 1);
        Matrix a = Matrix.RotationX(Math.PI / 2);
        Matrix b = Matrix.Scaling(5, 5, 5);
        Matrix c = Matrix.Translation(10, 5, 7);

        Tuple p2 = a.mul(p);
        Assertions.assertEquals(new Point(1, -1, 0), p2);

        Tuple p3 = b.mul(p2);
        Assertions.assertEquals(new Point(5, -5, 0), p3);

        Tuple p4 = c.mul(p3);
        Assertions.assertEquals(new Point(15, 0, 7), p4);
    }

    @Test
    public void ChainedTransformation()
    {
        Point p = new Point(1, 0, 1);
        Matrix a = Matrix.RotationX(Math.PI / 2);
        Matrix b = Matrix.Scaling(5, 5, 5);
        Matrix c = Matrix.Translation(10, 5, 7);
        Matrix t = c.mul(b).mul(a);

        Assertions.assertEquals(new Point(15, 0, 7), t.mul(p));
    }

    @Test
    public void ViewTransform()
    {
        Point from = Point.Zero;
        Point to = new Point(0, 0, -1);
        Vector up = Vector.VectorY;
        Matrix t = Matrix.ViewTransform(from, to, up);

        Assertions.assertEquals(t, Matrix.Identity());
    }

    @Test
    public void ViewTransformPositiveZ()
    {
        Point from = Point.Zero;
        Point to = Point.PointZ;
        Vector up = Vector.VectorY;
        Matrix t = Matrix.ViewTransform(from, to, up);

        Assertions.assertEquals(t, Matrix.Scaling(-1, 1, -1));
    }

    @Test
    public void ViewTransformMoveWorld()
    {
        Point from = new Point(0, 0, 8);
        Point to = Point.Zero;
        Vector up = Vector.VectorY;
        Matrix t = Matrix.ViewTransform(from, to, up);

        Assertions.assertEquals(t, Matrix.Translation(0, 0, -8));
    }

    @Test
    public void ViewTransformArbitrary()
    {
        Point from = new Point(1, 3, 2);
        Point to = new Point(4, -2, 8);
        Vector up = new Vector(1, 1, 0);
        Matrix t = Matrix.ViewTransform(from, to, up);

        Matrix expected = new Matrix(4, 4, new double[] { -0.50709, 0.50709, 0.67612, -2.36643,
                                                           0.76772, 0.60609, 0.12122, -2.82843,
                                                          -0.35857, 0.59761, -0.71714, 0.00000,
                                                           0.00000, 0.00000, 0.00000, 1.00000});
        
        Assertions.assertEquals(t, expected);
    }
}
