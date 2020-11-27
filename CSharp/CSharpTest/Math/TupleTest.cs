using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using static System.Math;

namespace CSharpTest
{
    [TestClass]
    public class TupleTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestTupleAsPoint()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 1.0);
            Assert.AreEqual(a.x, 4.3, epsilon);
            Assert.AreEqual(a.y, -4.2, epsilon);
            Assert.AreEqual(a.z, 3.1, epsilon);
            Assert.AreEqual(a.w, 1.0, epsilon);
            Assert.IsTrue(a.IsPoint);
            Assert.IsFalse(a.IsVector);
        }

        [TestMethod]
        public void TestTupleAsVector()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);
            Assert.AreEqual(a.x, 4.3, epsilon);
            Assert.AreEqual(a.y, -4.2, epsilon);
            Assert.AreEqual(a.z, 3.1, epsilon);
            Assert.AreEqual(a.w, 0.0, epsilon);
            Assert.IsFalse(a.IsPoint);
            Assert.IsTrue(a.IsVector);
        }

        [TestMethod]
        public void TestCreatePoint()
        {
            var p = new Point(4, -4, 3);
            Assert.AreEqual(p, new Tuple(4, -4, 3, 1));
        }

        [TestMethod]
        public void TestCreateVector()
        {
            var v = new Vector(4, -4, 3);
            Assert.AreEqual(v, new Tuple(4, -4, 3, 0));
        }

        [TestMethod]
        public void TestTupleAddition()
        {
            var a1 = new Tuple(3, -2, 5, 1);
            var a2 = new Tuple(-2, 3, 1, 0);
            Assert.AreEqual(a1 + a2, new Tuple(1, 1, 6, 1));
        }

        [TestMethod]
        public void TestPointSubtraction()
        {
            var p1 = new Point(3, 2, 1);
            var p2 = new Point(5, 6, 7);
            Assert.AreEqual(p1 - p2, new Vector(-2, -4, -6));
        }

        [TestMethod]
        public void TestPointVectorSubtraction()
        {
            var p = new Point(3, 2, 1);
            var v = new Vector(5, 6, 7);
            Assert.AreEqual(p - v, new Point(-2, -4, -6));
        }

        [TestMethod]
        public void TestVectorSubtraction()
        {
            var v1 = new Vector(3, 2, 1);
            var v2 = new Vector(5, 6, 7);
            Assert.AreEqual(v1 - v2, new Vector(-2, -4, -6));
        }

        [TestMethod]
        public void TestVectorSubtractFromZero()
        {
            var zero = Vector.Zero;
            var v = new Vector(1, -2, 3);
            Assert.AreEqual(zero - v, new Vector(-1, 2, -3));
        }

        [TestMethod]
        public void TestTupleNegating()
        {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(-a, new Tuple(-1, 2, -3, 4));
        }

        [TestMethod]
        public void TestTupleMultiplyByScalar()
        {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(a * 3.5, new Tuple(3.5, -7, 10.5, -14));
        }

        [TestMethod]
        public void TestTupleMultiplyByFraction()
        {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(a * 0.5, new Tuple(0.5, -1, 1.5, -2));
        }

        [TestMethod]
        public void TestTupleDivideByScalar()
        {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(a / 2, new Tuple(0.5, -1, 1.5, -2));
        }

        [TestMethod]
        public void TestVectorMagnitude1()
        {
            var v = Vector.VectorX;
            Assert.AreEqual(v.Magnitude, 1);
        }

        [TestMethod]
        public void TestVectorMagnitude2()
        {
            var v = Vector.VectorY;
            Assert.AreEqual(v.Magnitude, 1);
        }

        [TestMethod]
        public void TestVectorMagnitude3()
        {
            var v = Vector.VectorZ;
            Assert.AreEqual(v.Magnitude, 1);
        }

        [TestMethod]
        public void TestVectorMagnitude4()
        {
            var v = new Vector(1, 2, 3);
            Assert.AreEqual(v.Magnitude, Sqrt(14));
        }

        [TestMethod]
        public void TestVectorMagnitude5()
        {
            var v = new Vector(-1, -2, -3);
            Assert.AreEqual(v.Magnitude, Sqrt(14));
        }

        [TestMethod]
        public void TestVectorNormalize1()
        {
            var v = new Vector(4, 0, 0);
            Assert.AreEqual(v.Normalize, Vector.VectorX);
        }

        [TestMethod]
        public void TestVectorNormalize2()
        {
            var v = new Vector(1, 2, 3);
            Assert.AreEqual(v.Normalize, new Vector(1 / Sqrt(14), 2 / Sqrt(14), 3 / Sqrt(14)));
        }

        [TestMethod]
        public void TestVectorMagnitudeOfNormalize()
        {
            var v = new Vector(1, 2, 3);
            var norm = v.Normalize;
            Assert.AreEqual(norm.Magnitude, 1);
        }

        [TestMethod]
        public void TestVectorDotProduct()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(2, 3, 4);
            Assert.AreEqual(a.Dot(b), 20);
        }

        [TestMethod]
        public void TestVectorCrossProduct()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(2, 3, 4);
            Assert.AreEqual(a.Cross(b), new Vector(-1, 2, -1));
            Assert.AreEqual(b.Cross(a), new Vector(1, -2, 1));
        }

        [TestMethod]
        public void TestVectorReflect45()
        {
            var v = new Vector(1, -1, 0);
            var n = Vector.VectorY;
            var r = v.Reflect(n);

            Assert.AreEqual(r, new Vector(1, 1, 0));
        }

        [TestMethod]
        public void TestVectorReflectOther()
        {
            var value = Sqrt(2) / 2.0;
            var v = new Vector(0, -1, 0);
            var n = new Vector(value, value, 0);
            var r = v.Reflect(n);

            Assert.AreEqual(r, Vector.VectorX);
        }
    }
}
