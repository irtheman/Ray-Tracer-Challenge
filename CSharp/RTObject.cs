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
            Material = new Material();
        }

        public Material Material { get; set; }
        public Matrix Transform { get; set; }
        public abstract Intersections Intersect(Ray r);
        public abstract Vector Normal(Point p);
    }
}
