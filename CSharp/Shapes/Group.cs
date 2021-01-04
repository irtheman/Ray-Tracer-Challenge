using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp
{
    public class Group : RTObject, IEnumerable<RTObject>
    {
        private BoundingBox _bounds;
        private List<RTObject> objects;

        public Group()
        {
            objects = new List<RTObject>();
            _bounds = null;
        }

        public int Count => objects.Count;

        public override BoundingBox BoundsOf
        {
            get
            {
                if (_bounds == null)
                {
                    _bounds = new BoundingBox();

                    foreach (var child in objects)
                    {
                        var cbox = child.ParentSpaceBoundsOf;
                        _bounds.Add(cbox);
                    }
                }

                return _bounds;
            }
        }

        public RTObject this[int index]
        {
            get
            {
                return objects[index];
            }
        }

        public void Add(RTObject obj)
        {
            obj.Parent = this;
            if (obj is Group || obj is CSG)
            {
                objects.Insert(0, obj);
            }
            else
            {
                objects.Add(obj);
            }
            _bounds = null;
        }

        public RTObject FindById(int id)
        {
            foreach (var obj in objects)
            {
                if (obj.ID == id)
                {
                    return obj;
                }

                if (obj is Group temp)
                {
                    RTObject found = temp.FindById(id);
                    if ((found != null) && (found.ID == id))
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        public bool Contains(RTObject obj)
        {
            return objects.Contains(obj);
        }

        protected override Intersections LocalIntersect(Ray ray)
        {
            Intersections results = new Intersections();

            if (BoundsOf.Intersects(ray))
            {
                foreach (var obj in objects)
                {
                    results.Add(obj.Intersect(ray));
                }
            }

            return results;
        }

        protected override Vector LocalNormal(Point p, Intersection hit)
        {
            throw new System.NotImplementedException();
        }

        public void PartitionChildren(out List<RTObject> left, out List<RTObject> right)
        {
            left = new List<RTObject>();
            right = new List<RTObject>();

            BoundingBox boundsLeft, boundsRight;
            BoundsOf.SplitBounds(out boundsLeft, out boundsRight);

            List<RTObject> remove = new List<RTObject>();
            foreach (var child in objects)
            {
                if (boundsLeft.Contains(child.ParentSpaceBoundsOf))
                {
                    left.Add(child);
                    remove.Add(child);
                }
                else if (boundsRight.Contains(child.ParentSpaceBoundsOf))
                {
                    right.Add(child);
                    remove.Add(child);
                }
            }

            foreach (var item in remove)
            {
                objects.Remove(item);
            }
        }

        public void MakeSubGroup(IEnumerable<RTObject> children)
        {
            var g = new Group();
            foreach (var child in children)
            {
                g.Add(child);
            }

            Add(g);
        }

        public override void Divide(int threshold)
        {
            if (threshold <= Count)
            {
                List<RTObject> left, right;
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

        public IEnumerator<RTObject> GetEnumerator()
        {
            foreach (var obj in objects)
                yield return obj;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Group;
            return base.Equals(other) &&
                   objects.Equals(other.objects);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("Group", base.GetHashCode(), objects);
        }

        public override string ToString()
        {
            return $"Group ({ID}): {Count}";
        }
    }
}
