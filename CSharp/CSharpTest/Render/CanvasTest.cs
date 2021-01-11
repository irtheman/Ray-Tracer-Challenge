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

        [TestMethod]
        public void TestCanvasSetPPMFail()
        {
            string ppm = @"P32
1 1
255
0 0 0";

            var c = Canvas.SetPPM(ppm);
            Assert.IsNull(c);
        }

        [TestMethod]
        public void TestCanvasSetPPMSize()
        {
            string ppm = @"P3
10 2
255
0 0 0  0 0 0  0 0 0  0 0 0  0 0 0
0 0 0  0 0 0  0 0 0  0 0 0  0 0 0
0 0 0  0 0 0  0 0 0  0 0 0  0 0 0
0 0 0  0 0 0  0 0 0  0 0 0  0 0 0";

            var c = Canvas.SetPPM(ppm);

            Assert.AreEqual(c.Width, 10);
            Assert.AreEqual(c.Height, 2);
        }

        [TestMethod]
        public void TestCanvasPixelData()
        {
            string ppm = @"P3
4 3
255
255 127 0  0 127 255  127 255 0  255 255 255
0 0 0  255 0 0  0 255 0  0 0 255
255 255 0  0 255 255  255 0 255  127 127 127";

            var c = Canvas.SetPPM(ppm);

            Assert.AreEqual(c[0, 0], new Color(1, 0.498, 0));
            Assert.AreEqual(c[1, 0], new Color(0, 0.498, 1));
            Assert.AreEqual(c[2, 0], new Color(0.498, 1, 0));
            Assert.AreEqual(c[3, 0], new Color(1, 1, 1));

            Assert.AreEqual(c[0, 1], new Color(0, 0, 0));
            Assert.AreEqual(c[1, 1], new Color(1, 0, 0));
            Assert.AreEqual(c[2, 1], new Color(0, 1, 0));
            Assert.AreEqual(c[3, 1], new Color(0, 0, 1));

            Assert.AreEqual(c[0, 2], new Color(1, 1, 0));
            Assert.AreEqual(c[1, 2], new Color(0, 1, 1));
            Assert.AreEqual(c[2, 2], new Color(1, 0, 1));
            Assert.AreEqual(c[3, 2], new Color(0.498, 0.498, 0.498));
        }

        [TestMethod]
        public void TestCanvasComment()
        {
            string ppm = @"P3
# this is a comment
2 1
# this, too
255
# another comment
255 255 255
# oh, no, comments in the pixel data!
255 0 255";

            var c = Canvas.SetPPM(ppm);

            Assert.AreEqual(c[0, 0], new Color(1, 1, 1));
            Assert.AreEqual(c[1, 0], new Color(1, 0, 1));
        }

        [TestMethod]
        public void TestCanvasPixelSpansLines()
        {
            string ppm = @"P3
1 1
255
51
153

204";

            var c = Canvas.SetPPM(ppm);

            Assert.AreEqual(c[0, 0], new Color(0.2, 0.6, 0.8));
        }

        [TestMethod]
        public void TestCanvasParsingWithScaleSetting()
        {
            string ppm = @"P3
2 2
100
100 100 100  50 50 50
75 50 25  0 0 0";

            var c = Canvas.SetPPM(ppm);

            Assert.AreEqual(c[0, 1], new Color(0.75, 0.5, 0.25));
        }
    }
}
