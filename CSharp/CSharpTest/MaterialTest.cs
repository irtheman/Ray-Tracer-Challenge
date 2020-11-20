﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            position = new Point(0, 0, 0);
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
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var result = m.Lighting(light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(1.9, 1.9, 1.9));
        }

        [TestMethod]
        public void TestMaterialLightingBehindEyeOffset45()
        {
            var value = Math.Sqrt(2) / 2.0;
            var eyev = new Vector(0, value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var result = m.Lighting(light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(1, 1, 1));
        }

        [TestMethod]
        public void TestMaterialLightingOffset45()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
            var result = m.Lighting(light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(0.7364, 0.7364, 0.7364));
        }

        [TestMethod]
        public void TestMaterialLightingEyeInlineReflection()
        {
            var value = Math.Sqrt(2) / 2.0;
            var eyev = new Vector(0, -value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
            var result = m.Lighting(light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(1.6364, 1.6364, 1.6364));
        }
        [TestMethod]
        public void TestMaterialLightingBehindSurface()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, 10), new Color(1, 1, 1));
            var result = m.Lighting(light, position, eyev, normalv, false);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialLightingSurfaceInShadow()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var inShadow = true;
            var result = m.Lighting(light, position, eyev, normalv, inShadow);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialNoShadowCollinear()
        {
            var w = new World(true);
            var p = new Point(0, 10, 0);

            Assert.IsFalse(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialWithShadow()
        {
            var w = new World(true);
            var p = new Point(10, -10, 10);

            Assert.IsTrue(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialNoShadowBehindLight()
        {
            var w = new World(true);
            var p = new Point(-20, 20, -20);

            Assert.IsFalse(w.IsShadowed(p));
        }

        [TestMethod]
        public void TestMaterialNoShadowBehindPoint()
        {
            var w = new World(true);
            var p = new Point(-2, 2, -2);

            Assert.IsFalse(w.IsShadowed(p));
        }
    }
}
