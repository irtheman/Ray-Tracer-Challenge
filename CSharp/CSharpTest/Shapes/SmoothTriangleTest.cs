using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class SmoothTriangleTest
    {
        private const double epsilon = 0.00001d;

        private SmoothTriangle tri;

        [TestInitialize]
        public void Startup()
        {
            var p1 = new Point(0, 1, 0);
            var p2 = new Point(-1, 0, 0);
            var p3 = new Point(1, 0, 0);
            var n1 = new Vector(0, 1, 0);
            var n2 = new Vector(-1, 0, 0);
            var n3 = new Vector(1, 0, 0);
            tri = new SmoothTriangle(p1, p2, p3, n1, n2, n3);
        }

        [TestMethod]
        public void TestSmoothTriangleCreate()
        {
            Assert.AreEqual(tri.P1, new Point(0, 1, 0));
            Assert.AreEqual(tri.P2, new Point(-1, 0, 0));
            Assert.AreEqual(tri.P3, new Point(1, 0, 0));
            Assert.AreEqual(tri.N1, new Vector(0, 1, 0));
            Assert.AreEqual(tri.N2, new Vector(-1, 0, 0));
            Assert.AreEqual(tri.N3, new Vector(1, 0, 0));
        }

        [TestMethod]
        public void TestSmoothTriangleUandV()
        {
            var r = new Ray(new Point(-0.2, 0.3, -2), Vector.VectorZ);
            var xs = tri.Intersect(r);

            Assert.AreEqual(xs[0].U, 0.45, epsilon);
            Assert.AreEqual(xs[0].V, 0.25, epsilon);
        }

        [TestMethod]
        public void TestSmoothTriangleInterpolateNormal()
        {
            var i = new Intersection(1, tri, 0.45, 0.25);
            var n = tri.NormalAt(new Point(0, 0, 0), i);

            Assert.AreEqual(n, new Vector(-0.5547, 0.83205, 0));
        }

        [TestMethod]
        public void TestSmoothTriangleNormal()
        {
            var i = new Intersection(1, tri, 0.45, 0.25);
            var r = new Ray(new Point(-0.2, 0.3, -2), Vector.VectorZ);
            var xs = new Intersections(i);
            var comps = i.PrepareComputations(r, xs);

            Assert.AreEqual(comps.NormalVector, new Vector(-0.5547, 0.83205, 0));
        }
    }
}
