using System.Collections;
using System.Collections.Generic;

namespace CSharp
{
    public class Group : RTObject, IEnumerable<RTObject>
    {
        private List<RTObject> objects;
        private BoundingBox box;


        public Group()
        {
            objects = new List<RTObject>();
            box = null;
        }

        public long Count => objects.Count;

        public override BoundingBox Bounds
        {
            get
            {
                if (box == null)
                {
                    box = new BoundingBox();

                    foreach (var child in objects)
                    {
                        var cbox = child.ParentSpaceBounds();
                        box.Add(cbox);
                    }
                }

                return box;
            }
        }

        public void Add(RTObject obj)
        {
            box = null;
            obj.Parent = this;
            objects.Add(obj);
        }

        public void Add(Group obj)
        {
            box = null;
            obj.Parent = this;
            Add((RTObject)obj);
        }

        public void Add(IEnumerable<RTObject> list)
        {
            foreach (var item in list)
                Add(item);
        }

        public RTObject this[int index]
        {
            get
            {
                return objects[index];
            }
        }

        public bool Contains(RTObject obj)
        {
            var ret = objects.Contains(obj);

            if (!ret)
            {
                foreach (var item in objects)
                {
                    var group = item as Group;
                    if (group != null)
                    {
                        ret = group.Contains(obj);
                        if (ret) break;
                    }
                }
            }

            return ret;
        }

        public override void Divide(int threshold)
        {
            if (threshold <= Count)
            {
                Group left, right;
                PartitionChildren(out left, out right);

                if (left.Count > 0)
                {
                    MakeSubGroup(left);
                }

                if (right.Count > 0)
                {
                    MakeSubGroup(right);
                }
            }

            foreach (var child in objects)
            {
                child.Divide(threshold);
            }
        }

        protected override Intersections LocalIntersect(Ray ray)
        {
            var intersections = new Intersections();

            if (Bounds.Intersects(ray))
            {
                foreach (var obj in objects)
                {
                    intersections.Add(obj.Intersect(ray));
                }
            }

            return intersections;
        }

        protected override Vector LocalNormalAt(Point p, Intersection i)
        {
            throw new System.NotImplementedException();
        }

        public void PartitionChildren(out Group left, out Group right)
        {
            left = new Group();
            right = new Group();

            BoundingBox boundsLeft, boundsRight;
            Bounds.SplitBounds(out boundsLeft, out boundsRight);

            foreach (var obj in objects)
            {
                if (boundsLeft.Contains(obj.ParentSpaceBounds()))
                {
                    left.Add(obj);
                }
                else if (boundsRight.Contains(obj.ParentSpaceBounds()))
                {
                    right.Add(obj);
                }
            }

            foreach (var obj in left)
            {
                objects.Remove(obj);
            }

            foreach (var obj in right)
            {
                objects.Remove(obj);
            }

            box = null;
        }

        public void MakeSubGroup(IEnumerable<RTObject> lists)
        {
            var group = new Group();
            group.Add(lists);

            Add(group);
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
