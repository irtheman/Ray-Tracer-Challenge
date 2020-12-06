using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public abstract class RTPattern
    {
        protected Matrix transform;
        protected Matrix transformInverse;

        public RTPattern(Color a, Color b)
        {
            Transform = Matrix.Identity;
            this.a = a;
            this.b = b;
        }

        public Color a { get; set; }
        public Color b { get; set; }
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

        public abstract Color PatternAt(Point p);

        public Color PatternAtObject(RTObject obj, Point worldPoint)
        {
            var localPoint = obj.WorldToObject(worldPoint); //obj.TransformInverse * worldPoint;
            return PatternAtShape(obj, localPoint);
        }

        protected abstract Color PatternAtShape(RTObject obj, Point localPoint);
    }
}
