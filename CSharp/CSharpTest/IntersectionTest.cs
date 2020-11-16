using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class IntersectionTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestCreateIntersection()
        {
            var s = new Sphere();
            var i = new Intersection(3.5, s);

            Assert.AreEqual(i.t, 3.5, epsilon);
            Assert.AreEqual(i.Object, s);
        }

        [TestMethod]
        public void TestCreateIntersections()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 1, epsilon);
            Assert.AreEqual(xs[1].t, 2, epsilon);
        }

        [TestMethod]
        public void TestHitsAllPositive()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.AreEqual(i, i1);
        }

        [TestMethod]
        public void TestHitsSomePositive()
        {
            var s = new Sphere();
            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(1, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.AreEqual(i, i2);
        }

        [TestMethod]
        public void TestHitsAllNegative()
        {
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.IsNull(i);
        }

        [TestMethod]
        public void TestHitsBestChoice()
        {
            var s = new Sphere();
            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);
            var xs = new Intersections(i1, i2, i3, i4);
            var i = xs.Hit;

            Assert.AreEqual(i, i4);
        }
    }
}
