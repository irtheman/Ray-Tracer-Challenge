import RTC.Math.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class TupleTest 
{
    @Test
    public void TupleIsPoint()
    {
        Tuple a = new Tuple(4.3, -4.2, 3.1, 1.0);

        Assertions.assertEquals(4.3, a.getX());
        Assertions.assertEquals(-4.2, a.getY());
        Assertions.assertEquals(3.1, a.getZ());
        Assertions.assertEquals(1.0, a.getW());
        Assertions.assertTrue(a.isPoint());
    }

    @Test
    public void TupleIsVector()
    {
        Tuple a = new Tuple(4.3, -4.2, 3.1, 0.0);

        Assertions.assertEquals(4.3, a.getX());
        Assertions.assertEquals(-4.2, a.getY());
        Assertions.assertEquals(3.1, a.getZ());
        Assertions.assertEquals(0.0, a.getW());
        Assertions.assertTrue(a.isVector());
    }

    @Test
    public void CreatePoint()
    {
        Point p = new Point(4, -4, 3);

        Assertions.assertTrue(p instanceof Tuple);
        Assertions.assertEquals(4, p.getX());
        Assertions.assertEquals(-4, p.getY());
        Assertions.assertEquals(3, p.getZ());
        Assertions.assertEquals(1.0, p.getW());
        Assertions.assertTrue(p.isPoint());
    }

    @Test
    public void CreateVector()
    {
        Tuple v = new Vector(4, -4, 3);

        Assertions.assertTrue(v instanceof Tuple);
        Assertions.assertEquals(4, v.getX());
        Assertions.assertEquals(-4, v.getY());
        Assertions.assertEquals(3, v.getZ());
        Assertions.assertEquals(0, v.getW());
        Assertions.assertTrue(v.isVector());
    }

    @Test
    public void AddingTwoTuples()
    {
        Tuple a1 = new Tuple(3, -2, 5, 1);
        Tuple a2 = new Tuple(-2, 3, 1, 0);
        Tuple expected = new Tuple(1, 1, 6, 1);
        Tuple actual = a1.add(a2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void SubtractingTwoPoints()
    {
        Point p1 = new Point(3, 2, 1);
        Point p2 = new Point(5, 6, 7);
        Vector expected = new Vector(-2, -4, -6);
        Tuple actual = p1.sub(p2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void SubtractingVectorFromPoint()
    {
        Point p = new Point(3, 2, 1);
        Vector v = new Vector(5, 6, 7);
        Point expected = new Point(-2, -4, -6);
        Tuple actual = p.sub(v);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void SubtractingTwoVectors()
    {
        Vector v1 = new Vector(3,2, 1);
        Vector v2 = new Vector(5, 6, 7);
        Vector expected = new Vector(-2, -4, -6);
        Tuple actual = v1.sub(v2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void SubtractVectorFromZero()
    {
        Vector zero = Vector.Zero;
        Vector v = new Vector(1, -2, 3);
        Vector expected = new Vector(-1, 2, -3);
        Tuple actual = zero.sub(v);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void NegateTuple() {
        Tuple a = new Tuple(1, -2, 3, -4);
        Tuple expected = new Tuple(-1, 2, -3, 4);
        Tuple actual = a.neg();

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MultiplyTupleByScalar() {
        Tuple a = new Tuple(1, -2, 3, -4);
        Tuple expected = new Tuple(3.5, -7, 10.5, -14);
        Tuple actual = a.mul(3.5);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MultiplyTupleByFraction() {
        Tuple a = new Tuple(1, -2, 3, -4);
        Tuple expected = new Tuple(0.5, -1, 1.5, -2);
        Tuple actual = a.mul(0.5);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void DivideTupleByScalar() {
        Tuple a = new Tuple(1, -2, 3, -4);
        Tuple expected = new Tuple(0.5, -1, 1.5, -2);
        Tuple actual = a.div(2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MagnitudeVectorX() {
        double expected = 1;
        double actual = Vector.VectorX.mag();

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MagnitudeNegVector() {
        Vector v = new Vector(-1, -2, -3);
        double expected = Math.sqrt(14);
        double actual = v.mag();
        
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void NormalizeVector() {
        Vector v = new Vector(1, 2, 3);
        double a = Math.sqrt(14);
        Tuple expected = new Vector(1/a, 2/a, 3/a);
        Tuple actual = v.norm();
        
        Assertions.assertEquals(expected, actual);
        Assertions.assertEquals(1, actual.mag());
    }

    @Test
    public void DotProduct() {
        Vector a = new Vector(1, 2, 3);
        Vector b = new Vector(2, 3, 4);
        double expected = 20;
        double actual = a.dot(b);
        
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void CrossProduct() {
        Vector a = new Vector(1, 2, 3);
        Vector b = new Vector(2, 3, 4);

        Tuple expected = new Vector(-1, 2, -1);
        Tuple actual = a.cross(b);
        Assertions.assertEquals(expected, actual);

        expected = new Vector(1, -2, 1);
        actual = b.cross(a);
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void TestVectorReflect45()
    {
        Vector v = new Vector(1, -1, 0);
        Vector n = Vector.VectorY;
        Vector r = new Vector(v.reflect(n));

        Assertions.assertEquals(new Vector(1, 1, 0), r);
    }

    @Test
    public void TestVectorReflectOther()
    {
        double value = Helper.SQRT2 / 2.0;
        Vector v = new Vector(0, -1, 0);
        Vector n = new Vector(value, value, 0);
        Vector r = new Vector(v.reflect(n));

        Assertions.assertEquals(Vector.VectorX, r);
    }
}
