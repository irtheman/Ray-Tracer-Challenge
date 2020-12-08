using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class CylinderTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestCylinderRayNoIntersect()
        {
            var cyl = new Cylinder();
            var rays = new List<Ray>()
            {
                new Ray(new Point(1, 0, 0), (new Vector(0, 1, 0)).Normalize),
                new Ray(new Point(0, 0, 0), (new Vector(0, 1, 0)).Normalize),
                new Ray(new Point(0, 0, -5), (new Vector(1, 1, 1)).Normalize)
            };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = cyl.Intersect(r);

                Assert.AreEqual(xs.Count, 0);
            }
        }

        [TestMethod]
        public void TestCylinderRayIntersect()
        {
            var cyl = new Cylinder();
            var rays = new List<Ray>()
            {
                new Ray(new Point(1, 0, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 0, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0.5, 0, -5), (new Vector(0.1, 1, 1)).Normalize)
            };
            var Ts = new double[,]
            {
                { 5, 5 },
                { 4, 6 },
                { 6.80798, 7.08872 }
            };
            var counts = new int[] { 0, 2, 2 };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = cyl.Intersect(r);

                Assert.AreEqual(xs.Count, counts[i], i.ToString());
                if (counts[i] > 0)
                {
                    Assert.AreEqual(xs[0].t, Ts[i, 0], epsilon, i.ToString());
                    Assert.AreEqual(xs[1].t, Ts[i, 1], epsilon, i.ToString());
                }
            }
        }

        [TestMethod]
        public void TestCylinderNormal()
        {
            var cyl = new Cylinder();
            var points = new List<Point>()
            {
                new Point(1, 0, 0),
                new Point(0, 5, -1),
                new Point(0, -2, 1),
                new Point(-1, 1, 0),
            };
            var normals = new List<Vector>()
            {
                new Vector(1, 0, 0),
                new Vector(0, 0, -1),
                new Vector(0, 0, 1),
                new Vector(-1, 0, 0),
            };

            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                var normal = cyl.NormalAt(p);

                Assert.AreEqual(normal, normals[i]);
            }
        }

        [TestMethod]
        public void TestCylinderMinMax()
        {
            var cyl = new Cylinder();

            Assert.AreEqual(cyl.Minimum, double.NegativeInfinity);
            Assert.AreEqual(cyl.Maximum, double.PositiveInfinity);
        }

        [TestMethod]
        public void TestCylinderIntersect()
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;

            var rays = new List<Ray>()
            {
                new Ray(new Point(0, 1.5, 0), (new Vector(0.1, 1, 0)).Normalize),
                new Ray(new Point(0, 3, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 0, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 2, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 1, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(0, 1.5, -2), (new Vector(0, 0, 1)).Normalize)
            };

            var counts = new int[] { 0, 0, 0, 0, 0, 2 };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = cyl.Intersect(r);

                Assert.AreEqual(xs.Count, counts[i]);
            }
        }

        [TestMethod]
        public void TestCylinderClosed()
        {
            var cyl = new Cylinder();

            Assert.IsFalse(cyl.Closed);
        }

        [TestMethod]
        public void TestCylinderCappedIntersect()
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            cyl.Closed = true;

            var rays = new List<Ray>()
            {
                new Ray(new Point(0, 3, 0), (new Vector(0, -1, 0)).Normalize),
                new Ray(new Point(0, 3, -2), (new Vector(0, -1, 2)).Normalize),
                new Ray(new Point(0, 4, -2), (new Vector(0, -1, 1)).Normalize),
                new Ray(new Point(0, 0, -2), (new Vector(0, 1, 2)).Normalize),
                new Ray(new Point(0, -1, -2), (new Vector(0, 1, 1)).Normalize),
            };

            var counts = new int[] { 2, 2, 2, 2, 2 };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = cyl.Intersect(r);

                Assert.AreEqual(xs.Count, counts[i]);
            }
        }

        [TestMethod]
        public void TestCylinderCappedNormal()
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            cyl.Closed = true;

            var points = new List<Point>()
            {
                new Point(0, 1, 0),
                new Point(0.5, 1, 0),
                new Point(0, 1, 0.5),
                new Point(0, 2, 0),
                new Point(0.5, 2, 0),
                new Point(0, 2, 0.5)
            };
            var normals = new List<Vector>()
            {
                new Vector(0, -1, 0),
                new Vector(0, -1, 0),
                new Vector(0, -1, 0),
                new Vector(0, 1, 0),
                new Vector(0, 1, 0),
                new Vector(0, 1, 0)
            };

            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                var normal = cyl.NormalAt(p);

                Assert.AreEqual(normal, normals[i]);
            }
        }

        [TestMethod]
        public void TestCylinderBounds()
        {
            var shape = new Cylinder();
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-1, double.NegativeInfinity, -1));
            Assert.AreEqual(box.Max, new Point(1, double.PositiveInfinity, 1));
        }

        [TestMethod]
        public void TestCylinderBoundedBounds()
        {
            var shape = new Cylinder();
            shape.Minimum = -5;
            shape.Maximum = 3;
            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-1, -5, -1));
            Assert.AreEqual(box.Max, new Point(1, 3, 1));
        }
    }
}
