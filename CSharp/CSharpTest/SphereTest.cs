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

        [TestMethod]
        public void TestSphereNormalX()
        {
            var s = new Sphere();
            var n = s.Normal(new Point(1, 0, 0));

            Assert.AreEqual(n, new Vector(1, 0, 0));
        }

        [TestMethod]
        public void TestSphereNormalY()
        {
            var s = new Sphere();
            var n = s.Normal(new Point(0, 1, 0));

            Assert.AreEqual(n, new Vector(0, 1, 0));
        }

        [TestMethod]
        public void TestSphereNormalZ()
        {
            var s = new Sphere();
            var n = s.Normal(new Point(0, 0, 1));

            Assert.AreEqual(n, new Vector(0, 0, 1));
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
        public void TestSphereTranslatedNormal()
        {
            var s = new Sphere();
            s.Transform = Matrix.Translation(0, 1, 0);

            var n = s.Normal(new Point(0, 1.70711, -0.70711));

            Assert.AreEqual(n, new Vector(0, 0.70711, -0.70711));
        }

        [TestMethod]
        public void TestSphereTransformedNormal()
        {
            var s = new Sphere();
            var m = Matrix.Scaling(1, 0.5, 1) * Matrix.RotationZ(Math.PI / 5);
            s.Transform = m;

            var value = Math.Sqrt(2) / 2.0;
            var n = s.Normal(new Point(0, value, -value));

            Assert.AreEqual(n, new Vector(0, 0.97014, -0.24254));
        }

        [TestMethod]
        public void TestSphereDefaultMaterial()
        {
            var s = new Sphere();
            var m = s.Material;

            Assert.AreEqual(m, new Material());
        }

        [TestMethod]
        public void TestSphereCustomMaterial()
        {
            var s = new Sphere();
            var m = new Material();
            m.Ambient = 1;
            s.Material = m;

            Assert.AreEqual(s.Material, m);
        }
    }
}
