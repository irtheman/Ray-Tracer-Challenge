using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class ColorTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestColorCreate()
        {
            var c = new Color(-0.5, 0.4, 1.7);
            Assert.AreEqual(c.red, -0.5, epsilon);
            Assert.AreEqual(c.green, 0.4, epsilon);
            Assert.AreEqual(c.blue, 1.7, epsilon);
        }

        [TestMethod]
        public void TestColorAdd()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            Assert.AreEqual(c1 + c2, new Color(1.6, 0.7, 1.0));
        }

        [TestMethod]
        public void TestColorSubtract()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            Assert.AreEqual(c1 - c2, new Color(0.2, 0.5, 0.5));
        }

        [TestMethod]
        public void TestColorMultiplyScalar()
        {
            var c = new Color(0.2, 0.3, 0.4);
            Assert.AreEqual(c * 2, new Color(0.4, 0.6, 0.8));
        }


        [TestMethod]
        public void TestColorMultiply()
        {
            var c1 = new Color(1, 0.2, 0.4);
            var c2 = new Color(0.9, 1, 0.1);
            Assert.AreEqual(c1 * c2, new Color(0.9, 0.2, 0.04));
        }
    }
}
