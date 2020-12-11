using System.Collections.Specialized;
using System.IO;

namespace CSharp
{
    public partial class YamlParser
    {
        private Values yaml;

        public YamlParser()
        {
            yaml = new Values();
        }

        public bool Parse(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                return false;
            }

            var lines = File.ReadAllLines(fullPath);
            int index = 0;

            ParseYaml(yaml, 2, lines, ref index);

            return true;
        }

        public Values Root => yaml;

        private void ParseYaml(Values top, int tab, string[] lines, ref int index)
        {
            Values current = top;
            int currentTab = -1;

            while (index < lines.Length)
            {
                var ln = CleanLine(lines[index], out currentTab);
                if (string.IsNullOrWhiteSpace(ln))
                {
                    index += 1;
                    continue;
                }

                if (currentTab < tab)
                {
                    return;
                }
                else if (currentTab > tab)
                {
                    Values next = current.List[current.List.Count - 1];
                    ParseYaml(next, currentTab, lines, ref index);
                    continue;
                }

                index += 1;

                string key, value;
                var parts = ln.Split(':');
                if (parts.Length > 1)
                {
                    key = parts[0];
                    value = parts[1];
                }
                else
                {
                    key = string.Empty;
                    value = parts[0];
                }

                if (key.StartsWith('-'))
                {
                    current = new Values();
                    top.Add(current);
                    key = key.Substring(1);
                }

                if (value.StartsWith("-[") || value.StartsWith("["))
                {
                    int i = value.IndexOf('[');
                    value = value.Substring(i + 1, value.Length - i - 2);
                    var segs = value.Split(',');
                    var temp = new Values();

                    foreach (var s in segs)
                    {
                        temp.Add(s);
                    }

                    current.Add(key, temp);
                }
                else
                {
                    current.Add(key, value);
                }
            }
        }

        private string CleanLine(string l, out int t)
        {
            t = 0;

            // Remove comments from line
            var index = l.IndexOf("#");
            if (index > -1)
            {
                l = l.Remove(index);
            }

            // Remove trailing whitespace from line
            l = l.TrimEnd();
            if (string.IsNullOrEmpty(l)) return l;

            // Count initial spaces
            while ((t < l.Length) && (l[t] == ' '))
                t += 1;
            if (l[t] == '-') t += 2;

            // Remove initial spaces and all blanks
            l = l.TrimStart().Replace(" ", string.Empty);

            return l;
        }
    }
}
