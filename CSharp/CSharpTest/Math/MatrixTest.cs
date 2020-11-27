using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class MatrixTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestMatrixCreate4x4()
        {
            var m = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               5.5, 6.5, 7.5, 8.5,
                               9, 10, 11, 12,
                               13.5, 14.5, 15.5, 16.5 });

            Assert.AreEqual(m[0, 0], 1, epsilon);
            Assert.AreEqual(m[0, 3], 4, epsilon);
            Assert.AreEqual(m[1, 0], 5.5, epsilon);
            Assert.AreEqual(m[1, 2], 7.5, epsilon);
            Assert.AreEqual(m[2, 2], 11, epsilon);
            Assert.AreEqual(m[3, 0], 13.5, epsilon);
            Assert.AreEqual(m[3, 2], 15.5, epsilon);
        }

        [TestMethod]
        public void TestMatrixCreate2x2()
        {
            var m = new Matrix(2, 2);
            m[0, 0] = -3;
            m[0, 1] = 5;
            m[1, 0] = 1;
            m[1, 1] = -2;

            Assert.AreEqual(m[0, 0], -3, epsilon);
            Assert.AreEqual(m[0, 1], 5, epsilon);
            Assert.AreEqual(m[1, 0], 1, epsilon);
            Assert.AreEqual(m[1, 1], -2, epsilon);
        }

        [TestMethod]
        public void TestMatrixCreate3x3()
        {
            var m = new Matrix(3, 3, new double[] {
                               -3, 5, 0,
                               1, -2, -7,
                               0, 1, 1 });

            Assert.AreEqual(m[0, 0], -3, epsilon);
            Assert.AreEqual(m[1, 1], -2, epsilon);
            Assert.AreEqual(m[2, 2], 1, epsilon);
        }

        [TestMethod]
        public void TestMatrixEqualityTrue()
        {
            var a = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               5, 6, 7, 8,
                               9, 8, 7, 6,
                               5, 4, 3, 2 });

            var b = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               5, 6, 7, 8,
                               9, 8, 7, 6,
                               5, 4, 3, 2 });

            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void TestMatrixEqualityFalse()
        {
            var a = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               5, 6, 7, 8,
                               9, 8, 7, 6,
                               5, 4, 3, 2 });

            var b = new Matrix(4, 4, new double[] {
                               2, 3, 4, 5,
                               6, 7, 8, 9,
                               8, 7, 6, 5,
                               4, 3, 2, 1 });

            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void TestMatrixMultiply()
        {
            var a = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               5, 6, 7, 8,
                               9, 8, 7, 6,
                               5, 4, 3, 2 });

            var b = new Matrix(4, 4, new double[] {
                               -2, 1, 2, 3,
                               3, 2, 1, -1,
                               4, 3, 6, 5,
                               1, 2, 7, 8 });

            var result = new Matrix(4, 4, new double[] {
                                    20, 22, 50, 48,
                                    44, 54, 114, 108,
                                    40, 58, 110, 102,
                                    16, 26, 46, 42 });

            Assert.AreEqual(a * b, result);
        }

        [TestMethod]
        public void TestMatrixMultiplyTuple()
        {
            var a = new Matrix(4, 4, new double[] {
                               1, 2, 3, 4,
                               2, 4, 4, 2,
                               8, 6, 4, 1,
                               0, 0, 0, 1 });

            var b = new Tuple(1, 2, 3, 1);

            var result = new Tuple(18, 24, 33, 1);

            Assert.AreEqual(a * b, result);
        }

        [TestMethod]
        public void TestMatrixIdentity()
        {
            var a = new Matrix(4, 4, new double[] {
                               0, 1, 2, 4,
                               1, 2, 4, 8,
                               2, 4, 8, 16,
                               4, 8, 16, 32 });

            Assert.AreEqual(a * Matrix.Identity, a);
        }

        [TestMethod]
        public void TestMatrixIdentityTuple()
        {
            var a = new Tuple(1, 2, 3, 4);

            Assert.AreEqual(Matrix.Identity * a, a);
        }

        [TestMethod]
        public void TestMatrixTranspose()
        {
            var a = new Matrix(4, 4, new double[] {
                               0, 9, 3, 0,
                               9, 8, 0, 8,
                               1, 8, 5, 3,
                               0, 0, 5, 8 });

            var result = new Matrix(4, 4, new double[] {
                                    0, 9, 1, 0,
                                    9, 8, 8, 0,
                                    3, 0, 5, 5,
                                    0, 8, 3, 8 });

            Assert.AreEqual(a.Transpose, result);
        }

        [TestMethod]
        public void TestMatrixIdentityTranspose()
        {
            Assert.AreEqual(Matrix.Identity.Transpose, Matrix.Identity);
        }

        [TestMethod]
        public void TestMatrixDeterminant2x2()
        {
            var a = new Matrix(2, 2, new double[] { 1, 5,
                                                   -3, 2 });

            Assert.AreEqual(a.Determinant, 17, epsilon);
        }

        [TestMethod]
        public void TestMatrixSubmatrix3x3()
        {
            var a = new Matrix(3, 3, new double[] { 1, 5, 0,
                                                   -3, 2, 7,
                                                    0, 6, -3 });

            var result = new Matrix(2, 2, new double[] { -3, 2,
                                                          0, 6});

            Assert.AreEqual(a.Submatrix(0, 2), result);
        }

        [TestMethod]
        public void TestMatrixSubmatrix4x4()
        {
            var a = new Matrix(4, 4, new double[] { -6, 1, 1, 6,
                                                    -8, 5, 8, 6,
                                                    -1, 0, 8, 2,
                                                    -7, 1, -1, 1 });

            var result = new Matrix(3, 3, new double[] { -6, 1, 6,
                                                         -8, 8, 6,
                                                         -7, -1, 1 });

            Assert.AreEqual(a.Submatrix(2, 1), result);
        }

        [TestMethod]
        public void TestMatrixMinor3x3()
        {
            var a = new Matrix(3, 3, new double[] { 3, 5, 0,
                                                    2, -1, -7,
                                                    6, -1, 5 });

            var b = a.Submatrix(1, 0);

            Assert.AreEqual(b.Determinant, 25, epsilon);
            Assert.AreEqual(a.Minor(1, 0), 25, epsilon);
        }

        [TestMethod]
        public void TestMatrixCofactor3x3()
        {
            var a = new Matrix(3, 3, new double[] { 3, 5, 0,
                                                    2, -1, -7,
                                                    6, -1, 5 });

            Assert.AreEqual(a.Minor(0, 0), -12, epsilon);
            Assert.AreEqual(a.Cofactor(0, 0), -12, epsilon);
            Assert.AreEqual(a.Minor(1, 0), 25, epsilon);
            Assert.AreEqual(a.Cofactor(1, 0), -25, epsilon);
        }

        [TestMethod]
        public void TestMatrixDeterminant3x3()
        {
            var a = new Matrix(3, 3, new double[] { 1, 2, 6,
                                                   -5, 8, -4,
                                                    2, 6, 4});

            Assert.AreEqual(a.Cofactor(0, 0), 56, epsilon);
            Assert.AreEqual(a.Cofactor(0, 1), 12, epsilon);
            Assert.AreEqual(a.Cofactor(0, 2), -46, epsilon);
            Assert.AreEqual(a.Determinant, -196, epsilon);
        }

        [TestMethod]
        public void TestMatrixDeterminant4x4()
        {
            var a = new Matrix(4, 4, new double[] { -2, -8, 3, 5,
                                                   -3, 1, 7, 3,
                                                    1, 2, -9, 6,
                                                    -6, 7, 7, -9});

            Assert.AreEqual(a.Cofactor(0, 0), 690, epsilon);
            Assert.AreEqual(a.Cofactor(0, 1), 447, epsilon);
            Assert.AreEqual(a.Cofactor(0, 2), 210, epsilon);
            Assert.AreEqual(a.Cofactor(0, 3), 51, epsilon);
            Assert.AreEqual(a.Determinant, -4071, epsilon);
        }

        [TestMethod]
        public void TestMatrixInvertible()
        {
            var a = new Matrix(4, 4, new double[] { 6, 4, 4, 4,
                                                    5, 5, 7, 6,
                                                    4, -9, 3, -7,
                                                    9, 1, 7, -6});

            Assert.AreEqual(a.Determinant, -2120, epsilon);
            Assert.IsTrue(a.IsInvertible);
        }

        [TestMethod]
        public void TestMatrixNotInvertible()
        {
            var a = new Matrix(4, 4, new double[] { -4, 2, 2, -3,
                                                    9, 6, 2, 6,
                                                    0, -5, 1, -5,
                                                    0, 0, 0, 0});

            Assert.AreEqual(a.Determinant, 0, epsilon);
            Assert.IsFalse(a.IsInvertible);
        }

        [TestMethod]
        public void TestMatrixInverse1()
        {
            var a = new Matrix(4, 4, new double[] { -5, 2, 6, -8,
                                                    1, -5, 1, 8,
                                                    7, 7, -6, -7,
                                                    1, -3, 7, 4 });

            var b = a.Inverse;

            var result = new Matrix(4, 4, new double[] { 0.21805, 0.45113, 0.24060, -0.04511,
                                                         -0.80827, -1.45677, -0.44361, 0.52068,
                                                         -0.07895, -0.22368, -0.05263, 0.19737,
                                                         -0.52256, -0.81391, -0.30075, 0.30639 });

            Assert.AreEqual(a.Determinant, 532, epsilon);
            Assert.AreEqual(a.Cofactor(2, 3), -160, epsilon);
            Assert.AreEqual(b[3, 2], -160 / 532.0, epsilon);
            Assert.AreEqual(a.Cofactor(3, 2), 105, epsilon);
            Assert.AreEqual(b[2, 3], 105 / 532.0, epsilon);
            Assert.AreEqual(b, result);
        }

        [TestMethod]
        public void TestMatrixInverse2()
        {
            var a = new Matrix(4, 4, new double[] { 8, -5, 9, 2,
                                                    7, 5, 6, 1,
                                                    -6, 0, 9, 6,
                                                    -3, 0, -9, -4 });

            var result = new Matrix(4, 4, new double[] { -0.15385, -0.15385, -0.28205, -0.53846,
                                                         -0.07692, 0.12308, 0.02564, 0.03077,
                                                         0.35897, 0.35897, 0.43590, 0.92308,
                                                         -0.69231, -0.69231, -0.76923, -1.92308});

            Assert.AreEqual(a.Inverse, result);
        }

        [TestMethod]
        public void TestMatrixInverse3()
        {
            var a = new Matrix(4, 4, new double[] { 9, 3, 0, 9,
                                                    -5, -2, -6, -3,
                                                    -4, 9, 6, 4,
                                                    -7, 6, 6, 2 });

            var result = new Matrix(4, 4, new double[] { -0.04074, -0.07778, 0.14444, -0.22222,
                                                         -0.07778, 0.03333, 0.36667, -0.33333,
                                                         -0.02901, -0.14630, -0.10926, 0.12963,
                                                         0.17778, 0.06667, -0.26667, 0.33333 });

            Assert.AreEqual(a.Inverse, result);
        }

        [TestMethod]
        public void TestMatrixProductXInverse()
        {
            var a = new Matrix(4, 4, new double[] { 3, -9, 7, 3,
                                                    3, -8, 2, -9,
                                                    -4, 4, 4, 1,
                                                    -6, 5, -1, 1 });

            var b = new Matrix(4, 4, new double[] { 8, 2, 2, 2,
                                                    3, -1, 7, 0,
                                                    7, 0, 5, 4,
                                                    6, -2, 0, 5 });

            var c = a * b;

            Assert.AreEqual(c * b.Inverse, a);
        }
    }
}
