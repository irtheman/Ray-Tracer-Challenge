using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp;

namespace CSharpTest
{
    [TestClass]
    public class YamlTest
    {
        [TestMethod]
        public void TestYamlIgnoreComments()
        {
            string yaml = @"
# Comment 1
# Comment 2

# Comment 3

      # Add a test
";

            //Assert.Fail();
        }
    }
}
