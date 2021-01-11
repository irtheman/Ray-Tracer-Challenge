using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    [TestClass]
    public class PatternTest
    {
        [TestMethod]
        public void TestPatternTest()
        {
            var pattern = new TestPattern();
            Assert.AreEqual(pattern.Transform, Matrix.Identity);
        }

        [TestMethod]
        public void TestPatternApplyTransform()
        {
            var pattern = new TestPattern();
            pattern.Transform = Matrix.Translation(1, 2, 3);
            Assert.AreEqual(pattern.Transform, Matrix.Translation(1, 2, 3));
        }

        [TestMethod]
        public void TestPatternCreate()
        {
            var pattern = new StripePattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.a, Color.White);
            Assert.AreEqual(pattern.b, Color.Black);
        }

        [TestMethod]
        public void TestPatternY()
        {
            var pattern = new StripePattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(Point.PointY), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 2, 0)), Color.White);
        }

        [TestMethod]
        public void TestPatternZ()
        {
            var pattern = new StripePattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(Point.PointZ), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0, 2)), Color.White);
        }

        [TestMethod]
        public void TestPatternX()
        {
            var pattern = new StripePattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, 0, 0)), Color.White);
            Assert.AreEqual(pattern.PatternAt(Point.PointX), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.1, 0, 0)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-1, 0, 0)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-1.1, 0, 0)), Color.White);
        }

        [TestMethod]
        public void TestPatternObjectTransform()
        {
            var obj = new Sphere();
            obj.Transform = Matrix.Scaling(2, 2, 2);
            var pattern = new TestPattern();
            var c = pattern.PatternAtObject(obj, new Point(2, 3, 4));

            Assert.AreEqual(c, new Color(1, 1.5, 2));
        }

        [TestMethod]
        public void TestPatternPatternTransform()
        {
            var obj = new Sphere();
            var pattern = new TestPattern();
            pattern.Transform = Matrix.Scaling(2, 2, 2);
            var c = pattern.PatternAtObject(obj, new Point(2, 3, 4));

            Assert.AreEqual(c, new Color(1, 1.5, 2));
        }

        [TestMethod]
        public void TestPatternObjectPatternTransform()
        {
            var obj = new Sphere();
            obj.Transform = Matrix.Scaling(2, 2, 2);
            var pattern = new TestPattern();
            pattern.Transform = Matrix.Translation(0.5, 1, 1.5);
            var c = pattern.PatternAtObject(obj, new Point(2.5, 3, 3.5));

            Assert.AreEqual(c, new Color(0.75, 0.5, 0.25));
        }

        [TestMethod]
        public void TestPatternGradientCreate()
        {
            var pattern = new GradientPattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0.25, 0, 0)), new Color(0.75, 0.75, 0.75));
            Assert.AreEqual(pattern.PatternAt(new Point(0.5, 0, 0)), new Color(0.5, 0.5, 0.5));
            Assert.AreEqual(pattern.PatternAt(new Point(0.75, 0, 0)), new Color(0.25, 0.25, 0.25));
        }

        [TestMethod]
        public void TestPatternRingCreate()
        {
            var pattern = new RingPattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(Point.PointX), Color.Black);
            Assert.AreEqual(pattern.PatternAt(Point.PointZ), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(0.708, 0, 0.708)), Color.Black);
        }

        [TestMethod]
        public void TestPatternCheckerX()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0.99, 0, 0)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(1.01, 0, 0)), Color.Black);
        }

        [TestMethod]
        public void TestPatternCheckerY()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0.99, 0)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 1.01, 0)), Color.Black);
        }

        [TestMethod]
        public void TestPatternCheckerZ()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);

            Assert.AreEqual(pattern.PatternAt(Point.Zero), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0, 0.99)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0, 1.01)), Color.Black);
        }

        [TestMethod]
        public void TestPatternCheckerPattern2D()
        {
            var checkers = new CheckersPattern(2, 2, Color.Black, Color.White);

            Assert.AreEqual(checkers.UVPatternAt(0.0, 0.0), Color.Black);
            Assert.AreEqual(checkers.UVPatternAt(0.5, 0.0), Color.White);
            Assert.AreEqual(checkers.UVPatternAt(0.0, 0.5), Color.White);
            Assert.AreEqual(checkers.UVPatternAt(0.5, 0.5), Color.Black);
            Assert.AreEqual(checkers.UVPatternAt(1.0, 1.0), Color.Black);
        }

        [TestMethod]
        public void TestPatternSphericalMapping3D()
        {
            (double u, double v) uv;

            uv = TextureMap.SphericalMap(new Point(0, 0, -1));
            Assert.AreEqual(uv, (0.0, 0.5));

            uv = TextureMap.SphericalMap(new Point(1, 0, 0));
            Assert.AreEqual(uv, (0.25, 0.5));

            uv = TextureMap.SphericalMap(new Point(0, 0, 1));
            Assert.AreEqual(uv, (0.5, 0.5));

            uv = TextureMap.SphericalMap(new Point(-1, 0, 0));
            Assert.AreEqual(uv, (0.75, 0.5));

            uv = TextureMap.SphericalMap(new Point(0, 1, 0));
            Assert.AreEqual(uv, (0.5, 1.0));

            uv = TextureMap.SphericalMap(new Point(0, -1, 0));
            Assert.AreEqual(uv, (0.5, 0.0));

            uv = TextureMap.SphericalMap(new Point(MathHelper.SQRT2 / 2.0, MathHelper.SQRT2 / 2.0, 0.0));
            Assert.AreEqual(uv, (0.25, 0.75));
        }

        [TestMethod]
        public void TestPatternTextureMapPatternWithSphericalMap()
        {
            var checkers = new CheckersPattern(16, 8, Color.Black, Color.White);
            var pattern = new TextureMap(checkers, Mapping.Spherical);

            Assert.AreEqual(pattern.PatternAt(new Point(0.4315, 0.4670, 0.7719)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9654, 0.2552, -0.0534)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(0.1039, 0.7090, 0.6975)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.4986, -0.7856, -0.3663)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.0317, -0.9395, 0.3411)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(0.4809, -0.7721, 0.4154)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(0.0285, -0.9612, -0.2745)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.5734, -0.2162, -0.7903)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(0.7688, -0.1470, 0.6223)), Color.Black);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.7652, 0.2175, 0.6060)), Color.Black);
        }

        [TestMethod]
        public void TestPatternPlanarMapping3D()
        {
            (double u, double v) uv;

            uv = TextureMap.PlanarMap(new Point(0.25, 0, 0.5));
            Assert.AreEqual(uv, (0.25, 0.5));

            uv = TextureMap.PlanarMap(new Point(0.25, 0, -0.25));
            Assert.AreEqual(uv, (0.25, 0.75));

            uv = TextureMap.PlanarMap(new Point(0.25, 0.5, -0.25));
            Assert.AreEqual(uv, (0.25, 0.75));

            uv = TextureMap.PlanarMap(new Point(1.25, 0, 0.5));
            Assert.AreEqual(uv, (0.25, 0.5));

            uv = TextureMap.PlanarMap(new Point(0.25, 0, -1.75));
            Assert.AreEqual(uv, (0.25, 0.25));

            uv = TextureMap.PlanarMap(new Point(1, 0, -1));
            Assert.AreEqual(uv, (0.0, 0.0));

            uv = TextureMap.PlanarMap(new Point(0, 0, 0));
            Assert.AreEqual(uv, (0.0, 0.0));
        }

        [TestMethod]
        public void TestPatternCylindricalMapping3D()
        {
            (double u, double v) uv;

            uv = TextureMap.CylindricalMap(new Point(0, 0, -1));
            Assert.AreEqual(uv, (0.0, 0.0));

            uv = TextureMap.CylindricalMap(new Point(0, 0.5, -1));
            Assert.AreEqual(uv, (0.0, 0.5));

            uv = TextureMap.CylindricalMap(new Point(0, 1, -1));
            Assert.AreEqual(uv, (0.0, 0.0));

            uv = TextureMap.CylindricalMap(new Point(0.70711, 0.5, -0.70711));
            Assert.AreEqual(uv, (0.125, 0.5));

            uv = TextureMap.CylindricalMap(new Point(1, 0.5, 0));
            Assert.AreEqual(uv, (0.25, 0.5));

            uv = TextureMap.CylindricalMap(new Point(0.70711, 0.5, 0.70711));
            Assert.AreEqual(uv, (0.375, 0.5));

            uv = TextureMap.CylindricalMap(new Point(0, -0.25, 1));
            Assert.AreEqual(uv, (0.5, 0.75));

            uv = TextureMap.CylindricalMap(new Point(-0.70711, 0.5, 0.70711));
            Assert.AreEqual(uv, (0.625, 0.5));

            uv = TextureMap.CylindricalMap(new Point(-1, 1.25, 0));
            Assert.AreEqual(uv, (0.75, 0.25));

            uv = TextureMap.CylindricalMap(new Point(-0.70711, 0.5, -0.70711));
            Assert.AreEqual(uv, (0.875, 0.5));
        }

        [TestMethod]
        public void TestPatternAlignCheckCreate()
        {
            var pattern = new UVAlignCheck(Color.White, Color.Red, Color.Yellow, Color.Green, Color.Cyan);

            Assert.AreEqual(pattern.UVPatternAt(0.5, 0.5), Color.White);
            Assert.AreEqual(pattern.UVPatternAt(0.1, 0.9), Color.Red);
            Assert.AreEqual(pattern.UVPatternAt(0.9, 0.9), Color.Yellow);
            Assert.AreEqual(pattern.UVPatternAt(0.1, 0.1), Color.Green);
            Assert.AreEqual(pattern.UVPatternAt(0.9, 0.1), Color.Cyan);
        }

        [TestMethod]
        public void TestPatternCubeMapIdentifyFace()
        {
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(-1, 0.5, -0.25)), CubeFace.Left);
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(1.1, -0.75, 0.8)), CubeFace.Right);
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(0.1, 0.6, 0.9)), CubeFace.Front);
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(-0.7, 0, -2)), CubeFace.Back);
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(0.5, 1, 0.9)), CubeFace.Up);
            Assert.AreEqual(CubeMapPattern.FaceFromPoint(new Point(-0.2, -1.3, 1.1)), CubeFace.Down);
        }

        [TestMethod]
        public void TestPatternCubeMapFrontFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVFront(new Point(-0.5, 0.5, 1)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVFront(new Point(0.5, -0.5, 1)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapBackFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVBack(new Point(0.5, 0.5, -1)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVBack(new Point(-0.5, -0.5, -1)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapLeftFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVLeft(new Point(-1, 0.5, -0.5)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVLeft(new Point(-1, -0.5, 0.5)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapRightFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVRight(new Point(1, 0.5, 0.5)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVRight(new Point(1, -0.5, -0.5)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapUpperFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVUpper(new Point(-0.5, 1.0, -0.5)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVUpper(new Point(0.5, 1.0, 0.5)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapLowerFace()
        {
            Assert.AreEqual(CubeMapPattern.CubeUVLower(new Point(-0.5, -1.0, 0.5)), (0.25, 0.75));
            Assert.AreEqual(CubeMapPattern.CubeUVLower(new Point(0.5, -1, -0.5)), (0.75, 0.25));
        }

        [TestMethod]
        public void TestPatternCubeMapFindColors()
        {
            var left = new UVAlignCheck(Color.Yellow, Color.Cyan, Color.Red, Color.Blue, Color.Brown);
            var front = new UVAlignCheck(Color.Cyan, Color.Red, Color.Yellow, Color.Brown, Color.Green);
            var right = new UVAlignCheck(Color.Red, Color.Yellow, Color.Purple, Color.Green, Color.White);
            var back = new UVAlignCheck(Color.Green, Color.Purple, Color.Cyan, Color.White, Color.Blue);
            var up = new UVAlignCheck(Color.Brown, Color.Cyan, Color.Purple, Color.Red, Color.Yellow);
            var down = new UVAlignCheck(Color.Purple, Color.Brown, Color.Green, Color.Blue, Color.White);
            var pattern = new CubeMapPattern(left, front, right, back, up, down);

            // Left
            Assert.AreEqual(pattern.PatternAt(new Point(-1, 0, 0)), Color.Yellow);
            Assert.AreEqual(pattern.PatternAt(new Point(-1, 0.9, -0.9)), Color.Cyan);
            Assert.AreEqual(pattern.PatternAt(new Point(-1, 0.9, 0.9)), Color.Red);
            Assert.AreEqual(pattern.PatternAt(new Point(-1, -0.9, -0.9)), Color.Blue);
            Assert.AreEqual(pattern.PatternAt(new Point(-1, -0.9, 0.9)), Color.Brown);

            // Front
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0, 1)), Color.Cyan);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, 0.9, 1)), Color.Red);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, 0.9, 1)), Color.Yellow);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, -0.9, 1)), Color.Brown);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, -0.9, 1)), Color.Green);
            
            // Right
            Assert.AreEqual(pattern.PatternAt(new Point(1, 0, 0)), Color.Red);
            Assert.AreEqual(pattern.PatternAt(new Point(1, 0.9, 0.9)), Color.Yellow);
            Assert.AreEqual(pattern.PatternAt(new Point(1, 0.9, -0.9)), Color.Purple);
            Assert.AreEqual(pattern.PatternAt(new Point(1, -0.9, 0.9)), Color.Green);
            Assert.AreEqual(pattern.PatternAt(new Point(1, -0.9, -0.9)), Color.White);
            
            // Back
            Assert.AreEqual(pattern.PatternAt(new Point(0, 0, -1)), Color.Green);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, 0.9, -1)), Color.Purple);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, 0.9, -1)), Color.Cyan);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, -0.9, -1)), Color.White);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, -0.9, -1)), Color.Blue);
            
            // Upper
            Assert.AreEqual(pattern.PatternAt(new Point(0, 1, 0)), Color.Brown);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, 1, -0.9)), Color.Cyan);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, 1, -0.9)), Color.Purple);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, 1, 0.9)), Color.Red);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, 1, 0.9)), Color.Yellow);
            
            // Lower
            Assert.AreEqual(pattern.PatternAt(new Point(0, -1, 0)), Color.Purple);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, -1, 0.9)), Color.Brown);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, -1, 0.9)), Color.Green);
            Assert.AreEqual(pattern.PatternAt(new Point(-0.9, -1, -0.9)), Color.Blue);
            Assert.AreEqual(pattern.PatternAt(new Point(0.9, -1, -0.9)), Color.White);
        }

        [TestMethod]
        public void TestPatternCheckerPPM()
        {
            string ppm = @"P3
    10 10
    10
    0 0 0  1 1 1  2 2 2  3 3 3  4 4 4  5 5 5  6 6 6  7 7 7  8 8 8  9 9 9
    1 1 1  2 2 2  3 3 3  4 4 4  5 5 5  6 6 6  7 7 7  8 8 8  9 9 9  0 0 0
    2 2 2  3 3 3  4 4 4  5 5 5  6 6 6  7 7 7  8 8 8  9 9 9  0 0 0  1 1 1
    3 3 3  4 4 4  5 5 5  6 6 6  7 7 7  8 8 8  9 9 9  0 0 0  1 1 1  2 2 2
    4 4 4  5 5 5  6 6 6  7 7 7  8 8 8  9 9 9  0 0 0  1 1 1  2 2 2  3 3 3
    5 5 5  6 6 6  7 7 7  8 8 8  9 9 9  0 0 0  1 1 1  2 2 2  3 3 3  4 4 4
    6 6 6  7 7 7  8 8 8  9 9 9  0 0 0  1 1 1  2 2 2  3 3 3  4 4 4  5 5 5
    7 7 7  8 8 8  9 9 9  0 0 0  1 1 1  2 2 2  3 3 3  4 4 4  5 5 5  6 6 6
    8 8 8  9 9 9  0 0 0  1 1 1  2 2 2  3 3 3  4 4 4  5 5 5  6 6 6  7 7 7
    9 9 9  0 0 0  1 1 1  2 2 2  3 3 3  4 4 4  5 5 5  6 6 6  7 7 7  8 8 8";

            var canvas = Canvas.SetPPM(ppm);
            var pattern = new UVImage(canvas);

            Assert.AreEqual(pattern.UVPatternAt(0, 0), new Color(0.9, 0.9, 0.9));
            Assert.AreEqual(pattern.UVPatternAt(0.3, 0), new Color(0.2, 0.2, 0.2));
            Assert.AreEqual(pattern.UVPatternAt(0.6, 0.3), new Color(0.1, 0.1, 0.1));
            Assert.AreEqual(pattern.UVPatternAt(1, 1), new Color(0.9, 0.9, 0.9));
        }
    }
}
