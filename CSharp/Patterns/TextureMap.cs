using System;

namespace CSharp
{
    public enum Mapping
    {
        Spherical,
        Planar,
        Cylindrical,
        Cubical
    }

    public class TextureMap : RTPattern
    {
        private RTPattern _pattern;
        private Mapping _mapping;

        public TextureMap(RTPattern pattern, Mapping mapping) : base(Color.Black, Color.Black)
        {
            _pattern = pattern;
            _mapping = mapping;
        }

        public override Color PatternAt(Point p)
        {
            var uv = UVMap(p);
            return _pattern.UVPatternAt(uv.u, uv.v, _mapping == Mapping.Cubical ? CubeMapPattern.FaceFromPoint(p) : CubeFace.None);
        }

        public override Color UVPatternAt(double u, double v, CubeFace face = CubeFace.None)
        {
            return Color.Black;
        }

        private (double u, double v) UVMap(Point p)
        {
            (double u, double v) ret = (0, 0);

            switch (_mapping)
            {
                case Mapping.Spherical:
                    ret = SphericalMap(p);
                    break;

                case Mapping.Planar:
                    ret = PlanarMap(p);
                    break;

                case Mapping.Cylindrical:
                    ret = CylindricalMap(p);
                    break;

                case Mapping.Cubical:
                    ret = CubicalMap(p);
                    break;
            }

            return ret;
        }

        public static (double u, double v) SphericalMap(Point p)
        {
            var theta = Math.Atan2(p.x, p.z);
            var vec = new Vector(p.x, p.y, p.z);
            var radius = vec.Magnitude;
            var phi = Math.Acos(p.y / radius);
            var raw_u = theta / (2 * Math.PI);
            var u = 1 - (raw_u + 0.5);
            var v = 1 - phi / Math.PI;

            return (u, v);
        }

        public static (double u, double v) PlanarMap(Point p)
        {
            var u = p.x % 1.0;
            if (u < 0.0)
            {
                u = 1.0 + u;
            }

            var v = p.z % 1.0;
            if (v < 0.0)
            {
                v = 1.0 + v;
            }

            return (u, v);
        }

        public static (double u, double v) CylindricalMap(Point p)
        {
            var theta = Math.Atan2(p.x, p.z);
            var raw_u = theta / (2.0 * Math.PI);
            var u = 1 - (raw_u + 0.5);
            var v = p.y % 1.0;
            if (v < 0.0)
            {
                v = 1.0 + v;
            }

            return (u, v);
        }

        public static (double u, double v) CubicalMap(Point p)
        {
            var face = CubeMapPattern.FaceFromPoint(p);
            (double u, double v) uv;

            switch (face)
            {
                case CubeFace.Left:
                    uv = CubeMapPattern.CubeUVLeft(p);
                    break;
                case CubeFace.Right:
                    uv = CubeMapPattern.CubeUVRight(p);
                    break;
                case CubeFace.Front:
                    uv = CubeMapPattern.CubeUVFront(p);
                    break;
                case CubeFace.Back:
                    uv = CubeMapPattern.CubeUVBack(p);
                    break;
                case CubeFace.Up:
                    uv = CubeMapPattern.CubeUVUpper(p);
                    break;
                case CubeFace.Down:
                    uv = CubeMapPattern.CubeUVLower(p);
                    break;
                default:
                    uv = (0, 0);
                    break;
            }

            return uv;
        }
    }
}
