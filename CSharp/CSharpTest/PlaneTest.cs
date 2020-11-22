using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class PlaneTest
    {
        [TestMethod]
        public void TestPlaneNormalEverywhere()
        {
            var p = new Plane();
            var n1 = p.Normal(new Point(0, 0, 0));
            var n2 = p.Normal(new Point(10, 0, -10));
            var n3 = p.Normal(new Point(-5, 0, 150));

            var result = new Vector(0, 1, 0);
            Assert.AreEqual(n1, result);
            Assert.AreEqual(n2, result);
            Assert.AreEqual(n3, result);
        }

        [TestMethod]
        public void TestPlaneRayParallel()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, 10, 0), new Vector(0, 0, 1));
            var xs = p.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestPlaneRayCoplanar()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var xs = p.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestPlaneIntersectFromAbove()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
            var xs = p.Intersect(r);

            Assert.AreEqual(xs.Count, 1);
            Assert.AreEqual(xs[0].t, 1);
            Assert.AreEqual(xs[0].Object, p);
        }

        [TestMethod]
        public void TestPlaneIntersectFromBelow()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, -1, 0), new Vector(0, 1, 0));
            var xs = p.Intersect(r);

            Assert.AreEqual(xs.Count, 1);
            Assert.AreEqual(xs[0].t, 1);
            Assert.AreEqual(xs[0].Object, p);
        }
    }
}
