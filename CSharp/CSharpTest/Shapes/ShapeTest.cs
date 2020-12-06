using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
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
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new TestShape();

            s.Transform = Matrix.Scaling(2, 2, 2);
            var xs = s.Intersect(r);

            Assert.AreEqual(s.SavedRay.Origin, new Point(0, 0, -2.5));
            Assert.AreEqual(s.SavedRay.Direction, new Vector(0, 0, 0.5));
        }

        [TestMethod]
        public void TestShapeTranslated()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var s = new TestShape();

            s.Transform = Matrix.Translation(5, 0, 0);
            var xs = s.Intersect(r);

            // Bounding box broke this...
            //Assert.AreEqual(s.SavedRay.Origin, new Point(-5, 0, -5));
            //Assert.AreEqual(s.SavedRay.Direction, Vector.VectorZ);
            Assert.IsNull(s.SavedRay);
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

            var value = MathHelper.SQRT2 / 2.0;
            var n = s.Normal(new Point(0, value, -value));

            Assert.AreEqual(n, new Vector(0, 0.97014, -0.24254));
        }

        [TestMethod]
        public void TestShapeParent()
        {
            var s = new Sphere();
            Assert.IsNull(s.Parent);
        }

        [TestMethod]
        public void TestShapePointFromWorldToObjectSpace()
        {
            var g1 = new Group();
            g1.Transform = Matrix.RotationY(Math.PI / 2);

            var g2 = new Group();
            g2.Transform = Matrix.Scaling(2, 2, 2);
            g1.Add(g2);

            var s = new Sphere();
            s.Transform = Matrix.Translation(5, 0, 0);
            g2.Add(s);

            var p = s.WorldToObject(new Point(-2, 0, -10));

            Assert.AreEqual(p, new Point(0, 0, -1));
        }

        [TestMethod]
        public void TestShapeNormalFromObjectToWorldSpace()
        {
            var g1 = new Group();
            g1.Transform = Matrix.RotationY(Math.PI / 2);

            var g2 = new Group();
            g2.Transform = Matrix.Scaling(1, 2, 3);
            g1.Add(g2);

            var s = new Sphere();
            s.Transform = Matrix.Translation(5, 0, 0);
            g2.Add(s);

            var n = s.NormalToWorld(new Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));

            Assert.AreEqual(n, new Vector(0.2857, 0.4286, -0.8571));
        }

        [TestMethod]
        public void TestShapeNormalOfChildObject()
        {
            var g1 = new Group();
            g1.Transform = Matrix.RotationY(Math.PI / 2);

            var g2 = new Group();
            g2.Transform = Matrix.Scaling(1, 2, 3);
            g1.Add(g2);

            var s = new Sphere();
            s.Transform = Matrix.Translation(5, 0, 0);
            g2.Add(s);

            var n = s.Normal(new Point(1.7321, 1.1547, -5.5774));

            Assert.AreEqual(n, new Vector(0.2857, 0.4286, -0.8571));
        }

        [TestMethod]
        public void TestShapeBounds()
        {
            var shape = new TestShape();
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-1, -1, -1));
            Assert.AreEqual(box.Max, new Point(1, 1, 1));
        }

        [TestMethod]
        public void TestShapeParentSpace()
        {
            var shape = new Sphere();
            shape.Transform = Matrix.Translation(1, -3, 5) *
                              Matrix.Scaling(0.5, 2, 4);
            var box = shape.ParentSpaceBounds();

            Assert.AreEqual(box.Min, new Point(0.5, -5, 1));
            Assert.AreEqual(box.Max, new Point(1.5, -1, 9));
        }
    }
}
