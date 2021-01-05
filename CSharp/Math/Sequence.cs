using System;
using System.Collections.Generic;

namespace CSharp
{
    public class Sequence
    {
        private List<double> list = new List<double>();
        private int index = 0;
        private Random rand = new Random();

        public void Add(double d)
        {
            list.Add(d);
            rand = null;
        }

        public double Next
        {
            get
            {
                if (rand != null)
                {
                    return rand.NextDouble();
                }

                if (index >= list.Count)
                {
                    index = 0;
                }

                return list[index++];
            }
        }

        public static Sequence Generate(params double[] seq)
        {
            var ret = new Sequence();

            foreach (var num in seq)
            {
                ret.Add(num);
            }

            return ret;
        }
    }
}
