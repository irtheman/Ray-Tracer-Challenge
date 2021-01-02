using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class CubeTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestCubeRayIntersect()
        {
            var c = new Cube();
            var tests = new string[] { "+x", "-x", "+y", "-y", "+z", "-z", "inside" };
            var rays = new List<Ray>()
            {
                new Ray(new Point(5, 0.5, 0), new Vector(-1, 0, 0)),
                new Ray(new Point(-5, 0.5, 0), new Vector(1, 0, 0)),
                new Ray(new Point(0.5, 5, 0), new Vector(0, -1, 0)),
                new Ray(new Point(0.5, -5, 0), new Vector(0, 1, 0)),
                new Ray(new Point(0.5, 0, 5), new Vector(0, 0, -1)),
                new Ray(new Point(0.5, 0, -5), new Vector(0, 0, 1)),
                new Ray(new Point(0, 0.5, 0), new Vector(0, 0, 1))
            };
            var Ts = new double[,]
            {
                { 4, 6 },
                { 4, 6 },
                { 4, 6 },
                { 4, 6 },
                { 4, 6 },
                { 4, 6 },
                { -1, 1 }
            };

            for (int i = 0; i < tests.Length; i++)
            {
                var r = rays[i];
                var xs = c.Intersect(r);

                Assert.AreEqual(xs.Count, 2);
                Assert.AreEqual(xs[0].t, Ts[i, 0], epsilon, tests[i]);
                Assert.AreEqual(xs[1].t, Ts[i, 1], epsilon, tests[i]);
            }
        }

        [TestMethod]
        public void TestCubeRayNoIntersect()
        {
            var c = new Cube();
            var rays = new List<Ray>()
            {
                new Ray(new Point(-2, 0, 0), new Vector(0.2673, 0.5345, 0.8018)),
                new Ray(new Point(0, -2, 0), new Vector(0.8018, 0.2673, 0.5345)),
                new Ray(new Point(0, 0, -2), new Vector(0.5345, 0.8018, 0.2673)),
                new Ray(new Point(2, 0, 2), new Vector(0, 0, -1)),
                new Ray(new Point(0, 2, 2), new Vector(0, -1, 0)),
                new Ray(new Point(2, 2, 0), new Vector(-1, 0, 0))
            };

            for (int i = 0; i < rays.Count; i++)
            {
                var r = rays[i];
                var xs = c.Intersect(r);

                Assert.AreEqual(xs.Count, 0);
            }
        }

        [TestMethod]
        public void TestCubeNormal()
        {
            var c = new Cube();
            var points = new List<Point>()
            {
                new Point(1, 0.5, -0.8),
                new Point(-1, -0.2, 0.9),
                new Point(-0.4, 1, -0.1),
                new Point(0.3, -1, -0.7),
                new Point(-0.6, 0.3, 1),
                new Point(0.4, 0.4, -1),
                new Point(1, 1, 1),
                new Point(-1, -1, -1)
            };
            var normals = new List<Vector>()
            {
                new Vector(1, 0, 0),
                new Vector(-1, 0, 0),
                new Vector(0, 1, 0),
                new Vector(0, -1, 0),
                new Vector(0, 0, 1),
                new Vector(0, 0, -1),
                new Vector(1, 0, 0),
                new Vector(-1, 0, 0),
            };

            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                var normal = c.NormalAt(p);

                Assert.AreEqual(normal, normals[i]);
            }
        }

        [TestMethod]
        public void TestCubeBounds()
        {
            var shape = new Cube();
            var box = shape.BoundsOf;

            Assert.AreEqual(box.Min, new Point(-1, -1, -1));
            Assert.AreEqual(box.Max, new Point(1, 1, 1));
        }
    }
}
