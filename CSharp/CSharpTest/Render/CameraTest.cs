using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class CameraTest
    {
        private const double epsilon = 0.00001;

        [TestMethod]
        public void TestCameraCreate()
        {
            int hsize = 160;
            int vsize = 120;
            double fieldOfView = Math.PI / 2;
            var c = new Camera(hsize, vsize, fieldOfView);

            Assert.AreEqual(c.HSize, 160);
            Assert.AreEqual(c.VSize, 120);
            Assert.AreEqual(c.FOV, Math.PI / 2, epsilon);
            Assert.AreEqual(c.Transform, Matrix.Identity);
        }

        [TestMethod]
        public void TestCameraPixelSizeHorizontal()
        {
            var c = new Camera(200, 125, Math.PI / 2);

            Assert.AreEqual(c.PixelSize, 0.01, epsilon);
        }

        [TestMethod]
        public void TestCameraPixelSizeVertical()
        {
            var c = new Camera(125, 200, Math.PI / 2);

            Assert.AreEqual(c.PixelSize, 0.01, epsilon);
        }

        [TestMethod]
        public void TestCameraIntersectCenter()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var r = c.RayForPixel(100, 50);

            Assert.AreEqual(r.Origin, Point.Zero);
            Assert.AreEqual(r.Direction, new Vector(0, 0, -1));
        }

        [TestMethod]
        public void TestCameraIntersectCorner()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var r = c.RayForPixel(0, 0);

            Assert.AreEqual(r.Origin, Point.Zero);
            Assert.AreEqual(r.Direction, new Vector(0.66519, 0.33259, -0.66851));
        }

        [TestMethod]
        public void TestCameraIntersectTransformed()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            c.Transform = Matrix.RotationY(Math.PI / 4) * Matrix.Translation(0, -2, 5);
            var r = c.RayForPixel(100, 50);
            var value = MathHelper.SQRT2 / 2;

            Assert.AreEqual(r.Origin, new Point(0, 2, -5));
            Assert.AreEqual(r.Direction, new Vector(value, 0, -value));
        }

        [TestMethod]
        public void TestCameraRenderWorld()
        {
            var w = World.Default;
            var c = new Camera(11, 11, Math.PI / 2);
            var from = new Point(0, 0, -5);
            var to = Point.Zero;
            var up = Vector.VectorY;
            c.Transform = Matrix.ViewTransform(from, to, up);
            var image = c.Render(w);

            Assert.AreEqual(image[5, 5], new Color(0.38066, 0.47583, 0.2855));


        }
    }
}
