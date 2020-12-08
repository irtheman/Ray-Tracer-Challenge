using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class BoundsTest
    {
        [TestMethod]
        public void TestBBCreate()
        {
            var box = new BoundingBox();

            Assert.AreEqual(box.Min, new Point(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity));
            Assert.AreEqual(box.Max, new Point(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity));
        }

        [TestMethod]
        public void TestBBCreateVolume()
        {
            var box = new BoundingBox(new Point(-1, -2, -3), new Point(3, 2, 1));

            Assert.AreEqual(box.Min, new Point(-1, -2, -3));
            Assert.AreEqual(box.Max, new Point(3, 2, 1));
        }

        [TestMethod]
        public void TestBBCreateAddedPoints()
        {
            var box = new BoundingBox();
            var p1 = new Point(-5, 2, 0);
            var p2 = new Point(7, 0, -3);
            box.Add(p1);
            box.Add(p2);

            Assert.AreEqual(box.Min, new Point(-5, 0, -3));
            Assert.AreEqual(box.Max, new Point(7, 2, 0));
        }

        [TestMethod]
        public void TestBBNested()
        {
            var box1 = new BoundingBox(new Point(-5, -2, 0), new Point(7, 4, 4));
            var box2 = new BoundingBox(new Point(8, -7, -2), new Point(14, 2, 8));
            box1.Add(box2);

            Assert.AreEqual(box1.Min, new Point(-5, -7, -2));
            Assert.AreEqual(box1.Max, new Point(14, 4, 8));
        }

        [TestMethod]
        public void TestBBContainsPoint()
        {
            var box = new BoundingBox(new Point(5, -2, 0), new Point(11, 4, 7));

            var points = new Point[]
            {
                new Point(5, -2, 0),
                new Point(11, 4, 7),
                new Point(8, 1, 3),
                new Point(3, 0, 3),
                new Point(8, -4, 3),
                new Point(8, 1, -1),
                new Point(13, 1, 3),
                new Point(8, 5, 3),
                new Point(8, 1, 8)
            };

            var results = new bool[]
            {
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
            };

            for (int i = 0; i < points.Length; i++)
            {
                var p = points[i];
                var result = box.Contains(p);

                Assert.AreEqual(result, results[i], i.ToString());
            }
        }


        [TestMethod]
        public void TestBBContainsBox()
        {
            var box = new BoundingBox(new Point(5, -2, 0), new Point(11, 4, 7));

            var mins = new Point[]
            {
                new Point(5, -2, 0),
                new Point(6, -1, 1),
                new Point(4, -3, -1),
                new Point(6, -1, 1),
            };

            var maxs = new Point[]
            {
                new Point(11, 4, 7),
                new Point(10, 3, 6),
                new Point(10, 3, 6),
                new Point(12, 5, 8),
            };

            var results = new bool[]
            {
                true,
                true,
                false,
                false,
            };

            for (int i = 0; i < mins.Length; i++)
            {
                var min = mins[i];
                var max = maxs[i];
                var box2 = new BoundingBox(min, max);

                var result = box.Contains(box2);

                Assert.AreEqual(result, results[i], i.ToString());
            }
        }

        [TestMethod]
        public void TestBBTransform()
        {
            var box = new BoundingBox(new Point(-1, -1, -1), new Point(1, 1, 1));
            var matrix = Matrix.RotationX(Math.PI / 4) *
                         Matrix.RotationY(Math.PI / 4);
            var box2 = box.Transform(matrix);

            Assert.AreEqual(box2.Min, new Point(-1.4142, -1.7071, -1.7071));
            Assert.AreEqual(box2.Max, new Point(1.4142, 1.7071, 1.7071));
        }

        [TestMethod]
        public void TestBBRayIntersection()
        {
            var box = new BoundingBox(new Point(-1, -1, -1), new Point(1, 1, 1));

            var rays = new Ray[]
            {
                new Ray(new Point(5, 0.5, 0), (new Vector(-1, 0, 0).Normalize)),
                new Ray(new Point(-5, 0.5, 0), (new Vector(1, 0, 0).Normalize)),
                new Ray(new Point(0.5, 5, 0), (new Vector(0, -1, 0).Normalize)),
                new Ray(new Point(0.5, -5, 0), (new Vector(0, 1, 0).Normalize)),
                new Ray(new Point(0.5, 0, 5), (new Vector(0, 0, -1).Normalize)),
                new Ray(new Point(0.5, 0, -5), (new Vector(0, 0, 1).Normalize)),
                new Ray(new Point(0, 0.5, 0), (new Vector(0, 0, 1).Normalize)),
                new Ray(new Point(-2, 0, 0), (new Vector(2, 4, 6).Normalize)),
                new Ray(new Point(0, -2, 0), (new Vector(6, 2, 4).Normalize)),
                new Ray(new Point(0, 0, -2), (new Vector(4, 6, 2).Normalize)),
                new Ray(new Point(2, 0, 2), (new Vector(0, 0, -1).Normalize)),
                new Ray(new Point(0, 2, 2), (new Vector(0, -1, 0).Normalize)),
                new Ray(new Point(2, 2, 0), (new Vector(-1, 0, 0).Normalize))
            };

            var results = new bool[] { true, true, true, true, true, true, true, false, false, false, false, false, false };

            for (int i = 0; i < rays.Length; i++)
            {
                var r = rays[i];
                var result = box.Intersects(r);

                Assert.AreEqual(result, results[i], i.ToString());
            }
        }

        [TestMethod]
        public void TestBBRayNonCubicIntersection()
        {
            var box = new BoundingBox(new Point(5, -2, 01), new Point(11, 4, 7));

            var rays = new Ray[]
            {
                new Ray(new Point(15, 1, 2), (new Vector(-1, 0, 0)).Normalize),
                new Ray(new Point(-5, -1, 4), (new Vector(1, 0, 0)).Normalize),
                new Ray(new Point(7, 6, 5), (new Vector(0, -1, 0)).Normalize),
                new Ray(new Point(9, -5, 6), (new Vector(0, 1, 0)).Normalize),
                new Ray(new Point(8, 2, 12), (new Vector(0, 0, -1)).Normalize),
                new Ray(new Point(6, 0, -5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(8, 1, 3.5), (new Vector(0, 0, 1)).Normalize),
                new Ray(new Point(9, -1, -8), (new Vector(2, 4, 6)).Normalize),
                new Ray(new Point(8, 3, -4), (new Vector(6, 2, 4)).Normalize),
                new Ray(new Point(9, -1, -2), (new Vector(4, 6, 2)).Normalize),
                new Ray(new Point(4, 0, 9), (new Vector(0, 0, -1)).Normalize),
                new Ray(new Point(8, 6, -1), (new Vector(0, -1, 0)).Normalize),
                new Ray(new Point(12, 5, 4), (new Vector(-1, 0, 0)).Normalize)
            };

            var results = new bool[] { true, true, true, true, true, true, true, false, false, false, false, false, false };

            for (int i = 0; i < rays.Length; i++)
            {
                var r = rays[i];
                var result = box.Intersects(r);

                Assert.AreEqual(result, results[i], i.ToString());
            }
        }

        [TestMethod]
        public void TestBBSplittingPerfectCube()
        {
            var box = new BoundingBox(new Point(-1, -4, -5), new Point(9, 6, 5));
            BoundingBox left, right;
            box.SplitBounds(out left, out right);

            Assert.AreEqual(left.Min, new Point(-1, -4, -5));
            Assert.AreEqual(left.Max, new Point(4, 6, 5));
            Assert.AreEqual(right.Min, new Point(4, -4, -5));
            Assert.AreEqual(right.Max, new Point(9, 6, 5));
        }

        [TestMethod]
        public void TestBBSplittingXWideBox()
        {
            var box = new BoundingBox(new Point(-1, -2, -3), new Point(9, 5.5, 3));
            BoundingBox left, right;
            box.SplitBounds(out left, out right);

            Assert.AreEqual(left.Min, new Point(-1, -2, -3));
            Assert.AreEqual(left.Max, new Point(4, 5.5, 3));
            Assert.AreEqual(right.Min, new Point(4, -2, -3));
            Assert.AreEqual(right.Max, new Point(9, 5.5, 3));
        }

        [TestMethod]
        public void TestBBSplittingYWideBox()
        {
            var box = new BoundingBox(new Point(-1, -2, -3), new Point(5, 8, 3));
            BoundingBox left, right;
            box.SplitBounds(out left, out right);

            Assert.AreEqual(left.Min, new Point(-1, -2, -3));
            Assert.AreEqual(left.Max, new Point(5, 3, 3));
            Assert.AreEqual(right.Min, new Point(-1, 3, -3));
            Assert.AreEqual(right.Max, new Point(5, 8, 3));
        }

        [TestMethod]
        public void TestBBSplittingZWideBox()
        {
            var box = new BoundingBox(new Point(-1, -2, -3), new Point(5, 3, 7));
            BoundingBox left, right;
            box.SplitBounds(out left, out right);

            Assert.AreEqual(left.Min, new Point(-1, -2, -3));
            Assert.AreEqual(left.Max, new Point(5, 3, 2));
            Assert.AreEqual(right.Min, new Point(-1, -2, 2));
            Assert.AreEqual(right.Max, new Point(5, 3, 7));
        }
    }
}
