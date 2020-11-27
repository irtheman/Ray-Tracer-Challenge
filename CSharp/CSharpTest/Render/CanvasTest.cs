using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class CanvasTest
    {
        [TestMethod]
        public void TestCanvasCreate()
        {
            var c = new Canvas(10, 20);
            var black = Color.Black;

            Assert.AreEqual(c.Width, 10);
            Assert.AreEqual(c.Height, 20);

            for (int i = 0; i < c.Width; i++)
            {
                for (int j = 0; j < c.Height; j++)
                {
                    Assert.AreEqual(c[i, j], black);
                }
            }
        }

        [TestMethod]
        public void TestCanvasWrite()
        {
            var c = new Canvas(10, 20);
            var red = Color.Red;
            c[2, 3] = red;

            Assert.AreEqual(c[2, 3], red);
        }

        [TestMethod]
        public void TestCanvasPPMHeader()
        {
            var c = new Canvas(5, 3);
            var ppm = c.GetPPM();
            var lines = ppm.Split('\n');

            Assert.AreEqual(lines[0], "P3");
            Assert.AreEqual(lines[1], "5 3");
            Assert.AreEqual(lines[2], "255");
        }

        [TestMethod]
        public void TestCanvasPPMPixelData()
        {
            var c = new Canvas(5, 3);
            var c1 = new Color(1.5, 0, 0);
            var c2 = new Color(0, 0.5, 0);
            var c3 = new Color(-0.5, 0, 1);

            c[0, 0] = c1;
            c[2, 1] = c2;
            c[4, 2] = c3;

            var ppm = c.GetPPM();
            var lines = ppm.Split('\n');

            Assert.AreEqual(lines[3], "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
            Assert.AreEqual(lines[4], "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0");
            Assert.AreEqual(lines[5], "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");
        }

        [TestMethod]
        public void TestCanvasPPMLongLines()
        {
            var c = new Canvas(10, 2);
            var p = new Color(1, 0.8, 0.6);

            for (int i = 0; i < c.Width; i++)
            {
                for (int j = 0; j < c.Height; j++)
                {
                    c[i, j] = p;
                }
            }

            var ppm = c.GetPPM();
            var lines = ppm.Split('\n');

            Assert.AreEqual(lines[3], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
            Assert.AreEqual(lines[4], "153 255 204 153 255 204 153 255 204 153 255 204 153");
            Assert.AreEqual(lines[5], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
            Assert.AreEqual(lines[6], "153 255 204 153 255 204 153 255 204 153 255 204 153");
        }

        [TestMethod]
        public void TestCanvasPPMEndsNewLine()
        {
            var c = new Canvas(5, 3);
            var ppm = c.GetPPM();
         
            Assert.AreEqual(ppm[ppm.Length - 1], '\n');
        }
    }
}
