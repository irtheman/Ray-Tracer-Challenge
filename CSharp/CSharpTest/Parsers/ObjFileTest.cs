using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class ObjFileTest
    {
        [TestMethod]
        public void TestObjFileIgnoreGibberish()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\GibberishTest.obj");

            Assert.AreEqual(parser.Ignored, 5);
        }

        [TestMethod]
        public void TestObjFileVertexRecords()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\VertexTest.obj");

            Assert.AreEqual(parser.Vertices[1], new Point(-1, 1, 0));
            Assert.AreEqual(parser.Vertices[2], new Point(-1, 0.5, 0));
            Assert.AreEqual(parser.Vertices[3], new Point(1, 0, 0));
            Assert.AreEqual(parser.Vertices[4], new Point(1, 1, 0));
        }

        [TestMethod]
        public void TestObjFileFaces()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\FacesTest.obj");
            Group g = parser.DefaultGroup;
            var t1 = g[0] as Triangle;
            var t2 = g[1] as Triangle;

            Assert.AreEqual(t1.P1, parser.Vertices[1]);
            Assert.AreEqual(t1.P2, parser.Vertices[2]);
            Assert.AreEqual(t1.P3, parser.Vertices[3]);
            Assert.AreEqual(t2.P1, parser.Vertices[1]);
            Assert.AreEqual(t2.P2, parser.Vertices[3]);
            Assert.AreEqual(t2.P3, parser.Vertices[4]);
        }

        [TestMethod]
        public void TestObjFilePolygon()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\PolygonTest.obj");
            Group g = parser.DefaultGroup;
            var t1 = g[0] as Triangle;
            var t2 = g[1] as Triangle;
            var t3 = g[2] as Triangle;

            Assert.AreEqual(t1.P1, parser.Vertices[1]);
            Assert.AreEqual(t1.P2, parser.Vertices[2]);
            Assert.AreEqual(t1.P3, parser.Vertices[3]);
            Assert.AreEqual(t2.P1, parser.Vertices[1]);
            Assert.AreEqual(t2.P2, parser.Vertices[3]);
            Assert.AreEqual(t2.P3, parser.Vertices[4]);
            Assert.AreEqual(t3.P1, parser.Vertices[1]);
            Assert.AreEqual(t3.P2, parser.Vertices[4]);
            Assert.AreEqual(t3.P3, parser.Vertices[5]);
        }

        [TestMethod]
        public void TestObjFileConvertToGroup()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\TrianglesTest.obj");
            var g = parser.ObjToGroup;
            Assert.IsTrue(g.Contains(parser.Groups[0]));
            Assert.IsTrue(g.Contains(parser.Groups[1]));
        }

        [TestMethod]
        public void TestObjFileNormalVertex()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\NormalVertexTest.obj");

            Assert.AreEqual(parser.Normals[1], new Vector(0, 0, 1));
            Assert.AreEqual(parser.Normals[2], new Vector(0.707, 0, -0.707));
            Assert.AreEqual(parser.Normals[3], new Vector(1, 2, 3));
        }

        [TestMethod]
        public void TestObjFileNormalFaces()
        {
            var parser = new ObjFileParser(@"..\..\..\..\..\ObjFiles\NormalFacesTest.obj");
            Group g = parser.DefaultGroup;
            var t1 = g[0] as SmoothTriangle;
            var t2 = g[1] as SmoothTriangle;

            Assert.AreEqual(t1.P1, parser.Vertices[1]);
            Assert.AreEqual(t1.P2, parser.Vertices[2]);
            Assert.AreEqual(t1.P3, parser.Vertices[3]);
            Assert.AreEqual(t1.N1, parser.Normals[3]);
            Assert.AreEqual(t1.N2, parser.Normals[1]);
            Assert.AreEqual(t1.N3, parser.Normals[2]);
            Assert.AreEqual(t2, t1);
        }
    }
}
