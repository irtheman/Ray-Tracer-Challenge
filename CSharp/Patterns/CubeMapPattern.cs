using System;

namespace CSharp
{
    public enum CubeFace
    {
        None,
        Left,
        Right,
        Front,
        Back,
        Up,
        Down
    }

    public class CubeMapPattern : RTPattern
    {
        private Color c;
        private Color d;
        private Color e;
        private RTPattern left;
        private RTPattern front;
        private RTPattern right;
        private RTPattern back;
        private RTPattern up;
        private RTPattern down;

        public CubeMapPattern(Color main, Color ul) : base(main, ul)
        {
            // Nothing to do here
        }

        public CubeMapPattern(Color main, Color ul, Color ur, Color bl, Color br) : base(main, ul)
        {
            c = ur;
            d = bl;
            e = br;
        }

        public CubeMapPattern(RTPattern left, RTPattern front, RTPattern right, RTPattern back, RTPattern up, RTPattern down) : base(Color.Black, Color.Black)
        {
            this.left = left;
            this.front = front;
            this.right = right;
            this.back = back;
            this.up = up;
            this.down = down;
        }

        public override Color PatternAt(Point p)
        {
            var face = FaceFromPoint(p);
            (double u, double v) uv;

            switch (face)
            {
                case CubeFace.Left:
                    uv = CubeUVLeft(p);
                    break;
                case CubeFace.Right:
                    uv = CubeUVRight(p);
                    break;
                case CubeFace.Front:
                    uv = CubeUVFront(p);
                    break;
                case CubeFace.Back:
                    uv = CubeUVBack(p);
                    break;
                case CubeFace.Up:
                    uv = CubeUVUpper(p);
                    break;
                case CubeFace.Down:
                    uv = CubeUVLower(p);
                    break;
                default:
                    uv = (0, 0);
                    break;
            }

            return UVPatternAt(uv.u, uv.v, face);
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            switch (face)
            {
                case CubeFace.Left:
                    return left.UVPatternAt(u, v);
                case CubeFace.Right:
                    return right.UVPatternAt(u, v);
                case CubeFace.Front:
                    return front.UVPatternAt(u, v);
                case CubeFace.Back:
                    return back.UVPatternAt(u, v);
                case CubeFace.Up:
                    return up.UVPatternAt(u, v);
                case CubeFace.Down:
                    return down.UVPatternAt(u, v);
                default:
                    return Color.Black;
            }
        }

        public static CubeFace FaceFromPoint(Point p)
        {
            var x = Math.Abs(p.x);
            var y = Math.Abs(p.y);
            var z = Math.Abs(p.z);
            var coord = Math.Max(x, Math.Max(y, z));

            if (coord.IsEqual(p.x)) return CubeFace.Right;
            if (coord.IsEqual(-p.x)) return CubeFace.Left;
            if (coord.IsEqual(p.y)) return CubeFace.Up;
            if (coord.IsEqual(-p.y)) return CubeFace.Down;
            if (coord.IsEqual(p.z)) return CubeFace.Front;

            return CubeFace.Back;
        }

        public static (double, double) CubeUVFront(Point p)
        {
            var u = ((p.x + 1) % 2.0) / 2.0;
            var v = ((p.y + 1) % 2.0) / 2.0;

            return (u, v);
        }

        public static (double, double) CubeUVBack(Point p)
        {
            var u = ((1 - p.x) % 2.0) / 2.0;
            var v = ((p.y + 1) % 2.0) / 2.0;

            return (u, v);
        }

        public static (double, double) CubeUVLeft(Point p)
        {
            var u = ((p.z + 1) % 2.0) / 2.0;
            var v = ((p.y + 1) % 2.0) / 2.0;

            return (u, v);
        }

        public static (double, double) CubeUVRight(Point p)
        {
            var u = ((1 - p.z) % 2.0) / 2.0;
            var v = ((p.y + 1) % 2.0) / 2.0;

            return (u, v);
        }

        public static (double, double) CubeUVUpper(Point p)
        {
            var u = ((p.x + 1) % 2.0) / 2.0;
            var v = ((1 - p.z) % 2.0) / 2.0;
             
            return (u, v);
        }

        public static (double, double) CubeUVLower(Point p)
        {
            var u = ((p.x + 1) % 2.0) / 2.0;
            var v = ((p.z + 1) % 2.0) / 2.0;

            return (u, v);
        }
    }
}
