using System.Collections;
using System.Collections.Generic;

namespace CSharp
{
    public class Values : IEnumerable<Values>
    {
        public Values()
        {
            Key = string.Empty;
            String = string.Empty;
            List = new List<Values>();
        }

        public Values(string key, string value) : this()
        {
            Key = key;
            String = value;
        }

        public string Key { get; set; }
        public bool HasList => List.Count > 0;
        public bool IsEmpty => List.Count == 0 && Key.Length == 0 && String.Length == 0;
        public string String { get; set; }
        public List<Values> List { get; private set; }

        public Values this[int index]
        {
            get
            {
                Values value = null;
                if (index < List.Count)
                {
                    value = List[index];
                }
                else
                {
                    value = new Values(string.Empty, string.Empty);
                }

                return value;
            }
        }

        public Values this[string index]
        {
            get
            {
                Values temp = new Values(string.Empty, string.Empty);
                var key = index.ToLower();

                foreach (var item in List)
                {
                    if (item.Key.ToLower().Equals(key))
                    {
                        temp = item;
                        break;
                    }
                }

                return temp;
            }
        }

        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(Key))
            {
                Key = key;
                String = value;
            }
            else
            {
                var v = new Values(key, value);
                List.Add(v);
            }
        }

        public void Add(Values value)
        {
            List.Add(value);
        }

        public void Add(string s)
        {
            List.Add(new Values(string.Empty, s));
        }

        public void Add(string key, Values temp)
        {
            var value = new Values(key, string.Empty);
            foreach (var v in temp.List)
            {
                value.Add(v);
            }
            List.Add(value);
        }

        public int ToInt()
        {
            return int.Parse(String);
        }

        public double ToDouble()
        {
            return double.Parse(String);
        }

        public override string ToString()
        {
            return $"{Key}: {String} ({List.Count})";
        }

        public IEnumerator<Values> GetEnumerator()
        {
            foreach (var item in List)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
