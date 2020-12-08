using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class TriangleTest
    {
        private const double epsilon = 0.00001d;
        
        [TestMethod]
        public void TestTriangleCreate()
        {
            var p1 = new Point(0, 1, 0);
            var p2 = new Point(-1, 0, 0);
            var p3 = new Point(1, 0, 0);
            var t = new Triangle(p1, p2, p3);

            Assert.AreEqual(t.P1, p1);
            Assert.AreEqual(t.P2, p2);
            Assert.AreEqual(t.P3, p3);
            Assert.AreEqual(t.E1, new Vector(-1, -1, 0));
            Assert.AreEqual(t.E2, new Vector(1, -1, 0));
            Assert.AreEqual(t.Normal, new Vector(0, 0, -1));
        }

        [TestMethod]
        public void TestTriangleNormal()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var n1 = t.NormalAt(new Point(0, 0.5, 0));
            var n2 = t.NormalAt(new Point(-0.5, 0.75, 0));
            var n3 = t.NormalAt(new Point(0.5, 0.25, 0));

            Assert.AreEqual(n1, t.Normal);
            Assert.AreEqual(n2, t.Normal);
            Assert.AreEqual(n2, t.Normal);
        }

        [TestMethod]
        public void TestTriangleRayParallel()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var r = new Ray(new Point(0, -1, -2), new Vector(0, 1, 0));
            var xs = t.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestTriangleRayMissesE2()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var r = new Ray(new Point(1, 1, -2), new Vector(0, 0, 1));
            var xs = t.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestTriangleRayMissesE1()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var r = new Ray(new Point(-1, 1, -2), new Vector(0, 0, 1));
            var xs = t.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestTriangleRayMissesE3()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var r = new Ray(new Point(0, -1, -2), new Vector(0, 0, 1));
            var xs = t.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestTriangleRayIntersection()
        {
            var t = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(1, 0, 0));
            var r = new Ray(new Point(0, 0.5, -2), new Vector(0, 0, 1));
            var xs = t.Intersect(r);

            Assert.AreEqual(xs.Count, 1);
            Assert.AreEqual(xs[0].t, 2);

        }
    }
}
