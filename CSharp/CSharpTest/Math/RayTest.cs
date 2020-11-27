using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class RayTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestRayToPoint()
        {
            var r = new Ray(new Point(2, 3, 4), Vector.VectorX);
            Assert.AreEqual(r.Position(0), new Point(2, 3, 4));
            Assert.AreEqual(r.Position(1), new Point(3, 3, 4));
            Assert.AreEqual(r.Position(-1), new Point(1, 3, 4));
            Assert.AreEqual(r.Position(2.5), new Point(4.5, 3, 4));
        }

        [TestMethod]
        public void TestRayTranslation()
        {
            var r = new Ray(new Point(1, 2, 3), Vector.VectorY);
            var m = Matrix.Translation(3, 4, 5);
            var r2 = r.Transform(m);

            Assert.AreEqual(r2.Origin, new Point(4, 6, 8));
            Assert.AreEqual(r2.Direction, Vector.VectorY);
        }

        [TestMethod]
        public void TestRayScaling()
        {
            var r = new Ray(new Point(1, 2, 3), Vector.VectorY);
            var m = Matrix.Scaling(2, 3, 4);
            var r2 = r.Transform(m);

            Assert.AreEqual(r2.Origin, new Point(2, 6, 12));
            Assert.AreEqual(r2.Direction, new Vector(0, 3, 0));
        }
    }
}
