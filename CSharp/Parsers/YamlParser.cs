using System.Collections.Specialized;
using System.IO;

namespace CSharp
{
    public class YamlParser
    {
        private OrderedDictionary dict;

        public YamlParser(string fullPath)
        {
            dict = new OrderedDictionary();
            Parse(fullPath);
        }

        private bool Parse(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                return false;
            }

            var lines = File.ReadAllLines(fullPath);
            int index = 0;
            int tab = -1;

            return false;
        }
    }
}
