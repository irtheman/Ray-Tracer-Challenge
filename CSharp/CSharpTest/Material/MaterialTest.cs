using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class MaterialTest
    {
        private const double epsilon = 0.00001d;
        private Material m;
        private Point position;

        [TestInitialize]
        public void TestMaterialInitialize()
        {
            m = new Material();
            position = Point.Zero;
        }

        [TestCleanup]
        public void TestMaterialCleanup()
        {
            m = null;
            position = null;
        }

        [TestMethod]
        public void TestMaterialCreate()
        {
            Assert.AreEqual(m.Ambient, 0.1, epsilon);
            Assert.AreEqual(m.Diffuse, 0.9, epsilon);
            Assert.AreEqual(m.Specular, 0.9, epsilon);
            Assert.AreEqual(m.Shininess, 200.0, epsilon);
        }

        [TestMethod]
        public void TestMaterialLightingBehindEye()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(1.9, 1.9, 1.9));
        }

        [TestMethod]
        public void TestMaterialLightingBehindEyeOffset45()
        {
            var value = Math.Sqrt(2) / 2.0;
            var eyev = new Vector(0, value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, false);

            Assert.AreEqual(result, Color.White);
        }

        [TestMethod]
        public void TestMaterialLightingOffset45()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(0.7364, 0.7364, 0.7364));
        }

        [TestMethod]
        public void TestMaterialLightingEyeInlineReflection()
        {
            var value = Math.Sqrt(2) / 2.0;
            var eyev = new Vector(0, -value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(1.6364, 1.6364, 1.6364));
        }
        [TestMethod]
        public void TestMaterialLightingBehindSurface()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, 10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialLightingSurfaceInShadow()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var inShadow = true;
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, inShadow);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialNoShadowCollinear()
        {
            var w = World.Default;
            var p = new Point(0, 10, 0);

            Assert.IsFalse(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialWithShadow()
        {
            var w = World.Default;
            var p = new Point(10, -10, 10);

            Assert.IsTrue(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialNoShadowBehindLight()
        {
            var w = World.Default;
            var p = new Point(-20, 20, -20);

            Assert.IsFalse(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialNoShadowBehindPoint()
        {
            var w = World.Default;
            var p = new Point(-2, 2, -2);

            Assert.IsFalse(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialLightingWithPattern()
        {
            var shape = new Sphere();
            m.Pattern = new StripePattern(Color.White, Color.Black);
            m.Ambient = 1;
            m.Diffuse = 0;
            m.Specular = 0;
            var eyev = -Vector.VectorZ;
            var normalv = -Vector.VectorZ;
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var c1 = m.Lighting(shape, light, new Point(0.9, 0, 0), eyev, normalv, false);
            var c2 = m.Lighting(shape, light, new Point(1.1, 0, 0), eyev, normalv, false);

            Assert.AreEqual(c1, Color.White);
            Assert.AreEqual(c2, Color.Black);
        }

        [TestMethod]
        public void TestMaterialReflectivityCreate()
        {
            var m = new Material();
            Assert.AreEqual(m.Reflective, 0.0, epsilon);
        }

        [TestMethod]
        public void TestMaterialPrecomputingReflectionVector()
        {
            var shape = new Plane();
            var r = new Ray(new Point(0, 1, -1), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r, new Intersections());

            Assert.AreEqual(comps.ReflectVector, new Vector(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
        }

        [TestMethod]
        public void TestMaterialTransparencyRefractiveCreate()
        {
            var m = new Material();

            Assert.AreEqual(m.Transparency, 0.0, epsilon);
            Assert.AreEqual(m.RefractiveIndex, 1.0, epsilon);
        }
    }
}
