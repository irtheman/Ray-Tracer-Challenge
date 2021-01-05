using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class LightTest
    {
        private const double epsilon = 0.00001;

        [TestMethod]
        public void TestPointLightCreate()
        {
            var intensity = Color.White;
            var position = Point.Zero;
            var light = new PointLight(position, intensity);

            Assert.AreEqual(light.Position, position);
            Assert.AreEqual(light.Intensity, intensity);
        }

        [TestMethod]
        public void TestAreaLightCreate()
        {
            var corner = Point.Zero;
            var v1 = new Vector(2, 0, 0);
            var v2 = Vector.VectorZ;

            var light = new AreaLight(corner, v1, 4, v2, 2, Color.White);

            Assert.AreEqual(light.Corner, corner);
            Assert.AreEqual(light.UVec, new Vector(0.5, 0, 0));
            Assert.AreEqual(light.USteps, 4);
            Assert.AreEqual(light.VVec, new Vector(0, 0, 0.5));
            Assert.AreEqual(light.VSteps, 2);
            Assert.AreEqual(light.Samples, 8);
            Assert.AreEqual(light.Position, new Point(1, 0, 0.5));
        }

        [TestMethod]
        public void TestPointLightIntensityAtPoint()
        {
            var w = World.Default;
            var light = w.Lights[0];
            Point pt;

            pt = new Point(0, 1.0001, 0);
            Assert.AreEqual(light.IntensityAt(pt, w), 1.0, epsilon);

            pt = new Point(-1.0001, 0, 0);
            Assert.AreEqual(light.IntensityAt(pt, w), 1.0, epsilon);

            pt = new Point(0, 0, -1.0001);
            Assert.AreEqual(light.IntensityAt(pt, w), 1.0, epsilon);

            pt = new Point(0, 0, 1.0001);
            Assert.AreEqual(light.IntensityAt(pt, w), 0.0, epsilon);

            pt = new Point(1.0001, 0, 0);
            Assert.AreEqual(light.IntensityAt(pt, w), 0.0, epsilon);

            pt = new Point(0, -1.0001, 0);
            Assert.AreEqual(light.IntensityAt(pt, w), 0.0, epsilon);

            pt = new Point(0, 0, 0);
            Assert.AreEqual(light.IntensityAt(pt, w), 0.0, epsilon);
        }

        [TestMethod]
        public void TestAreaLightFindingSinglePoint()
        {
            var corner = Point.Zero;
            var v1 = new Vector(2, 0, 0);
            var v2 = Vector.VectorZ;

            var light = new AreaLight(corner, v1, 4, v2, 2, Color.White);
            light.JitterBy = Sequence.Generate(0.5, 0.5, 0.5);
            Point pt;

            pt = light.PointOnLight(0, 0);
            Assert.AreEqual(pt, new Point(0.25, 0, 0.25));

            pt = light.PointOnLight(1, 0);
            Assert.AreEqual(pt, new Point(0.75, 0, 0.25));

            pt = light.PointOnLight(0, 1);
            Assert.AreEqual(pt, new Point(0.25, 0, 0.75));

            pt = light.PointOnLight(2, 0);
            Assert.AreEqual(pt, new Point(1.25, 0, 0.25));

            pt = light.PointOnLight(3, 1);
            Assert.AreEqual(pt, new Point(1.75, 0, 0.75));
        }

        [TestMethod]
        public void TestAreaLightIntensityFunction()
        {
            var w = World.Default;
            var corner = new Point(-0.5, -0.5, -5);
            var v1 = Vector.VectorX;
            var v2 = Vector.VectorY;
            var light = new AreaLight(corner, v1, 2, v2, 2, Color.White);
            light.JitterBy = Sequence.Generate(0.5, 0.5, 0.5);

            Assert.AreEqual(light.IntensityAt(new Point(0, 0, 2), w), 0.0, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(1, -1, 2), w), 0.25, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(1.5, 0, 2), w), 0.5, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(1.25, 1.25, 3), w), 0.75, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(0, 0, -2), w), 1.0, epsilon);
        }

        [TestMethod]
        public void TestAreaLightFindingSinglePointJittered()
        {
            var corner = Point.Zero;
            var v1 = new Vector(2, 0, 0);
            var v2 = Vector.VectorZ;

            var light = new AreaLight(corner, v1, 4, v2, 2, Color.White);
            light.JitterBy = Sequence.Generate(0.3, 0.7);
            Point pt;

            pt = light.PointOnLight(0, 0);
            Assert.AreEqual(pt, new Point(0.15, 0, 0.35));

            pt = light.PointOnLight(1, 0);
            Assert.AreEqual(pt, new Point(0.65, 0, 0.35));

            pt = light.PointOnLight(0, 1);
            Assert.AreEqual(pt, new Point(0.15, 0, 0.85));

            pt = light.PointOnLight(2, 0);
            Assert.AreEqual(pt, new Point(1.15, 0, 0.35));

            pt = light.PointOnLight(3, 1);
            Assert.AreEqual(pt, new Point(1.65, 0, 0.85));
        }

        [TestMethod]
        public void TestAreaLightIntensityFunctionJittered()
        {
            var w = World.Default;
            var corner = new Point(-0.5, -0.5, -5);
            var v1 = Vector.VectorX;
            var v2 = Vector.VectorY;
            var light = new AreaLight(corner, v1, 2, v2, 2, Color.White);
            light.JitterBy = Sequence.Generate(0.7, 0.3, 0.9, 0.1, 0.5);

            Assert.AreEqual(light.IntensityAt(new Point(0, 0, 2), w), 0.0, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(1, -1, 2), w), 0.25, epsilon); // Note: Changed from 0.5
            Assert.AreEqual(light.IntensityAt(new Point(1.5, 0, 2), w), 0.75, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(1.25, 1.25, 3), w), 0.75, epsilon);
            Assert.AreEqual(light.IntensityAt(new Point(0, 0, -2), w), 1.0, epsilon);
        }
    }
}
