using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class SphereTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestRayIntersectsSphereTwoPoints()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 4, epsilon);
            Assert.AreEqual(xs[1].t, 6, epsilon);
        }

        [TestMethod]
        public void TestRayIntersectsSphereTangent()
        {
            var r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 5, epsilon);
            Assert.AreEqual(xs[1].t, 5, epsilon);
        }

        [TestMethod]
        public void TestRayIntersectsSphereMisses()
        {
            var r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestRayInsideSphere()
        {
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, -1, epsilon);
            Assert.AreEqual(xs[1].t, 1, epsilon);
        }

        [TestMethod]
        public void TestRayInFrontOfSphere()
        {
            var r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, -6, epsilon);
            Assert.AreEqual(xs[1].t, -4, epsilon);
        }

        [TestMethod]
        public void TestIntersectionWithSphere()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Object, s);
            Assert.AreEqual(xs[1].Object, s);
        }

        [TestMethod]
        public void TestSphereDefaultTransformation()
        {
            var s = new Sphere();
            Assert.AreEqual(s.Transform, Matrix.Identity);
        }

        [TestMethod]
        public void TestSphereTransformationChanged()
        {
            var s = new Sphere();
            var t = Matrix.Translation(2, 3, 4);

            s.Transform = t;
            Assert.AreEqual(s.Transform, t);
        }

        [TestMethod]
        public void TestSphereScaled()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
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
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();

            s.Transform = Matrix.Translation(5, 0, 0);
            var xs = s.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }
    }
}
