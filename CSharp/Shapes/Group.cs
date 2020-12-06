using System.Collections;
using System.Collections.Generic;

namespace CSharp
{
    public class Group : RTObject, IEnumerable<RTObject>
    {
        List<RTObject> objects;

        public Group()
        {
            objects = new List<RTObject>();
        }

        public long Count => objects.Count;

        public override BoundingBox Bounds
        {
            get
            {
                var box = new BoundingBox();

                foreach (var child in objects)
                {
                    var cbox = child.ParentSpaceBounds();
                    box.Add(cbox);
                }

                return box;
            }
        }

        public void Add(RTObject obj)
        {
            obj.Parent = this;
            objects.Add(obj);
        }

        public RTObject this[int index]
        {
            get
            {
                return objects[index];
            }
        }

        protected override Intersections LocalIntersect(Ray ray)
        {
            var intersections = new Intersections();

            foreach(var obj in objects)
            {
                intersections.Add(obj.Intersect(ray));
            }

            return intersections;
        }

        protected override Vector LocalNormal(Point p)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<RTObject> GetEnumerator()
        {
            foreach (var obj in objects)
                yield return obj;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
