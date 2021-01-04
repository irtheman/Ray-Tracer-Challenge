using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Intersections : IEnumerable<Intersection>
    {
        List<Intersection> intersections;

        public Intersections()
        {
            intersections = new List<Intersection>();
        }

        public Intersections(Intersection one) : this()
        {
            this.Add(one);
        }

        public Intersections(Intersection one, Intersection two) : this()
        {
            this.Add(one);
            this.Add(two);
        }

        public Intersections(Intersection one, Intersection two, Intersection three, Intersection four) : this()
        {
            this.Add(one);
            this.Add(two);
            this.Add(three);
            this.Add(four);
        }

        public long Count => intersections.Count;

        public void Add(Intersections items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Add(Intersection item)
        {
            int index = -1;

            for (int i = 0; i < intersections.Count; i++)
            {
                if (intersections[i].t >= item.t)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
                intersections.Add(item);
            else
                intersections.Insert(index, item);
        }

        public Intersection this[int index]
        {
            get
            {
                return intersections[index];
            }
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
            foreach (var item in intersections)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Intersection Hit
        {
            get
            {
                Intersection choice = null;
                foreach (var i in this)
                {
                    if (i.t >= 0)
                    {
                        if ((choice == null) || (i.t <= choice.t))
                        {
                            choice = i;
                        }
                    }
                }

                return choice;
            }
        }

        public override string ToString()
        {
            return $"Intersections: {Count}";
        }
    }
}
