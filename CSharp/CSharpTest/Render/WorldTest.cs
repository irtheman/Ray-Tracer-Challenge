using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

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
            var comps = i.PrepareComputations(r, new Intersections());
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
            var comps = i.PrepareComputations(r, new Intersections());
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
            var comps = i.PrepareComputations(r, new Intersections());
            var c = w.ShadeHit(i.Object, comps);

            Assert.AreEqual(c, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestWorldReflectForNonReflective()
        {
            var w = World.Default;
            var r = new Ray(Point.Zero, Vector.VectorZ);
            var shape = w.Objects[1];
            shape.Material.Ambient = 1;
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r, new Intersections());
            var color = w.ReflectedColor(comps);

            Assert.AreEqual(color, Color.Black);
        }

        [TestMethod]
        public void TestWorldReflectForReflective()
        {
            var w = World.Default;
            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0, -1, 0);
            w.Objects.Add(shape);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r, new Intersections());
            var color = w.ReflectedColor(comps);

            // Note: Modified results: 0.19032, 0.2379, 0.14274
            Assert.AreEqual(color, new Color(0.19033, 0.23792, 0.142749));
        }

        [TestMethod]
        public void TestWorldShadeHitWithReflective()
        {
            var w = World.Default;
            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0, -1, 0);
            w.Objects.Add(shape);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r, new Intersections());
            var color = w.ShadeHit(shape, comps);

            // Note: Modified results: 0.87677, 0.92436, 0.82918
            Assert.AreEqual(color, new Color(0.87675, 0.92434, 0.82917));
        }

        [TestMethod]
        public void TestWorldColorAt2Reflective()
        {
            var w = new World();
            w.Lights.Add(new PointLight(Point.Zero, Color.White));
            var lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform = Matrix.Translation(0, -1, 0);
            w.Objects.Add(lower);
            var upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform = Matrix.Translation(0, 1, 0);
            w.Objects.Add(upper);
            var r = new Ray(Point.Zero, Vector.VectorY);
            var color = w.ColorAt(r);

            Assert.AreNotEqual(color, Color.Black);
        }

        [TestMethod]
        public void TestWorldReflectedMaxRecursion()
        {
            var w = World.Default;
            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0, -1, 0);
            w.Objects.Add(shape);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r, new Intersections());
            var color = w.ReflectedColor(comps, 0);

            // Note: Modified results: 0.87677, 0.92436, 0.82918
            Assert.AreEqual(color, Color.Black);
        }

        [TestMethod]
        public void TestWorldRefractedColorOpaqueSurface()
        {
            var w = World.Default;
            var shape = w.Objects[0];
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var i1 = new Intersection(4, shape);
            var i2 = new Intersection(6, shape);
            var xs = new Intersections(i1, i2);
            var comps = xs[0].PrepareComputations(r, xs);
            var c = w.RefractedColor(comps, 5);

            Assert.AreEqual(c, Color.Black);
        }

        [TestMethod]
        public void TestWorldRefractedColorMaxRecursion()
        {
            var w = World.Default;
            var shape = w.Objects[0];
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, -5), Vector.VectorZ);
            var i1 = new Intersection(4, shape);
            var i2 = new Intersection(6, shape);
            var xs = new Intersections(i1, i2);
            var comps = xs[0].PrepareComputations(r, xs);
            var c = w.RefractedColor(comps, 0);

            Assert.AreEqual(c, Color.Black);
        }

        [TestMethod]
        public void TestWorldRefractedColorTotalInternalReflection()
        {
            var sqrt2 = Math.Sqrt(2);
            var w = World.Default;
            var shape = w.Objects[0];
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, sqrt2 / 2.0), Vector.VectorY);
            var i1 = new Intersection(-sqrt2 / 2.0, shape);
            var i2 = new Intersection(sqrt2 / 2.0, shape);
            var xs = new Intersections(i1, i2);
            var comps = xs[1].PrepareComputations(r, xs);
            var c = w.RefractedColor(comps, 5);

            Assert.AreEqual(c, Color.Black);
        }

        [TestMethod]
        public void TestWorldRefractedColorWithRefractedRay()
        {
            var w = World.Default;
            var a = w.Objects[0];
            a.Material.Ambient = 1.0;
            a.Material.Pattern = new TestPattern();
            var b = w.Objects[1];
            b.Material.Transparency = 1.0;
            b.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, 0.1), Vector.VectorY);
            var i1 = new Intersection(-0.9899, a);
            var i2 = new Intersection(-0.4899, b);
            var i3 = new Intersection(0.4899, b);
            var i4 = new Intersection(0.9899, a);
            var xs = new Intersections(i1, i2, i3, i4);
            var comps = xs[2].PrepareComputations(r, xs);
            var c = w.RefractedColor(comps, 5);

            // Note: Modified results: 0, 0.99888, 0.04725
            Assert.AreEqual(c, new Color(0, 0.99887, 0.04722));
        }

        [TestMethod]
        public void TestWorldShadeHitTransparentMaterial()
        {
            var sqrt2 = Math.Sqrt(2);
            var w = World.Default;

            var floor = new Plane();
            floor.Transform = Matrix.Translation(0, -1, 0);
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.Objects.Add(floor);

            var ball = new Sphere();
            ball.Material.Color = Color.Red;
            ball.Material.Ambient = 0.5;
            ball.Transform = Matrix.Translation(0, -3.5, -0.5);
            w.Objects.Add(ball);

            var r = new Ray(new Point(0, 0, -3), new Vector(0, -sqrt2 / 2.0, sqrt2 / 2.0));
            var i = new Intersection(sqrt2, floor);
            var xs = new Intersections(i);
            var comps = xs[0].PrepareComputations(r, xs);
            var color = w.ShadeHit(new Sphere(), comps, 5);

            Assert.AreEqual(color, new Color(0.93642, 0.68642, 0.68642));
        }
    }
}
