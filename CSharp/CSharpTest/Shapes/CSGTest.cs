using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class CSGTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestCsgCreate()
        {
            var s1 = new Sphere();
            var s2 = new Cube();
            var c = new CSG(CsgOperation.Union, s1, s2);
            
            Assert.AreEqual(c.Operation, CsgOperation.Union);
            Assert.AreEqual(c.Left, s1);
            Assert.AreEqual(c.Right, s2);
            Assert.AreEqual(s1.Parent, c);
            Assert.AreEqual(s2.Parent, c);
        }

        [TestMethod]
        public void TestCsgRules()
        {
            var op = new CsgOperation[]
            {
                CsgOperation.Union, CsgOperation.Union, CsgOperation.Union, CsgOperation.Union, CsgOperation.Union, CsgOperation.Union, CsgOperation.Union, CsgOperation.Union,
                CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection, CsgOperation.Intersection,
                CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference, CsgOperation.Difference
            };
            var lhit = new bool[]
            {
                true, true, true, true, false, false, false, false,
                true, true, true, true, false, false, false, false,
                true, true, true, true, false, false, false, false
            };
            var inl = new bool[]
            {
                true, true, false, false, true, true, false, false,
                true, true, false, false, true, true, false, false,
                true, true, false, false, true, true, false, false
            };
            var inr = new bool[]
            {
                true, false, true, false, true, false, true, false,
                true, false, true, false, true, false, true, false,
                true, false, true, false, true, false, true, false
            };
            var result = new bool[]
            {
                false, true, false, true, false, false, true, true,
                true, false, true, false, true, true, false, false,
                false, true, false, true, true, true, false, false
            };

            for (int i = 0; i < op.Length; i++)
            {
                Assert.AreEqual(result[i], CSG.IntersectionAllowed(op[i], lhit[i], inl[i], inr[i]));
            }
        }

        [TestMethod]
        public void TestCsgFilteringIntersections()
        {
            var s1 = new Sphere();
            var s2 = new Cube();

            var i1 = new Intersection(1, s1);
            var i2 = new Intersection(2, s2);
            var i3 = new Intersection(3, s1);
            var i4 = new Intersection(4, s2);
            var xs = new Intersections(i1, i2, i3, i4);

            var op = new CsgOperation[] { CsgOperation.Union, CsgOperation.Intersection, CsgOperation.Difference };
            var x0 = new int[] { 0, 1, 0 };
            var x1 = new int[] { 3, 2, 1 };

            for (int i = 0; i < op.Length; i++)
            {
                var c = new CSG(op[i], s1, s2);
                Intersections result = c.FilterIntersections(xs);

                Assert.AreEqual(result.Count, 2, i.ToString());
                Assert.AreEqual(result[0], xs[x0[i]], i.ToString());
                Assert.AreEqual(result[1], xs[x1[i]], i.ToString());
            }
        }

        [TestMethod]
        public void TestCsgRayMisses()
        {
            var c = new CSG(CsgOperation.Union, new Sphere(), new Cube());
            var r = new Ray(new Point(0, 2, -5), Vector.VectorZ);
            var xs = c.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestCsgRayHits()
        {
            var s1 = new Sphere();
            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(0, 0, 0.5);
            var c = new CSG(CsgOperation.Union, s1, s2);
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var xs = c.Intersect(r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 4, epsilon);
            Assert.AreEqual(xs[0].Object, s1);
            Assert.AreEqual(xs[1].t, 6.5, epsilon);
            Assert.AreEqual(xs[1].Object, s2);
        }

        [TestMethod]
        public void TestCsgBBWithChildren()
        {
            var left = new Sphere();
            var right = new Sphere();
            right.Transform = Matrix.Translation(2, 3, 4);

            var shape = new CSG(CsgOperation.Difference, left, right);
            var box = shape.BoundsOf;

            Assert.AreEqual(box.Min, new Point(-1, -1, -1));
            Assert.AreEqual(box.Max, new Point(3, 4, 5));
        }

        [TestMethod]
        public void TestCsgBBIntersectMiss()
        {
            var left = new TestShape();
            var right = new TestShape();
            var shape = new CSG(CsgOperation.Difference, left, right);
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
            var xs = shape.Intersect(r);

            Assert.IsNull(left.SavedRay);
            Assert.IsNull(right.SavedRay);
        }

        [TestMethod]
        public void TestCsgBBIntersectHit()
        {
            var left = new TestShape();
            var right = new TestShape();
            var shape = new CSG(CsgOperation.Difference, left, right);
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = shape.Intersect(r);

            Assert.IsNotNull(left.SavedRay);
            Assert.IsNotNull(right.SavedRay);
        }

        [TestMethod]
        public void TestCsgSubdividingChildren()
        {
            var left = new Group();

            var s1 = new Sphere();
            s1.Transform = Matrix.Translation(-1.5, 0, 0);
            left.Add(s1);

            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(1.5, 0, 0);
            left.Add(s2);

            var right = new Group();

            var s3 = new Sphere();
            s3.Transform = Matrix.Translation(0, 0, -1.5);
            right.Add(s3);

            var s4 = new Sphere();
            s4.Transform = Matrix.Translation(0, 0, 1.5);
            right.Add(s4);

            var shape = new CSG(CsgOperation.Difference, left, right);

            shape.Divide(1);

            Group group;

            group = left[0] as Group;
            Assert.IsNotNull(group);
            Assert.AreEqual(group[0], s1);

            group = left[1] as Group;
            Assert.IsNotNull(group);
            Assert.AreEqual(group[0], s2);

            group = right[0] as Group;
            Assert.IsNotNull(group);
            Assert.AreEqual(group[0], s3);

            group = right[1] as Group;
            Assert.IsNotNull(group);
            Assert.AreEqual(group[0], s4);
        }
    }
}
