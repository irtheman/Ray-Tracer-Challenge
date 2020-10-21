using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class TupleTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestTupleAsPoint()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 1.0);
            Assert.AreEqual(a.x, 4.3, epsilon);
            Assert.AreEqual(a.y, -4.2, epsilon);
            Assert.AreEqual(a.z, 3.1, epsilon);
            Assert.AreEqual(a.w, 1.0, epsilon);
            Assert.IsTrue(a.IsPoint);
            Assert.IsFalse(a.IsVector);
        }

        [TestMethod]
        public void TestTupleAsVector()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);
            Assert.AreEqual(a.x, 4.3, epsilon);
            Assert.AreEqual(a.y, -4.2, epsilon);
            Assert.AreEqual(a.z, 3.1, epsilon);
            Assert.AreEqual(a.w, 0.0, epsilon);
            Assert.IsFalse(a.IsPoint);
            Assert.IsTrue(a.IsVector);
        }

        [TestMethod]
        public void TestCreatePoint()
        {
            var p = new Point(4, -4, 3);
            Assert.AreEqual(p, new Tuple(4, -4, 3, 1));
        }

        [TestMethod]
        public void TestCreateVector()
        {
            var v = new Vector(4, -4, 3);
            Assert.AreEqual(v, new Tuple(4, -4, 3, 0));
        }
    }
}
