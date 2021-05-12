package RTC.Math;

import java.util.Objects;

public class Matrix {
    public static int MatrixId = 0;

    private int id;
    private double[][] m;
    private int width;
    private int height;

    public Matrix(int width, int height) {
        this(width, height, new double[] { });
    }

    public Matrix(int width, int height, double[] array) {
        id = MatrixId++;
        this.width = width;
        this.height = height;

        m = new double[width][height];

        int index = 0;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                m[x][y] = array.length > 0 ? array[index++] : 0;
            }
        }
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public void set(int x, int y, double value) {
        m[x][y] = value;
    }
    public double valueAt(int x, int y) {
        return m[x][y];
    }

    public Matrix mul(Matrix b) {
        Matrix M = new Matrix(width, height);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                M.set(x, y, m[x][0] * b.valueAt(0, y) +
                            m[x][1] * b.valueAt(1, y) +
                            m[x][2] * b.valueAt(2, y) +
                            m[x][3] * b.valueAt(3, y));
            }
        }

        return M;
    }

    public Tuple mul(Tuple b) {
        double x = b.getX();
        double y = b.getY();
        double z = b.getZ();
        double w = b.getW();

        double X = m[0][0] * x +
                   m[0][1] * y +
                   m[0][2] * z +
                   m[0][3] * w;
        double Y = m[1][0] * x +
                   m[1][1] * y +
                   m[1][2] * z +
                   m[1][3] * w;
        double Z = m[2][0] * x +
                   m[2][1] * y +
                   m[2][2] * z +
                   m[2][3] * w;
        double W = m[3][0] * x +
                   m[3][1] * y +
                   m[3][2] * z +
                   m[3][3] * w;

        return new Tuple(X, Y, Z, W);
    }

    public Matrix transpose() {
        Matrix M = new Matrix(height, width);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                M.set(y, x, m[x][y]);
            }
        }

        return M;
    }

    public double determinate() {
        double det = 0.0;

        if ((width == 2) && (height == 2)) {
            det = m[0][0] * m[1][1] - m[0][1] * m[1][0];
        } else {
            for (int c = 0; c < height; c++) {
                det = det + m[0][c] * cofactor(0, c);
            }
        }

        return det;
    }

    public Matrix submatrix(int row, int column) {
        Matrix M = new Matrix(width - 1, height - 1);

        int i = -1, j;
        for (int x = 0; x < width; x++) {
            if (x == row) continue;
            i++;

            j = -1;
            for (int y = 0; y < height; y++) {
                if (y == column) continue;
                j++;

                M.set(i, j, m[x][y]);
            }
        }

        return M;
    }

    public double minor(int row, int column) {
        Matrix M = submatrix(row, column);
        return M.determinate();
    }

    public double cofactor(int row, int column) {
        double m = minor(row, column);
        return (row + column) % 2 == 0 ? m : -m;
    }

    public boolean isInvertible() {
        return !Helper.IsEqual(determinate(), 0.0);
    }

    public Matrix inverse() {
        Matrix M = new Matrix(width, height);

        double c = 0.0;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                c = cofactor(x, y);
                M.set(y, x, c / determinate());
            }
        }

        return M;
    }

    public static Matrix Identity() {
        return new Matrix(4, 4, new double[] { 1, 0, 0, 0,
                                               0, 1, 0, 0,
                                               0, 0, 1, 0,
                                               0, 0, 0, 1});
    }

    public static Matrix Translation(double x, double y, double z) {
        Matrix M = Identity();
        M.set(0, 3, x);
        M.set(1, 3, y);
        M.set(2, 3, z);

        return M;
    }

    public static Matrix Scaling(double x, double y, double z) {
        Matrix M = Identity();
        M.set(0, 0, x);
        M.set(1, 1, y);
        M.set(2, 2, z);

        return M;
    }

    public static Matrix RotationX(double r) {
        Matrix M = Identity();
        double cos = Math.cos(r);
        double sin = Math.sin(r);

        M.set(1, 1, cos);
        M.set(1, 2, -sin);
        M.set(2, 1, sin);
        M.set(2, 2, cos); 

        return M;
    }

    public static Matrix RotationY(double r) {
        Matrix M = Identity();
        double cos = Math.cos(r);
        double sin = Math.sin(r);

        M.set(0, 0, cos);
        M.set(0, 2, sin);
        M.set(2, 0, -sin);
        M.set(2, 2, cos); 

        return M;
    }

    public static Matrix RotationZ(double r) {
        Matrix M = Identity();
        double cos = Math.cos(r);
        double sin = Math.sin(r);

        M.set(0, 0, cos);
        M.set(0, 1, -sin);
        M.set(1, 0, sin);
        M.set(1, 1, cos); 

        return M;
    }

    public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy) {
        Matrix M = Identity();

        M.set(0, 1, xy);
        M.set(0, 2, xz);
        M.set(1, 0, yx);
        M.set(1, 2, yz);
        M.set(2, 0, zx);
        M.set(2, 1, zy);

        return M;
    }

    public static Matrix ViewTransform(Point from, Point to, Vector up)
    {
        Tuple forward = to.sub(from).norm();
        Tuple upn = up.norm();
        Tuple left = forward.cross(upn);
        Tuple trueUp = left.cross(forward);
        Matrix orientation = new Matrix(4, 4,
                                        new double[]
                                        {
                                            left.getX(),     left.getY(),     left.getZ(),     0,
                                            trueUp.getX(),   trueUp.getY(),   trueUp.getZ(),   0,
                                            -forward.getX(), -forward.getY(), -forward.getZ(), 0,
                                            0,               0,               0,               1
                                        });

        return orientation.mul(Matrix.Translation(-from.getX(), -from.getY(), -from.getZ()));
    }    

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Matrix)) {
            return false;
        }

        Matrix t = (Matrix) o;
        if ((width != t.getWidth()) || (height != t.getHeight())) {
            return false;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!Helper.IsEqual(m[x][y], t.valueAt(x, y))) {
                    return false;
                }
            }
        }

        return true;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, m.hashCode());
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append(id).append(" [");

        for (int x = 0; x < width; x++)
        {
            sb.append("[ ");
            for (int y = 0; y < height; y++)
            {
                sb.append(m[x][y]).append(" ");
            }
            sb.append("]");
        }

        sb.append("]");
        return sb.toString();
    }
}
