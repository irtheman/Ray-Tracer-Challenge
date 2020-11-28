using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;
using System;

namespace CSharpTest
{
    public class TestPattern : RTPattern
    {
        public TestPattern() : base(Color.White, Color.Black)
        {
            // Nothing to do
        }

        public override Color PatternAt(Point p)
        {
            return new Color(p.x, p.y, p.z);
        }

        protected override Color PatternAtShape(RTObject obj, Point localPoint)
        {
            var patternPoint = TransformInverse * localPoint;
            return PatternAt(patternPoint);
        }
    }

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
    }
}
