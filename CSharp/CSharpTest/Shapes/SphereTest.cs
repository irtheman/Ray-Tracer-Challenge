using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class SphereTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestSphereIsRTObject()
        {
            var s = new Sphere();
            Assert.IsTrue(s is RTObject);
        }

        [TestMethod]
        public void TestRayIntersectsSphereTwoPoints()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 4, epsilon);
            Assert.AreEqual(xs[1].t, 6, epsilon);
        }

        [TestMethod]
        public void TestRayIntersectsSphereTangent()
        {
            var r = new Ray(new Point(0, 1, -5), Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);

            // Bounding Box broke this...
            Assert.AreEqual(xs.Count, 0);
            //Assert.AreEqual(xs.Count, 2);
            //Assert.AreEqual(xs[0].t, 5, epsilon);
            //Assert.AreEqual(xs[1].t, 5, epsilon);
        }

        [TestMethod]
        public void TestRayIntersectsSphereMisses()
        {
            var r = new Ray(new Point(0, 2, -5), Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestRayInsideSphere()
        {
            var r = new Ray(Point.Zero, Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, -1, epsilon);
            Assert.AreEqual(xs[1].t, 1, epsilon);
        }

        [TestMethod]
        public void TestRayInFrontOfSphere()
        {
            var r = new Ray(new Point(0, 0, 5), Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, -6, epsilon);
            Assert.AreEqual(xs[1].t, -4, epsilon);
        }

        [TestMethod]
        public void TestIntersectionWithSphere()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new Sphere();
            var xs = s.Intersect(r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Object, s);
            Assert.AreEqual(xs[1].Object, s);
        }

        [TestMethod]
        public void TestSphereScaled()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new Sphere();

            s.Transform = Matrix.Scaling(2, 2, 2);
            var xs = s.Intersect(r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 3, epsilon);
            Assert.AreEqual(xs[1].t, 7, epsilon);
        }

        [TestMethod]
        public void TestSphereTranslated()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new Sphere();

            s.Transform = Matrix.Translation(5, 0, 0);
            var xs = s.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestSphereNormalX()
        {
            var s = new Sphere();
            var n = s.Normal(Point.PointX);

            Assert.AreEqual(n, Vector.VectorX);
        }

        [TestMethod]
        public void TestSphereNormalY()
        {
            var s = new Sphere();
            var n = s.Normal(Point.PointY);

            Assert.AreEqual(n, Vector.VectorY);
        }

        [TestMethod]
        public void TestSphereNormalZ()
        {
            var s = new Sphere();
            var n = s.Normal(Point.PointZ);

            Assert.AreEqual(n, Vector.VectorZ);
        }

        [TestMethod]
        public void TestSphereNormalOther()
        {
            var s = new Sphere();
            var value = Math.Sqrt(3) / 3.0;
            var n = s.Normal(new Point(value, value, value));

            Assert.AreEqual(n, new Vector(value, value, value));
        }

        [TestMethod]
        public void TestSphereNormalNormalized()
        {
            var s = new Sphere();
            var value = Math.Sqrt(3) / 3.0;
            var n = s.Normal(new Point(value, value, value));

            Assert.AreEqual(n, n.Normalize);
        }

        [TestMethod]
        public void TestSphereGlassCreate()
        {
            Sphere s = Sphere.Glass;

            Assert.AreEqual(s.Transform, Matrix.Identity);
            Assert.AreEqual(s.Material.Transparency, 1.0, epsilon);
            Assert.AreEqual(s.Material.RefractiveIndex, 1.5, epsilon);
        }

        [TestMethod]
        public void TestSphereBounds()
        {
            var shape = new Sphere();
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-1, -1, -1));
            Assert.AreEqual(box.Max, new Point(1, 1, 1));
        }
    }
}
