import RTC.Math.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class MatrixTest {
    @Test
    public void Construct2x2() {
        Matrix m = new Matrix(2, 2, new double[] { -3, 5, 1, -2 });

        Assertions.assertEquals(-3, m.valueAt(0, 0));
        Assertions.assertEquals(5, m.valueAt(0, 1));
        Assertions.assertEquals(1, m.valueAt(1, 0));
        Assertions.assertEquals(-2, m.valueAt(1, 1));
    }

    @Test
    public void Construct3x3() {
        Matrix m = new Matrix(3, 3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });

        Assertions.assertEquals(-3, m.valueAt(0, 0));
        Assertions.assertEquals(-2, m.valueAt(1, 1));
        Assertions.assertEquals(1, m.valueAt(2, 2));
    }

    @Test
    public void Construct4x4() {
        Matrix m = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                 5.5, 6.5, 7.5, 8.5,
                                                 9, 10, 11, 12,
                                                 13.5, 14.5, 15.5, 16.5 });

        Assertions.assertEquals(1, m.valueAt(0, 0));
        Assertions.assertEquals(4, m.valueAt(0, 3));
        Assertions.assertEquals(5.5, m.valueAt(1, 0));
        Assertions.assertEquals(7.5, m.valueAt(1, 2));
        Assertions.assertEquals(11, m.valueAt(2, 2));
        Assertions.assertEquals(13.5, m.valueAt(3, 0));
        Assertions.assertEquals(15.5, m.valueAt(3, 2));
    }

    @Test
    public void MatrixEqualityEqual() {
        Matrix m1 = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                    5, 6, 7, 8,
                                                    9, 8, 7, 6,
                                                    5, 4, 3, 2 });

        Matrix m2 = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                    5, 6, 7, 8,
                                                    9, 8, 7, 6,
                                                    5, 4, 3, 2 });

        Assertions.assertEquals(m1, m2);
     }

     @Test
     public void MatrixEqualityNotEqual() {
         Matrix m1 = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                     5, 6, 7, 8,
                                                     9, 8, 7, 6,
                                                     5, 4, 3, 2 });
 
         Matrix m2 = new Matrix(4, 4, new double[] { 2, 3, 4, 5,
                                                     6, 7, 8, 9,
                                                     8, 7, 6, 5,
                                                     4, 3, 2, 1 });
 
         Assertions.assertNotEquals(m1, m2);
      }

      @Test
      public void MatrixMultiply() {
        Matrix m1 = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                    5, 6, 7, 8,
                                                    9, 8, 7, 6,
                                                    5, 4, 3, 2 });

        Matrix m2 = new Matrix(4, 4, new double[] { -2, 1, 2, 3,
                                                    3, 2, 1, -1,
                                                    4, 3, 6, 5,
                                                    1, 2, 7, 8 });

        Matrix expected = new Matrix(4, 4, new double[] { 20, 22, 50, 48,
                                                          44, 54, 114, 108,
                                                          40, 58, 110, 102,
                                                          16, 26, 46, 42 });

        Matrix actual = m1.mul(m2);
    
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixMultiplyTuple() {
        Matrix m1 = new Matrix(4, 4, new double[] { 1, 2, 3, 4,
                                                    2, 4, 4, 2,
                                                    8, 6, 4, 1,
                                                    0, 0, 0, 1 });
        Tuple b = new Tuple(1, 2, 3, 1);
        Tuple expected = new Tuple(18, 24, 33, 1);
        Tuple actual = m1.mul(b);
  
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixIdentity()
    {
        Matrix a = new Matrix(4, 4, new double[] { 0, 1, 2, 4,
                                                   1, 2, 4, 8,
                                                   2, 4, 8, 16,
                                                   4, 8, 16, 32 });
        Matrix expected = a;
        Matrix actual = a.mul(Matrix.Identity());

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixIdentityTimesTuple()
    {
        Tuple a = new Tuple(1, 2, 3, 4);
        Tuple expected = a;
        Tuple actual = Matrix.Identity().mul(a);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixTranspose() {
        Matrix a = new Matrix(4, 4, new double[] { 0, 9, 3, 0,
                                                   9, 8, 0, 8,
                                                   1, 8, 5, 3,
                                                   0, 0, 5, 8 });
        Matrix expected  = new Matrix(4, 4, new double[] { 0, 9, 1, 0,
                                                           9, 8, 8, 0,
                                                           3, 0, 5, 5,
                                                           0, 8, 3, 8 });;
        Matrix actual = a.transpose();

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixIdentityTranspose()
    {
        Matrix expected = Matrix.Identity();
        Matrix actual = Matrix.Identity().transpose();

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixDeterminant2x2()
    {
        Matrix a = new Matrix(2, 2, new double[] { 1, 5,
                                                  -3, 2 });
        double expected = 17;
        double actual = a.determinate();

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixSubmatrix3x3()
    {
        Matrix a = new Matrix(3, 3, new double[] { 1, 5, 0,
                                                  -3, 2, 7,
                                                  0, 6, -3 });
        Matrix expected = new Matrix(2, 2, new double[] { -3, 2,
                                                           0, 6});
        Matrix actual = a.submatrix(0, 2);
        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixSubmatrix4x4()
    {
        Matrix a = new Matrix(4, 4, new double[] { -6, 1, 1, 6,
                                                   -8, 5, 8, 6,
                                                   -1, 0, 8, 2,
                                                   -7, 1, -1, 1 });
        Matrix expected = new Matrix(3, 3, new double[] { -6, 1, 6,
                                                          -8, 8, 6,
                                                          -7, -1, 1 });
        Matrix actual = a.submatrix(2, 1);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MatrixMinor3x3()
    {
        Matrix a = new Matrix(3, 3, new double[] { 3, 5, 0,
                                                   2, -1, -7,
                                                   6, -1, 5 });

        Matrix b = a.submatrix(1, 0);
        double expected = 25;
        double actual1 = b.determinate();
        double actual2 = a.minor(1, 0);

        Assertions.assertEquals(expected, actual1);
        Assertions.assertEquals(expected, actual2);
    }

    @Test
    public void MatrixCofactor3x3()
    {
        Matrix a = new Matrix(3, 3, new double[] { 3, 5, 0,
                                                   2, -1, -7,
                                                   6, -1, 5 });

        Assertions.assertEquals(-12, a.minor(0, 0));
        Assertions.assertEquals(-12, a.cofactor(0, 0));
        Assertions.assertEquals(25, a.minor(1, 0));
        Assertions.assertEquals(-25, a.cofactor(1, 0));
    }

    @Test
    public void MatrixDeterminant3x3()
    {
        Matrix a = new Matrix(3, 3, new double[] { 1, 2, 6,
                                                  -5, 8, -4,
                                                   2, 6, 4});

        Assertions.assertEquals(56, a.cofactor(0, 0));
        Assertions.assertEquals(12, a.cofactor(0, 1));
        Assertions.assertEquals(-46, a.cofactor(0, 2));
        Assertions.assertEquals(-196, a.determinate());
    }

    @Test
    public void MatrixDeterminant4x4()
    {
        Matrix a = new Matrix(4, 4, new double[] { -2, -8, 3, 5,
                                                   -3, 1, 7, 3,
                                                    1, 2, -9, 6,
                                                   -6, 7, 7, -9});

        Assertions.assertEquals(690, a.cofactor(0, 0));
        Assertions.assertEquals(447, a.cofactor(0, 1));
        Assertions.assertEquals(210, a.cofactor(0, 2));
        Assertions.assertEquals(51, a.cofactor(0, 3));
        Assertions.assertEquals(-4071, a.determinate());
    }

    @Test
    public void MatrixInvertible()
    {
        Matrix a = new Matrix(4, 4, new double[] { 6, 4, 4, 4,
                                                   5, 5, 7, 6,
                                                   4, -9, 3, -7,
                                                   9, 1, 7, -6});

        Assertions.assertEquals(-2120, a.determinate());
        Assertions.assertTrue(a.isInvertible());
    }

    @Test
    public void MatrixNotInvertible()
    {
        Matrix a = new Matrix(4, 4, new double[] { -4, 2, 2, -3,
                                                    9, 6, 2, 6,
                                                    0, -5, 1, -5,
                                                    0, 0, 0, 0});

        Assertions.assertEquals(0, a.determinate());
        Assertions.assertFalse(a.isInvertible());
    }

    @Test
    public void MatrixProductXInverse()
    {
        Matrix a = new Matrix(4, 4, new double[] { 3, -9, 7, 3,
                                                   3, -8, 2, -9,
                                                  -4, 4, 4, 1,
                                                  -6, 5, -1, 1 });

        Matrix b = new Matrix(4, 4, new double[] { 8, 2, 2, 2,
                                                   3, -1, 7, 0,
                                                   7, 0, 5, 4,
                                                   6, -2, 0, 5 });

        Matrix c = a.mul(b);
        Matrix expected = a;
        Matrix actual = c.mul(b.inverse());

        Assertions.assertEquals(expected, actual);
    }

}
