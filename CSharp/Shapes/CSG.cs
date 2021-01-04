using System;

namespace CSharp
{
    public enum CsgOperation
    {
        Union,
        Intersection,
        Difference
    }

    public class CSG : RTObject
    {
        private BoundingBox _bounds;

        public CSG(CsgOperation op, RTObject left, RTObject right)
        {
            Operation = op;
            Left = left;
            Right = right;

            Left.Parent = this;
            Right.Parent = this;
            _bounds = null;
        }


        public override BoundingBox BoundsOf
        {
            get
            {
                if (_bounds == null)
                {
                    _bounds = new BoundingBox();
                    var cbox = Left.ParentSpaceBoundsOf;
                    _bounds.Add(cbox);

                    cbox = Right.ParentSpaceBoundsOf;
                    _bounds.Add(cbox);
                }

                return _bounds;
            }
        }

        public CsgOperation Operation { get; }
        public RTObject Left { get; }
        public RTObject Right { get; }

        protected override Intersections LocalIntersect(Ray ray)
        {
            var results = new Intersections();

            if (BoundsOf.Intersects(ray))
            {
                var leftXs = Left.Intersect(ray);
                var rightXs = Right.Intersect(ray);

                results.Add(leftXs);
                results.Add(rightXs);
            }

            return FilterIntersections(results);
        }

        protected override Vector LocalNormal(Point p, Intersection i)
        {
            throw new System.NotImplementedException();
        }

        public override void Divide(int threshold)
        {
            Left.Divide(threshold);
            Right.Divide(threshold);
        }

        public static bool IntersectionAllowed(CsgOperation op, bool lhit, bool inl, bool inr)
        {
            if (op == CsgOperation.Union)
            {
                return (lhit && !inr) || (!lhit && !inl);
            }
            else if (op == CsgOperation.Intersection)
            {
                return (lhit && inr) || (!lhit && inl);
            }
            else if (op == CsgOperation.Difference)
            {
                return (lhit && !inr) || (!lhit && inl);
            }

            return false;
        }

        public Intersections FilterIntersections(Intersections xs)
        {
            var inl = false;
            var inr = false;

            var result = new Intersections();
            foreach (var i in xs)
            {
                var lhit = ChildContains(Left, i.Object);

                if (IntersectionAllowed(Operation, lhit, inl, inr))
                {
                    result.Add(i);
                }

                if (lhit)
                    inl = !inl;
                else
                    inr = !inr;
            }

            return result;
        }

        private bool ChildContains(RTObject child, RTObject obj)
        {
            if (child.Equals(obj))
            {
                return true;
            }

            var group = child as Group;
            if (group != null)
            {
                if (group.Contains(obj))
                {
                    return true;
                }
            }

            var csg = child as CSG;
            if (csg != null)
            {
                return ChildContains(csg.Left, obj) || ChildContains(csg.Right, obj);
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CSG;
            return base.Equals(other) &&
                   Operation.Equals(other.Operation) &&
                   Left.Equals(other.Left) &&
                   Right.Equals(other.Right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine("CSG", base.GetHashCode(), Operation, Left, Right);
        }

        public override string ToString()
        {
            return $"CSG ({ID}): {Operation}";
        }
    }
}
