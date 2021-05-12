package RTC.Math;

import java.util.Objects;

public class Tuple {
    public static int TupleId = 0;

    private int id;
    private double x, y, z, w;

    public Tuple(double X, double Y, double Z, double W) {
        id = TupleId++;

        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public double getZ() {
        return z;
    }

    public double getW() {
        return w;
    }
    
    public void setW(double W) {
        w = W;
    }
    
    public boolean isPoint() {
        return Helper.IsEqual(w, 1.0);
    }

    public boolean isVector() {
        return Helper.IsEqual(w, 0.0);
    }

    public Tuple add(Tuple t) {
        return new Tuple(x + t.getX(),
                         y + t.getY(),
                         z + t.getZ(),
                         w + t.getW());
    }

    public Tuple sub(Tuple t) {
        return new Tuple(x - t.getX(),
                         y - t.getY(),
                         z - t.getZ(),
                         w - t.getW());
    }

    public Tuple neg() {
        return new Tuple(-x, -y, -z, -w);
    }

    public Tuple mul(double a) {
        return new Tuple(x * a, y * a, z * a, w * a);
    }

    public Tuple div(double a) {
        return new Tuple(x / a, y / a, z / a, w / a);
    }

    public double mag() {
        return Math.sqrt(x * x + y * y + z * z + w * w);
    }

    public Tuple norm() {
        double mag = mag();
        return new Tuple(x / mag, y / mag, z / mag, w / mag);
    }

    public double dot(Tuple t) {
        return x * t.getX() +
               y * t.getY() +
               z * t.getZ() +
               w * t.getW();
    }

    public Tuple cross(Tuple t)
    {
        return new Tuple(y * t.getZ() - z * t.getY(),
                         z * t.getX() - x * t.getZ(),
                         x * t.getY() - y * t.getX(),
                         0);
    }

    public Tuple reflect(Tuple normal) {
        double d = 2 * dot(normal);
        Tuple n = normal.mul(d);
        return sub(n);
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Tuple)) {
            return false;
        }

        Tuple t = (Tuple) o;
        return Helper.IsEqual(x, t.getX())
        && Helper.IsEqual(y, t.getY())
        && Helper.IsEqual(z, t.getZ())
        && Helper.IsEqual(w, t.getW());
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, x, y, z, w);
    }

    @Override
    public String toString() {
        return String.format("(%f,%f,%f,%f) Id: %d", x, y, z, w, id);
    }
}
