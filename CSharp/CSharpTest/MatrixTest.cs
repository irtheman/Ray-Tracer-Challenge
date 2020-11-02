using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void TestMatrixCreate4x4()
        {
            var m = Matrix(4, 4, new double[] {
                           1, 2, 3, 4,
                           5.5, 6.5, 7.5, 8.5,
                           9, 10, 11, 12,
                           13.5, 14.5, 15.5, 16.5 });

            Assert.AreEqual(m[0, 0], 1);
            Assert.AreEqual(m[0, 3], 4);
            Assert.AreEqual(m[1, 0], 5.5);
            Assert.AreEqual(m[1, 2], 7.5);
            Assert.AreEqual(m[2, 2], 11);
            Assert.AreEqual(m[3, 0], 13.5);
            Assert.AreEqual(m[3, 2], 15.5);
        }

        [TestMethod]
        public void TestMatrixCreate2x2()
        {
            var m = Matrix(2, 2);
            m[0, 0] = -3;
            m[0, 1] = 5;
            m[1, 0] = 1;
            m[1, 1] = -2;

            Assert.AreEqual(m[0, 0], -3);
            Assert.AreEqual(m[0, 1], 5);
            Assert.AreEqual(m[1, 0], 1);
            Assert.AreEqual(m[1, 1], -2);
        }

        [TestMethod]
        public void TestMatrixCreate3x3()
        {
            var m = Matrix(4, 4, new double[] {
                           -3, 5, 0,
                           1, -2, -7,
                           0, 1, 1 });

            Assert.AreEqual(m[0, 0], -3);
            Assert.AreEqual(m[1, 1], -2);
            Assert.AreEqual(m[2, 2], 1);
        }

        [TestMethod]
        public void TestMatrixEqualityTrue()
        {
            var m1 = Matrix(4, 4, new double[] {
                            1, 2, 3, 4,
                            5, 6, 7, 8,
                            9, 8, 7, 6,
                            5, 4, 3, 2 });

            var m2 = Matrix(4, 4, new double[] {
                            1, 2, 3, 4,
                            5, 6, 7, 8,
                            9, 8, 7, 6,
                            5, 4, 3, 2 });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void TestMatrixEqualityFalse()
        {
            var m1 = Matrix(4, 4, new double[] {
                            1, 2, 3, 4,
                            5, 6, 7, 8,
                            9, 8, 7, 6,
                            5, 4, 3, 2 });

            var m2 = Matrix(4, 4, new double[] {
                            2, 3, 4, 5,
                            6, 7, 8, 9,
                            8, 7, 6, 5,
                            4, 3, 2, 1 });

            Assert.AreNotEqual(m1, m2);
        }
    }
}
