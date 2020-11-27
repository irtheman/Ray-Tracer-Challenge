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
            var intensity = Color.White;
            var position = Point.Zero;
            var light = new PointLight(position, intensity);

            Assert.AreEqual(light.Position, position);
            Assert.AreEqual(light.Intensity, intensity);
        }
    }
}
