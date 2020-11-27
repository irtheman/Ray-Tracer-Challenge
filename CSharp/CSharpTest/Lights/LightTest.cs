using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class LightTest
    {
        [TestMethod]
        public void TestPointLightCreate()
        {
            var intensity = new Color(1, 1, 1);
            var position = new Point(0, 0, 0);
            var light = new PointLight(position, intensity);

            Assert.AreEqual(light.Position, position);
            Assert.AreEqual(light.Intensity, intensity);
        }
    }
}
