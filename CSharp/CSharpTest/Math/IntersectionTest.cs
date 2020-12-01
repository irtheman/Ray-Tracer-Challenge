using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class IntersectionTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestCreateIntersection()
        {
            var s = new Sphere();
            var i = new Intersection(3.5, s);

            Assert.AreEqual(i.t, 3.5, epsilon);
            Assert.AreEqual(i.Object, s);
        }

        [TestMethod]
        public void TestCreateIntersections()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].t, 1, epsilon);
            Assert.AreEqual(xs[1].t, 2, epsilon);
        }

        [TestMethod]
        public void TestHitsAllPositive()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.AreEqual(i, i1);
        }

        [TestMethod]
        public void TestHitsSomePositive()
        {
            var s = new Sphere();
            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(1, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.AreEqual(i, i2);
        }

        [TestMethod]
        public void TestHitsAllNegative()
        {
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = new Intersections(i1, i2);
            var i = xs.Hit;

            Assert.IsNull(i);
        }

        [TestMethod]
        public void TestHitsBestChoice()
        {
            var s = new Sphere();
            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);
            var xs = new Intersections(i1, i2, i3, i4);
            var i = xs.Hit;

            Assert.AreEqual(i, i4);
        }

        [TestMethod]
        public void TestPrepareComputation()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r, new Intersections(i));

            Assert.AreEqual(comps.t, i.t);
            Assert.AreEqual(comps.Object, i.Object);
            Assert.AreEqual(comps.Point, new Point(0, 0, -1));
            Assert.AreEqual(comps.EyeVector, new Vector(0, 0, -1));
            Assert.AreEqual(comps.NormalVector, new Vector(0, 0, -1));
        }

        [TestMethod]
        public void TestPrepareComputationInside()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r, new Intersections(i));

            Assert.IsFalse(comps.Inside);
        }

        [TestMethod]
        public void TestPrepareComputationInsideHit()
        {
            var r = new Ray(Point.Zero, Vector.VectorZ);
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r, new Intersections(i));

            Assert.AreEqual(comps.Point, Point.PointZ);
            Assert.AreEqual(comps.EyeVector, new Vector(0, 0, -1));
            Assert.AreEqual(comps.NormalVector, new Vector(0, 0, -1));
            Assert.IsTrue(comps.Inside);
        }

        [TestMethod]
        public void TestHitOffsetsPoint()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var shape = new Sphere();
            shape.Transform = Matrix.Translation(0, 0, 1);
            var i = new Intersection(5, shape);
            var comps = i.PrepareComputations(r, new Intersections(i));

            Assert.IsTrue(comps.OverPoint.z < -epsilon / 2);
            Assert.IsTrue(comps.Point.z > comps.OverPoint.z);
        }

        [TestMethod]
        public void TestIntersectionPrecomputingReflectionVector()
        {
            var shape = new Plane();
            var r = new Ray(new Point(0, 1, -1), new Vector(0, -MathHelper.SQRT2 / 2.0, MathHelper.SQRT2 / 2.0));
            var i = new Intersection(MathHelper.SQRT2, shape);
            var comps = i.PrepareComputations(r, new Intersections(i));

            Assert.AreEqual(comps.ReflectVector, new Vector(0, MathHelper.SQRT2 / 2, MathHelper.SQRT2 / 2));
        }

        [TestMethod]
        public void TestIntersectionN1N2()
        {
            var a = Sphere.Glass;
            a.Transform = Matrix.Scaling(2, 2, 2);
            a.Material.RefractiveIndex = 1.5;

            var b = Sphere.Glass;
            b.Transform = Matrix.Translation(0, 0, -0.25);
            b.Material.RefractiveIndex = 2.0;

            var c = Sphere.Glass;
            c.Transform = Matrix.Translation(0, 0, 0.25);
            c.Material.RefractiveIndex = 2.5;

            var r = new Ray(new Point(0, 0, -4), Vector.VectorZ);
            var xs = new Intersections();
            xs.Add(new Intersection(2, a));
            xs.Add(new Intersection(2.75, b));
            xs.Add(new Intersection(3.25, c));
            xs.Add(new Intersection(4.75, b));
            xs.Add(new Intersection(5.25, c));
            xs.Add(new Intersection(6, a));

            double[,] ns = new double[,]
            {
                { 1.0, 1.5 },
                { 1.5, 2.0 },
                { 2.0, 2.5 },
                { 2.5, 2.5 },
                { 2.5, 1.5 },
                { 1.5, 1.0 }
            };

            for (int i = 0; i < xs.Count; i++)
            {
                var comps = xs[i].PrepareComputations(r, xs);
                Assert.AreEqual(comps.N1, ns[i, 0]);
                Assert.AreEqual(comps.N2, ns[i, 1]);
            }
        }

        [TestMethod]
        public void TestIntersectionUnderPoint()
        {
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var shape = Sphere.Glass;
            shape.Transform = Matrix.Translation(0, 0, 1);
            var i = new Intersection(5, shape);
            var xs = new Intersections(i);
            var comps = i.PrepareComputations(r, xs);

            Assert.IsTrue(comps.UnderPoint.z > epsilon / 2.0);
            Assert.IsTrue(comps.Point.z < comps.UnderPoint.z);
        }
    }
}
