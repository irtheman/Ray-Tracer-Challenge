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
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 1.0);

            Assert.AreEqual(result, new Color(1.9, 1.9, 1.9));
        }

        [TestMethod]
        public void TestMaterialLightingBehindEyeOffset45()
        {
            var value = MathHelper.SQRT2 / 2.0;
            var eyev = new Vector(0, value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 1.0);

            Assert.AreEqual(result, Color.White);
        }

        [TestMethod]
        public void TestMaterialLightingOffset45()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 1.0);

            Assert.AreEqual(result, new Color(0.7364, 0.7364, 0.7364));
        }

        [TestMethod]
        public void TestMaterialLightingEyeInlineReflection()
        {
            var value = MathHelper.SQRT2 / 2.0;
            var eyev = new Vector(0, -value, -value);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 1.0);

            Assert.AreEqual(result, new Color(1.6364, 1.6364, 1.6364));
        }
        [TestMethod]
        public void TestMaterialLightingBehindSurface()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, 10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 1.0);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialLightingSurfaceInShadow()
        {
            var eyev = new Vector(0, 0, -1);
            var normalv = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), Color.White);
            var result = m.Lighting(new Sphere(), light, position, eyev, normalv, 0.0);

            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
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
            var c1 = m.Lighting(shape, light, new Point(0.9, 0, 0), eyev, normalv, 1.0);
            var c2 = m.Lighting(shape, light, new Point(1.1, 0, 0), eyev, normalv, 1.0);

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
        public void TestMaterialTransparencyRefractiveCreate()
        {
            var m = new Material();

            Assert.AreEqual(m.Transparency, 0.0, epsilon);
            Assert.AreEqual(m.RefractiveIndex, 1.0, epsilon);
        }

        [TestMethod]
        public void TestMaterialLightingIntensityForAttenuateColor()
        {
            var w = World.Default;
            w.Lights[0] = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            var shape = w.Objects[0];
            shape.Material.Ambient = 0.1;
            shape.Material.Diffuse = 0.9;
            shape.Material.Specular = 0.0;
            shape.Material.Color = Color.White;
            var pt = new Point(0, 0, -1);
            var eyeV = new Vector(0, 0, -1);
            var normalV = new Vector(0, 0, -1);

            Assert.AreEqual(shape.Material.Lighting(w.Objects[1], w.Lights[0], pt, eyeV, normalV, 1.0), new Color(1, 1, 1));
            Assert.AreEqual(shape.Material.Lighting(w.Objects[1], w.Lights[0], pt, eyeV, normalV, 0.5), new Color(0.55, 0.55, 0.55));
            Assert.AreEqual(shape.Material.Lighting(w.Objects[1], w.Lights[0], pt, eyeV, normalV, 0.0), new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestMaterialAreaLight()
        {
            var corner = new Point(-0.5, -0.5, -5);
            var v1 = Vector.VectorX;
            var v2 = Vector.VectorY;
            var light = new AreaLight(corner, v1, 2, v2, 2, Color.White);
            var shape = new Sphere();
            shape.Material.Ambient = 0.1;
            shape.Material.Diffuse = 0.9;
            shape.Material.Specular = 0.0;
            shape.Material.Color = Color.White;
            var eye = new Point(0, 0, -5);
            Point pt;
            Vector eyeV, normalV;
            Color result;

            pt = new Point(0, 0, -1);
            eyeV = (new Vector(eye - pt)).Normalize;
            normalV = new Vector(pt);
            result = shape.Material.Lighting(shape, light, pt, eyeV, normalV, 1.0);
            Assert.AreEqual(result, new Color(0.9965, 0.9965, 0.9965), "test 1");

            pt = new Point(0, 0.7071, -0.7071);
            eyeV = (new Vector(eye - pt)).Normalize;
            normalV = new Vector(pt);
            result = shape.Material.Lighting(shape, light, pt, eyeV, normalV, 1.0);
            Assert.AreEqual(result, new Color(0.6232, 0.6232, 0.6232), "test 2");
        }
    }
}
