using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void TestWorldDefaultCreate()
        {
            var w = new World();

            Assert.AreEqual(w.Objects.Count, 0);
            Assert.AreEqual(w.Lights.Count, 0);
        }

        [TestMethod]
        public void TestWorldDefault()
        {
            var w = World.Default;
            var light = new PointLight(new Point(-10, 10, -10), Color.White);
            var s1 = new Sphere();
            s1.Material.Color = new Color(0.8, 1, 0.6);
            s1.Material.Diffuse = 0.7;
            s1.Material.Specular = 0.2;
            var s2 = new Sphere();
            s2.Transform = Matrix.Scaling(0.5, 0.5, 0.5);

            Assert.AreEqual(w.Lights[0], light);
            Assert.IsTrue(w.Objects.Contains(s1));
            Assert.IsTrue(w.Objects.Contains(s2));
        }

        [TestMethod]
        public void TestWorldRayIntersect()
        {
            var w = World.Default;
            var ray = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var xs = w.Intersect(ray);

            Assert.AreEqual(xs.Count, 4);
            Assert.AreEqual(xs[0].t, 4);
            Assert.AreEqual(xs[1].t, 4.5);
            Assert.AreEqual(xs[2].t, 5.5);
            Assert.AreEqual(xs[3].t, 6);
        }

        [TestMethod]
        public void TestWorldShadingIntersection()
        {
            var w = World.Default;
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var shape = w.Objects[0];
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHit(i.Object, comps);

            Assert.AreEqual(c, new Color(0.38066, 0.47583, 0.2855));
        }

        [TestMethod]
        public void TestWorldShadingIntersectionInside()
        {
            var w = World.Default;
            w.Lights[0] = new PointLight(new Point(0, 0.25, 0), Color.White);
            var r = new Ray(Point.Zero, Vector.VectorZ);
            var shape = w.Objects[1];
            var i = new Intersection(0.5, shape);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHit(i.Object, comps);

            Assert.AreEqual(c, new Color(0.90498, 0.90498, 0.90498));
        }

        [TestMethod]
        public void TestWorldMissesColor()
        {
            var w = World.Default;
            var r = new Ray(new Point(0, 0, -5), Vector.VectorY);
            var c = w.ColorAt(r);

            Assert.AreEqual(c, Color.Black);
        }

        [TestMethod]
        public void TestWorldHitsColor()
        {
            var w = World.Default;
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var c = w.ColorAt(r);

            Assert.AreEqual(c, new Color(0.38066, 0.47583, 0.2855));
        }

        [TestMethod]
        public void TestWorldHitBehindRayColor()
        {
            var w = World.Default;
            var outer = w.Objects[0];
            outer.Material.Ambient = 1;
            var inner = w.Objects[1];
            inner.Material.Ambient = 1;
            var r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
            var c = w.ColorAt(r);

            Assert.AreEqual(c, inner.Material.Color);
        }

        [TestMethod]
        public void TestWorldShadeHitWithShadow()
        {
            var w = new World();
            w.Lights.Add(new PointLight(new Point(0, 0, -10), Color.White));
            
            var s1 = new Sphere();
            w.Objects.Add(s1);

            var s2 = new Sphere();
            s2.Transform = Matrix.Translation(0, 0, 10);
            w.Objects.Add(s2);

            var r = new Ray(new Point(0, 0, 5), Vector.VectorZ);
            var i = new Intersection(4, s2);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHit(i.Object, comps);

            Assert.AreEqual(c, new Color(0.1, 0.1, 0.1));
        }
    }
}
