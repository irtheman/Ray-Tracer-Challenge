using System;

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
            Parent = null;
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

        public RTObject Parent { get; set; }

        public Intersections Intersect(Ray r)
        {
            var localRay = r.Transform(transformInverse);
            return LocalIntersect(localRay);
        }

        protected abstract Intersections LocalIntersect(Ray ray);

        public Vector NormalAt(Point p, Intersection i = null)
        {
            var localPoint = WorldToObject(p);
            var localNormal = LocalNormalAt(localPoint, i);
            var worldNormal = NormalToWorld(localNormal);
         
            return worldNormal;
        }

        protected abstract Vector LocalNormalAt(Point p, Intersection i);

        public Point WorldToObject(Point point)
        {
            if (Parent != null)
            {
                point = Parent.WorldToObject(point);
            }

            return transformInverse * point;
        }

        public Vector NormalToWorld(Vector normal)
        {
            normal = (transformInverse.Transpose * normal).Normalize;

            if (Parent != null)
            {
                normal = Parent.NormalToWorld(normal);
            }

            return normal;
        }

        public abstract BoundingBox Bounds { get; }

        public BoundingBox ParentSpaceBounds()
        {
            return Bounds.Transform(Transform);
        }

        public override bool Equals(object obj)
        {
            var other = obj as RTObject;
            return ((other != null) &&
                    (Material.Equals(other.Material)) &&
                    (Transform.Equals(other.Transform)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(transform, Material);
        }
    }
}
