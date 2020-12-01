﻿using static System.Math;

namespace CSharp
{
    public class Vector : Tuple
    {
        public static readonly Vector Zero = new Vector(0, 0, 0);
        public static readonly Vector Ones = new Vector(1, 1, 1);
        public static readonly Vector VectorX = new Vector(1, 0, 0);
        public static readonly Vector VectorY = new Vector(0, 1, 0);
        public static readonly Vector VectorZ = new Vector(0, 0, 1);

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

            return ret;
        }
        public static Vector operator -(Vector rhs)
        {
            return new Vector(-rhs.x,
                              -rhs.y,
                              -rhs.z);
        }

        public double Magnitude
        {
            get
            {
                return Sqrt(this.x * this.x +
                            this.y * this.y +
                            this.z * this.z);
            }
        }

        public Vector Normalize
        {
            get
            {
                var mag = this.Magnitude;
                return new Vector(this.x / mag,
                                  this.y / mag,
                                  this.z / mag);
            }
        }

        public double Dot(Vector rhs)
        {
            return this.x * rhs.x +
                   this.y * rhs.y +
                   this.z * rhs.z;
        }

        public Vector Cross(Vector rhs)
        {
            return new Vector(
                              this.y * rhs.z - this.z * rhs.y,
                              this.z * rhs.x - this.x * rhs.z,
                              this.x * rhs.y - this.y * rhs.x
                             );
        }

        public Vector Reflect(Vector normal)
        {
            return new Vector(this - normal * 2 * Dot(normal));
        }
    }
}