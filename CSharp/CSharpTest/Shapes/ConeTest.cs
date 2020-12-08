using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class ConeTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestConeRayIntersect()
        {
            var shape = new Cone();
            var rays = new List<Ray>()
            {
                new Ray(new Point(0, 0, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 0, -5), (new Vector(1, 1, 1)).Normalize),
                new Ray(new Point(1, 1, -5), (new Vector(-0.5, -1, 1)).Normalize)
            };
            var Ts = new double[,]
            {
                { 5, 5 },
                { 8.66025, 8.66025 },
                { 4.55006, 49.44994 }
            };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = shape.Intersect(r);

                Assert.AreEqual(xs.Count, 2);
                Assert.AreEqual(xs[0].t, Ts[i, 0], epsilon);
                Assert.AreEqual(xs[1].t, Ts[i, 1], epsilon);
            }
        }

        [TestMethod]
        public void TestConeIntersectHalf()
        {
            var shape = new Cone();
            var r = new Ray(new Point(0, 0, -1), (new Vector(0, 1, 1)).Normalize);
            var xs = shape.Intersect(r);

            Assert.AreEqual(xs.Count, 1);
            Assert.AreEqual(xs[0].t, 0.35355, epsilon);
        }

        [TestMethod]
        public void TestConeCappedIntersect()
        {
            var shape = new Cone();
            shape.Minimum = -0.5;
            shape.Maximum = 0.5;
            shape.Closed = true;

            var rays = new List<Ray>()
            {
                new Ray(new Point(0, 0, -5), (new Vector(0, 1, 0)).Normalize),
                new Ray(new Point(0, 0, -0.25), (new Vector(0, 1, 1)).Normalize),
                new Ray(new Point(0, 0, -0.25), (new Vector(0, 1, 0)).Normalize)
            };
            var count = new int[] { 0, 2, 4 };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = shape.Intersect(r);

                Assert.AreEqual(xs.Count, count[i], i.ToString());
            }
        }

        [TestMethod]
        public void TestConeNormal()
        {
            var shape = new Cone();
            var points = new List<Point>()
            {
                new Point(0, 0, 0),
                new Point(1, 1, 1),
                new Point(-1, -1, 0),
            };
            var normals = new List<Vector>()
            {
                (new Vector(0, 0, 0)).Normalize,
                (new Vector(1, -MathHelper.SQRT2, 1)).Normalize,
                (new Vector(-1, -1, 0)).Normalize,
            };

            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                var normal = shape.NormalAt(p);

                Assert.AreEqual(normal, normals[i], i.ToString());
            }
        }

        [TestMethod]
        public void TestConeBounds()
        {
            var shape = new Cone();
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity));
            Assert.AreEqual(box.Max, new Point(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity));
        }

        [TestMethod]
        public void TestConeBoundedBounds()
        {
            var shape = new Cone();
            shape.Minimum = -5;
            shape.Maximum = 3;
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-5, -5, -5));
            Assert.AreEqual(box.Max, new Point(5, 3, 5));
        }
    }
}
