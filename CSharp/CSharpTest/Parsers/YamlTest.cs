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

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root;
            Assert.IsNotNull(root);
            Assert.IsTrue(root.IsEmpty);
        }

        [TestMethod]
        public void TestYamlNewElement()
        {
            string yaml = @"
- add: camera
";

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root;
            Assert.IsFalse(root.IsEmpty);
            Assert.IsTrue(root.HasList);

            root = root.List[0];
            Assert.AreEqual(root.Key, "add");
            Assert.AreEqual(root.String, "camera");
        }

        [TestMethod]
        public void TestYamlNewElementWithParams()
        {
            string yaml = @"
- add: camera
  width: 400
  height: 200
";

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root;
            Assert.IsFalse(root.IsEmpty);
            Assert.IsTrue(root.HasList);

            root = root.List[0];
            Assert.AreEqual(root.List[0].Key, "width");
            Assert.AreEqual(root.List[0].String, "400");
            Assert.AreEqual(root.List[1].Key, "height");
            Assert.AreEqual(root.List[1].String, "200");
        }

        [TestMethod]
        public void TestYamlNewElementWithList()
        {
            string yaml = @"
- add: camera
  from: [1, 2, 3]
";

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root.List[0].List[0];
            Assert.AreEqual(root.Key, "from");

            Assert.IsTrue(root.HasList);
            Assert.AreEqual(root.List[0].ToInt(), 1);
            Assert.AreEqual(root.List[1].ToDouble(), 2.0);
            Assert.AreEqual(root.List[2].String, "3");
        }

        [TestMethod]
        public void TestYamlNewElementWithList2()
        {
            string yaml = @"
- add: sphere
  transform:
    - [ translate, 1, 2, 3]
";

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root.List[0].List[0];
            Assert.AreEqual(root.Key, "transform");

            Assert.IsTrue(root.HasList);
            root = root.List[0];

            Assert.AreEqual(root.List[0].String, "translate");
            Assert.AreEqual(root.List[1].ToInt(), 1);
            Assert.AreEqual(root.List[2].ToDouble(), 2.0);
            Assert.AreEqual(root.List[3].String, "3");
        }

        [TestMethod]
        public void TestYamlNewElementNested()
        {
            string yaml = @"
- add: sphere
  material:
    pattern:
      type: stripes
      colors:
        - [1, 0.5, 0]
        - [1, 0.3, 0]";

            var yml = new YamlParser();
            yml.ParseYaml(yaml);

            var root = yml.Root.List[0];
            Assert.AreEqual(root.Key, "add");
            Assert.AreEqual(root.String, "sphere");

            root = root.List[0];
            Assert.AreEqual(root.Key, "material");
            Assert.AreEqual(root.String, "");
            Assert.IsTrue(root.HasList);

            root = root.List[0];
            Assert.AreEqual(root.Key, "pattern");
            Assert.AreEqual(root.String, "");
            Assert.IsTrue(root.HasList);

            Assert.AreEqual(root.List[0].Key, "type");
            Assert.AreEqual(root.List[0].String, "stripes");
            Assert.IsFalse(root.List[0].HasList);

            Assert.AreEqual(root.List[1].Key, "colors");
            Assert.AreEqual(root.List[1].String, "");
            Assert.IsTrue(root.List[1].HasList);

            var c1 = root.List[1].List[0];
            var c2 = root.List[1].List[1];

            Assert.AreEqual(c1.List[0].String, "1");
            Assert.AreEqual(c1.List[1].String, "0.5");
            Assert.AreEqual(c1.List[2].String, "0");

            Assert.AreEqual(c2.List[0].String, "1");
            Assert.AreEqual(c2.List[1].String, "0.3");
            Assert.AreEqual(c2.List[2].String, "0");
        }
    }
}
