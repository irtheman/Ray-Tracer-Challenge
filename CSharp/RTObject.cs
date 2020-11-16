using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CSharp
{
    public abstract class RTObject
    {
        protected RTObject()
        {
            Transform = Matrix.Identity;
        }

        public Matrix Transform { get; set; }
        public abstract Intersections Intersect(Ray r);
    }
}
