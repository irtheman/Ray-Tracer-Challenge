using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CSharp
{
    public class Matrix
    {
        private readonly double[,] m;

        public Matrix() : this(4, 4)
        {
            // Nothing here
        }

        public Matrix(int rows, int columns) : this(rows, columns, null)
        {
            // Nothing here
        }

        public Matrix(int rows, int columns, double[] data)
        {
            m = new double[rows, columns];
            if (data != null)
            {
                LoadMatrix(rows, columns, data);
            }
        }

        public Matrix(Matrix matrix)
        {
            m = new double[matrix.Rows, matrix.Columns];

            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                    m[i, j] = matrix[i, j];
        }

        public Matrix(Tuple t, bool isColumn = true)
        {
            int rows = 1;
            int columns = 1;

            if (isColumn)
                rows = 4;
            else
                columns = 4;

            double[] data = { t.x, t.y, t.z, t.w };

            m = new double[rows, columns];
            LoadMatrix(rows, columns, data);
        }

        public int Rows => m.GetUpperBound(0) + 1;
        public int Columns => m.GetUpperBound(1) + 1;

        public double this[int row, int column]
        {
            get
            {
                return m[row, column];
            }

            set
            {
                m[row, column] = value;
            }
        }

        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            if (lhs.Columns != rhs.Rows)
                throw new Exception("Matrix Cannot Be Multiplied.");

            Matrix ret = new Matrix(lhs.Rows, rhs.Columns);
            double temp = 0.0;

            for (int i = 0; i < lhs.Rows; i++)
            {
                for (int j = 0; j < rhs.Columns; j++)
                {
                    temp = 0;
                    for (int k = 0; k < lhs.Columns; k++)
                        temp += lhs[i, k] * rhs[k, j];
                    ret[i, j] = temp;
                }
            }

            return ret;
        }

        public static Tuple operator *(Matrix lhs, Tuple rhs)
        {
            if ((lhs.Rows != 4) || (lhs.Columns != 4))
                throw new Exception("Matrix must be Nx4 or 4xM.");

            Matrix value = lhs * new Matrix(rhs, lhs.Rows == 4);

            double x, y, z, w;
            if (value.Rows == 4)
            {
                x = value[0, 0];
                y = value[1, 0];
                z = value[2, 0];
                w = value[3, 0];
            }
            else
            {
                x = value[0, 0];
                y = value[0, 1];
                z = value[0, 2];
                w = value[0, 3];
            }

            return new Tuple(x, y, z, w);
        }

        public static Point operator *(Matrix lhs, Point rhs)
        {
            Tuple other = (Tuple)rhs;
            Tuple t = lhs * other;

            return new Point(t.x, t.y, t.z);
        }

        public static Vector operator *(Matrix lhs, Vector rhs)
        {
            Tuple other = (Tuple)rhs;
            Tuple t = lhs * other;

            return new Vector(t.x, t.y, t.z);
        }

        public static Matrix Identity
        {
            get
            {
                var m = new Matrix(4, 4);
                m[0, 0] = 1;
                m[1, 1] = 1;
                m[2, 2] = 1;
                m[3, 3] = 1;

                return m;
            }
        }

        public Matrix Transpose
        {
            get
            {
                Matrix m = new Matrix(Columns, Rows);

                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Columns; j++)
                        m[j, i] = this[i, j];

                return m;
            }
        }

        public double Determinant
        {
            get
            {
                double det = 0;
                int size = Math.Max(Rows, Columns);
                if (size == 2)
                {
                    det = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                }
                else
                {
                    for (var c = 0; c < size; c++)
                    {
                        det += this[0, c] * Cofactor(0, c);
                    }
                }

                return det;
            }
        }

        public Matrix Submatrix(int deleteRow, int deleteColumn)
        {
            Matrix m = new Matrix(Rows - 1, Columns - 1);
            int r = 0;
            int c = 0;

            for (int i = 0; i < Rows; i++)
            {
                if (i == deleteRow) continue;

                c = 0;
                for (int j = 0; j < Columns; j++)
                {
                    if (j == deleteColumn) continue;

                    m[r, c] = this[i, j];
                    c += 1;
                }

                r += 1;
            }

            return m;
        }

        public double Minor(int deleteRow, int deleteColumn)
        {
            Matrix m = Submatrix(deleteRow, deleteColumn);
            return m.Determinant;
        }

        public double Cofactor(int deleteRow, int deleteColumn)
        {
            Matrix m = Submatrix(deleteRow, deleteColumn);
            var det = m.Determinant;
            return (deleteRow + deleteColumn) % 2 == 0 ? det : -det;
        }

        public bool IsInvertible => !Determinant.IsEqual(0);

        public Matrix Inverse
        {
            get
            {
                if (!IsInvertible) throw new Exception("Matrix cannot be inverted.");

                Matrix m = new Matrix(Rows, Columns);
                var size = Math.Max(Rows, Columns);
                var det = Determinant;

                for (var row = 0; row < size; row++)
                {
                    for (var col = 0; col < size; col++)
                    {
                        var c = Cofactor(row, col);
                        m[col, row] = c / det;
                    }
                }

                return m;
            }
        }

        public static Matrix Translation(double x, double y, double z)
        {
            var id = Identity;
            id[0, 3] = x;
            id[1, 3] = y;
            id[2, 3] = z;

            return id;
        }

        public static Matrix Scaling(double x, double y, double z)
        {
            var id = Identity;
            id[0, 0] = x;
            id[1, 1] = y;
            id[2, 2] = z;

            return id;
        }

        public static Matrix RotationX(double angle)
        {
            var id = Identity;
            var sine = Math.Sin(angle);
            var cose = Math.Cos(angle);

            id[1, 1] = cose;
            id[1, 2] = -sine;
            id[2, 1] = sine;
            id[2, 2] = cose;

            return id;
        }

        public static Matrix RotationY(double angle)
        {
            var id = Identity;
            var sine = Math.Sin(angle);
            var cose = Math.Cos(angle);

            id[0, 0] = cose;
            id[0, 2] = sine;
            id[2, 0] = -sine;
            id[2, 2] = cose;

            return id;
        }

        public static Matrix RotationZ(double angle)
        {
            var id = Identity;
            var sine = Math.Sin(angle);
            var cose = Math.Cos(angle);

            id[0, 0] = cose;
            id[0, 1] = -sine;
            id[1, 0] = sine;
            id[1, 1] = cose;

            return id;
        }

        public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            var id = Identity;

            id[0, 1] = xy;
            id[0, 2] = xz;
            id[1, 0] = yx;
            id[1, 2] = yz;
            id[2, 0] = zx;
            id[2, 1] = zy;

            return id;
        }

        public static Matrix ViewTransform(Point from, Point to, Vector up)
        {
            var forward = (new Vector(to - from)).Normalize;
            var upn = up.Normalize;
            var left = forward.Cross(upn);
            var trueUp = left.Cross(forward);
            var orientation = new Matrix(4, 4,
                                         new double[]
                                         {
                                             left.x,      left.y,     left.z,    0,
                                             trueUp.x,    trueUp.y,   trueUp.z,  0,
                                            -forward.x,  -forward.y, -forward.z, 0,
                                             0,           0,          0,         1
                                         });

            return orientation * Matrix.Translation(-from.x, -from.y, -from.z);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Matrix;
            if ((other == null) ||
                (Rows != other.Rows) ||
                (Columns != other.Columns))
                return false;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (!m[i, j].IsEqual(other[i, j]))
                        return false;

            return true;
        }

        public override int GetHashCode()
        {
            return m.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Rows}x{Columns} [");

            for (int i = 0; i < Rows; i++)
            {
                sb.Append("[");

                for (int j = 0; j < Columns; j++)
                {
                    sb.Append($"{m[i, j]} ");
                }

                sb.Append("] ");
            }
            sb.Append("]");

            return sb.ToString();
        }

        private void LoadMatrix(int rows, int columns, double[] data)
        {
            int index = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    m[i, j] = (data != null) && (index < data.Length) ? data[index++] : 0;
        }
    }
}
