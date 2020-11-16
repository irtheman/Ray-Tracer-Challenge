using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Intersections : List<Intersection>
    {
        public Intersections()
        {
            // Nothing to do
        }

        public Intersections(Intersection one, Intersection two)
        {
            this.Add(one);
            this.Add(two);
        }

        public Intersections(Intersection one, Intersection two, Intersection three, Intersection four)
        {
            this.Add(one);
            this.Add(two);
            this.Add(three);
            this.Add(four);
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
    }
}
