using static System.Math;

namespace CSharp
{
    public class Vector : Tuple
    {
        public Vector(double x, double y, double z) : base(x, y, z, 0.0)
        {
            // Nothing else to do
        }

        public Vector(Tuple t) : base(t.x, t.y, t.z, 0.0)
        {
            // Nothing else to do
        }

        public static Vector operator *(Vector lhs, double rhs)
        {
            var ret = new Vector(lhs.x * rhs,
                             lhs.y * rhs,
                             lhs.z * rhs);
            ret.w = lhs.w * rhs;

            return ret;
        }

        public double Magnitude
        {
            get
            {
                return Sqrt(this.x * this.x +
                            this.y * this.y +
                            this.z * this.z +
                            this.w * this.w);
            }
        }

        public Vector Normalize
        {
            get
            {
                var mag = this.Magnitude;
                return new Vector(this.x / mag,
                                  this.y / mag,
                                  this.z / mag)
                {
                    w = w / mag
                };
            }
        }

        public double Dot(Vector rhs)
        {
            return this.x * rhs.x +
                   this.y * rhs.y +
                   this.z * rhs.z +
                   this.w * rhs.w;
        }

        public Vector Cross(Vector rhs)
        {
            return new Vector(
                              this.y * rhs.z - this.z * rhs.y,
                              this.z * rhs.x - this.x * rhs.z,
                              this.x * rhs.y - this.y * rhs.x
                             );
        }
    }
}
