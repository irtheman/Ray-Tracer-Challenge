using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System.Collections.Generic;

namespace CSharpTest
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void TestGroupCreate()
        {
            var g = new Group();

            Assert.AreEqual(g.Transform, Matrix.Identity);
            Assert.AreEqual(g.Count, 0);
        }

        [TestMethod]
        public void TestGroupChild()
        {
            var g = new Group();
            var s = new Sphere();
            g.Add(s);

            Assert.AreEqual(g.Count, 1);
            Assert.AreEqual(g[0], s);
            Assert.AreEqual(s.Parent, g);
        }

        [TestMethod]
        public void TestGroupEmptyIntersect()
        {
            var g = new Group();
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var xs = g.Intersect(r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestGroupIntersect()
        {
            var g = new Group();
            var s1 = new Sphere();
            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(0, 0, -3);
            var s3 = new Sphere();
            s3.Transform = Matrix.Translation(5, 0, 0);

            g.Add(s1);
            g.Add(s2);
            g.Add(s3);

            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = g.Intersect(r);

            Assert.AreEqual(xs.Count, 4);
            Assert.AreEqual(xs[0].Object, s2);
            Assert.AreEqual(xs[1].Object, s2);
            Assert.AreEqual(xs[2].Object, s1);
            Assert.AreEqual(xs[3].Object, s1);
        }

        [TestMethod]
        public void TestGroupTransformIntersect()
        {
            var g = new Group();
            g.Transform = Matrix.Scaling(2, 2, 2);
            var s = new Sphere();
            s.Transform = Matrix.Translation(5, 0, 0);
            g.Add(s);

            var r = new Ray(new Point(10, 0, -10), new Vector(0, 0, 1));
            var xs = g.Intersect(r);

            Assert.AreEqual(xs.Count, 2);
        }

        [TestMethod]
        public void TestGroupBBWithChildren()
        {
            var s = new Sphere();
            s.Transform = Matrix.Translation(2, 5, -3) *
                          Matrix.Scaling(2, 2, 2);

            var c = new Cylinder();
            c.Minimum = -2;
            c.Maximum = 2;
            c.Transform = Matrix.Translation(-4, -1, 4) *
                          Matrix.Scaling(0.5, 1, 0.5);

            var shape = new Group();
            shape.Add(s);
            shape.Add(c);

            var box = shape.Bounds;

            Assert.AreEqual(box.Min, new Point(-4.5, -3, -5));
            Assert.AreEqual(box.Max, new Point(4, 7, 4.5));
        }

        [TestMethod]
        public void TestGroupIntersectMiss()
        {
            var child = new TestShape();
            var shape = new Group();
            shape.Add(child);
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
            var xs = shape.Intersect(r);

            Assert.IsNull(child.SavedRay);
        }

        [TestMethod]
        public void TestGroupIntersectHit()
        {
            var child = new TestShape();
            var shape = new Group();
            shape.Add(child);
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = shape.Intersect(r);

            Assert.IsNotNull(child.SavedRay);
        }

        [TestMethod]
        public void TestGroupPartitionChildren()
        {
            var g = new Group();

            var s1 = new Sphere();
            s1.Transform = Matrix.Translation(-2, 0, 0);
            g.Add(s1);

            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(2, 0, 0);
            g.Add(s2);

            var s3 = new Sphere();
            g.Add(s3);

            Group left, right;
            g.PartitionChildren(out left, out right);

            Assert.AreEqual(g[0], s3);
            Assert.AreEqual(left[0], s1);
            Assert.AreEqual(right[0], s2);
        }

        [TestMethod]
        public void TestGroupSubGroup()
        {
            var s1 = new Sphere();
            var s2 = new Sphere();
            var g = new Group();

            g.MakeSubGroup(new List<RTObject>() { s1, s2 });

            Assert.AreEqual(g.Count, 1);

            var group = g[0] as Group;
            Assert.IsNotNull(group);
            Assert.IsTrue(group.Contains(s1));
            Assert.IsTrue(group.Contains(s2));
        }

        [TestMethod]
        public void TestGroupSubdividePartitionChildren()
        {
            var g = new Group();

            var s1 = new Sphere();
            s1.Transform = Matrix.Translation(-2, -2, 0);
            g.Add(s1);

            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(-2, 2, 0);
            g.Add(s2);

            var s3 = new Sphere();
            s3.Transform = Matrix.Scaling(4, 4, 4);
            g.Add(s3);

            g.Divide(1);

            Assert.AreEqual(g[0], s3);

            Group subgroup = g[1] as Group;
            Assert.IsNotNull(subgroup);
            Assert.AreEqual(subgroup.Count, 2);
            Assert.AreEqual(((Group)subgroup[0])[0], s1);
            Assert.AreEqual(((Group)subgroup[1])[0], s2);
        }
        [TestMethod]
        public void TestGroupSubdivideToFewChildren()
        {
            var group = new Group();
            var subgroup = new Group();

            var s1 = new Sphere();
            s1.Transform = Matrix.Translation(-2, 0, 0);
            subgroup.Add(s1);

            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(2, 1, 0);
            subgroup.Add(s2);

            var s3 = new Sphere();
            s3.Transform = Matrix.Translation(2, -1, 0);
            subgroup.Add(s3);

            var s4 = new Sphere();
            group.Add(subgroup);
            group.Add(s4);

            group.Divide(3);

            Assert.AreEqual(group[0], subgroup);
            Assert.AreEqual(group[1], s4);

            Assert.AreEqual(subgroup.Count, 2);
            Assert.AreEqual(((Group)subgroup[0])[0], s1);
            Assert.AreEqual(((Group)subgroup[1])[0], s2);
            Assert.AreEqual(((Group)subgroup[1])[1], s3);
        }
    }
}
