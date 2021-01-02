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

        public RTObject Parent { get; set; }

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

        public abstract BoundingBox BoundsOf { get; }

        public BoundingBox ParentSpaceBoundsOf => BoundsOf.Transform(transform);

        public Intersections Intersect(Ray r)
        {
            var localRay = r.Transform(transformInverse);
            return LocalIntersect(localRay);
        }

        protected abstract Intersections LocalIntersect(Ray ray);

        public Vector NormalAt(Point p, Intersection i = null)
        {
            var localPoint = WorldToObject(p);
            var localNormal = LocalNormal(localPoint, i);
            var worldNormal = NormalToWorld(localNormal);
         
            return worldNormal;
        }

        protected abstract Vector LocalNormal(Point p, Intersection hit);

        public override bool Equals(object obj)
        {
            var other = obj as RTObject;
            return ((other != null) &&
                    (Material.Equals(other.Material)) &&
                    (Transform.Equals(other.Transform)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Material, Transform);
        }

        public Point WorldToObject(Point p)
        {
            if (Parent != null)
            {
                p = Parent.WorldToObject(p);
            }

            return transformInverse * p;
        }

        public Vector NormalToWorld(Vector n)
        {
            n = transformInverse.Transpose * n;
            n.ClearW();
            n = n.Normalize;

            if (Parent != null)
            {
                n = Parent.NormalToWorld(n);
            }

            return n;
        }

        public virtual void Divide(int threshold)
        {
            // Nothing to do.
        }
    }
}
