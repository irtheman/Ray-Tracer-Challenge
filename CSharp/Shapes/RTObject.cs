using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CSharp
{
    public abstract class RTObject
    {
        private Matrix transform;
        protected Matrix transformInverse;

        protected RTObject()
        {
            Transform = Matrix.Identity;
            Material = new Material();
            HasShadow = true;
        }

        public Material Material { get; set; }

        public Matrix Transform
        {
            get
            {
                return transform;
            }
            set
            {
                transform = value;
                transformInverse = transform.Inverse;
            }
        }

        public Matrix TransformInverse => transformInverse;

        public bool HasShadow { get; set; }

        public Intersections Intersect(Ray r)
        {
            var localRay = r.Transform(transformInverse);
            return LocalIntersect(localRay);
        }

        protected abstract Intersections LocalIntersect(Ray ray);

        public Vector Normal(Point p)
        {
            var localPoint = transformInverse * p;
            var localNormal = LocalNormal(localPoint);
            var worldNormal = new Vector(transformInverse.Transpose * localNormal);
         
            return worldNormal.Normalize;
        }

        protected abstract Vector LocalNormal(Point p);

        public override bool Equals(object obj)
        {
            var other = obj as RTObject;
            return ((other != null) &&
                    (Material.Equals(other.Material)) &&
                    (Transform.Equals(other.Transform)));
        }
    }
}
