using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class TransformationTest
    {
        [TestMethod]
        public void TestTranslationPoint()
        {
            var transform = Matrix.Translation(5, -3, 2);
            var p = new Point(-3, 4, 5);

            Assert.AreEqual(new Point(2, 1, 7), transform * p);
        }

        [TestMethod]
        public void TestTranslationPointInverse()
        {
            var transform = Matrix.Translation(5, -3, 2);
            var inv = transform.Inverse;
            var p = new Point(-3, 4, 5);

            Assert.AreEqual(new Point(-8, 7, 3), inv * p);
        }

        [TestMethod]
        public void TestTranslationVector()
        {
            var transform = Matrix.Translation(5, -3, 2);
            var v = new Vector(-3, 4, 5);

            Assert.AreEqual(v, transform * v);
        }

        [TestMethod]
        public void TestScalingPoint()
        {
            var transform = Matrix.Scaling(2, 3, 4);
            var p = new Point(-4, 6, 8);

            Assert.AreEqual(new Point(-8, 18, 32), transform * p);
        }

        [TestMethod]
        public void TestScalingVector()
        {
            var transform = Matrix.Scaling(2, 3, 4);
            var v = new Vector(-4, 6, 8);

            Assert.AreEqual(new Vector(-8, 18, 32), transform * v);
        }

        [TestMethod]
        public void TestScalingVectorInverse()
        {
            var transform = Matrix.Scaling(2, 3, 4);
            var inv = transform.Inverse;
            var v = new Vector(-4, 6, 8);

            Assert.AreEqual(new Vector(-2, 2, 2), inv * v);
        }

        [TestMethod]
        public void TestScalingReflection()
        {
            var transform = Matrix.Scaling(-1, 1, 1);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(-2, 3, 4), transform * p);
        }

        [TestMethod]
        public void TestRotationX()
        {
            var p = new Point(0, 1, 0);
            var half_quarter = Matrix.RotationX(Math.PI / 4);
            var full_quarter = Matrix.RotationX(Math.PI / 2);

            Assert.AreEqual(new Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2), half_quarter * p);
            Assert.AreEqual(new Point(0, 0, 1), full_quarter * p);
        }

        [TestMethod]
        public void TestRotationXInverse()
        {
            var p = new Point(0, 1, 0);
            var half_quarter = Matrix.RotationX(Math.PI / 4);
            var inv = half_quarter.Inverse;

            Assert.AreEqual(new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2), inv * p);
        }

        [TestMethod]
        public void TestRotationY()
        {
            var p = new Point(0, 0, 1);
            var half_quarter = Matrix.RotationY(Math.PI / 4);
            var full_quarter = Matrix.RotationY(Math.PI / 2);

            Assert.AreEqual(new Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2), half_quarter * p);
            Assert.AreEqual(new Point(1, 0, 0), full_quarter * p);
        }

        [TestMethod]
        public void TestRotationZ()
        {
            var p = new Point(0, 1, 0);
            var half_quarter = Matrix.RotationZ(Math.PI / 4);
            var full_quarter = Matrix.RotationZ(Math.PI / 2);

            Assert.AreEqual(new Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0), half_quarter * p);
            Assert.AreEqual(new Point(-1, 0, 0), full_quarter * p);
        }

        [TestMethod]
        public void TestShearingXY()
        {
            var transform = Matrix.Shearing(1, 0, 0, 0, 0, 0);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(5, 3, 4), transform * p);
        }

        [TestMethod]
        public void TestShearingXZ()
        {
            var transform = Matrix.Shearing(0, 1, 0, 0, 0, 0);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(6, 3, 4), transform * p);
        }

        [TestMethod]
        public void TestShearingYX()
        {
            var transform = Matrix.Shearing(0, 0, 1, 0, 0, 0);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(2, 5, 4), transform * p);
        }

        [TestMethod]
        public void TestShearingYZ()
        {
            var transform = Matrix.Shearing(0, 0, 0, 1, 0, 0);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(2, 7, 4), transform * p);
        }

        [TestMethod]
        public void TestShearingZX()
        {
            var transform = Matrix.Shearing(0, 0, 0, 0, 1, 0);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(2, 3, 6), transform * p);
        }

        [TestMethod]
        public void TestShearingZY()
        {
            var transform = Matrix.Shearing(0, 0, 0, 0, 0, 1);
            var p = new Point(2, 3, 4);

            Assert.AreEqual(new Point(2, 3, 7), transform * p);
        }

        [TestMethod]
        public void TestSequence()
        {
            var p = new Point(1, 0, 1);
            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scaling(5, 5, 5);
            var c = Matrix.Translation(10, 5, 7);

            var p2 = a * p;
            Assert.AreEqual(new Point(1, -1, 0), p2);

            var p3 = b * p2;
            Assert.AreEqual(new Point(5, -5, 0), p3);

            var p4 = c * p3;
            Assert.AreEqual(new Point(15, 0, 7), p4);
        }

        [TestMethod]
        public void TestChainedTransformation()
        {
            var p = new Point(1, 0, 1);
            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scaling(5, 5, 5);
            var c = Matrix.Translation(10, 5, 7);
            var t = c * b * a;

            Assert.AreEqual(new Point(15, 0, 7), t * p);
        }
    }
}
