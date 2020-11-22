using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    public class TestShape : RTObject
    {
        public Ray SavedRay { get; set; }

        protected override Intersections LocalIntersect(Ray ray)
        {
            SavedRay = ray;

            var result = new Intersections();
            return result;

        }

        protected override Vector LocalNormal(Point p)
        {
            return new Vector(p.x, p.y, p.z);
        }
    }

    [TestClass]
    public class ShapeTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestShapeDefaultTransformation()
        {
            var s = new TestShape();

            Assert.AreEqual(s.Transform, Matrix.Identity);
        }

        [TestMethod]
        public void TestShapeDefaultMaterial()
        {
            var s = new TestShape();
            var m = s.Material;

            Assert.AreEqual(m, new Material());
        }

        [TestMethod]
        public void TestShapeCustomMaterial()
        {
            var s = new TestShape();
            var m = new Material();
            m.Ambient = 1;
            s.Material = m;

            Assert.AreEqual(s.Material, m);
        }

        [TestMethod]
        public void TestShapeTransformationChanged()
        {
            var s = new TestShape();
            var t = Matrix.Translation(2, 3, 4);

            s.Transform = t;
            Assert.AreEqual(s.Transform, t);
        }

        [TestMethod]
        public void TestShapeScaled()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new TestShape();

            s.Transform = Matrix.Scaling(2, 2, 2);
            var xs = s.Intersect(r);

            Assert.AreEqual(s.SavedRay.Origin, new Point(0, 0, -2.5));
            Assert.AreEqual(s.SavedRay.Direction, new Vector(0, 0, 0.5));
        }

        [TestMethod]
        public void TestShapeTranslated()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new TestShape();

            s.Transform = Matrix.Translation(5, 0, 0);
            var xs = s.Intersect(r);

            Assert.AreEqual(s.SavedRay.Origin, new Point(-5, 0, -5));
            Assert.AreEqual(s.SavedRay.Direction, new Vector(0, 0, 1));
        }

        [TestMethod]
        public void TestShapeTranslatedNormal()
        {
            var s = new TestShape();
            s.Transform = Matrix.Translation(0, 1, 0);

            var n = s.Normal(new Point(0, 1.70711, -0.70711));

            Assert.AreEqual(n, new Vector(0, 0.70711, -0.70711));
        }

        [TestMethod]
        public void TestShapeTransformedNormal()
        {
            var s = new TestShape();
            var m = Matrix.Scaling(1, 0.5, 1) * Matrix.RotationZ(Math.PI / 5);
            s.Transform = m;

            var value = Math.Sqrt(2) / 2.0;
            var n = s.Normal(new Point(0, value, -value));

            Assert.AreEqual(n, new Vector(0, 0.97014, -0.24254));
        }
    }
}
