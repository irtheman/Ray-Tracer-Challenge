using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class SequenceTest
    {
        private const double epsilon = 0.00001d;

        [TestMethod]
        public void TestSequenceCyclic()
        {
            var gen = Sequence.Generate(0.1, 0.5, 1.0);

            Assert.AreEqual(gen.Next, 0.1, epsilon);
            Assert.AreEqual(gen.Next, 0.5, epsilon);
            Assert.AreEqual(gen.Next, 1.0, epsilon);
            Assert.AreEqual(gen.Next, 0.1, epsilon);
        }
    }
}
