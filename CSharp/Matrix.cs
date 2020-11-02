using System;
using System.Collections.Generic;
using System.Data;
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
            LoadMatrix(rows, columns, data);
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

        public static Matrix operator*(Matrix lhs, double rhs)
        {
            Matrix ret = new Matrix(lhs);

            for (int i = 0; i < lhs.Rows; i++)
                for (int j = 0; j < lhs.Columns; j++)
                    ret[i, j] *= rhs;

            return ret;
        }

        public static Matrix operator*(Matrix lhs, Matrix rhs)
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

        public static Matrix operator *(Matrix lhs, Tuple rhs)
        {
            if ((lhs.Rows != 4) || (lhs.Columns != 4))
                throw new Exception("Matrix must be Nx4 or 4xM.");

            return lhs * new Matrix(rhs, lhs.Rows == 4);
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
                    if (m[i, j] != other[i, j])
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
