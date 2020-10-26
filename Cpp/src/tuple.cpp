#include <math.h>
#include "tuple.h"
#include "rtmath.h"

Tuple::Tuple(double X, double Y, double Z, double W) : x(X), y(Y), z(Z), w(W)
{
    // Nothing to do here
}

bool Tuple::IsPoint()
{
    return cmpd(w, 1.0);
}

bool Tuple::IsVector()
{
    return cmpd(w, 0.0);
}

Tuple Tuple::operator+(const Tuple& rhs) const
{
    return Tuple(x + rhs.x,
                 y + rhs.y,
                 z + rhs.z,
                 w + rhs.w);
}

Tuple Tuple::operator-(const Tuple& rhs) const
{
    return Tuple(x - rhs.x,
                 y - rhs.y,
                 z - rhs.z,
                 w - rhs.w);
}

Tuple Tuple::operator-() const
{
    return Tuple(-x, -y, -z, -w);
}

Tuple Tuple::operator*(const double& rhs) const
{
    return Tuple(x * rhs,
                 y * rhs,
                 z * rhs,
                 w * rhs);
}

Tuple Tuple::operator/(const double& rhs) const
{
    return Tuple(x / rhs,
                 y / rhs,
                 z / rhs,
                 w / rhs);
}

bool Tuple::operator==(const Tuple& rhs) const
{
    return (this == &rhs) ||
            (cmpd(x, rhs.x) &&
            cmpd(y, rhs.y) &&
            cmpd(z, rhs.z) &&
            cmpd(w, rhs.w));
}

/*
bool operator==(const Tuple& lhs, const Tuple& rhs)
{
    return (&lhs == &rhs) ||
            (cmpd(lhs.x, rhs.x) &&
            cmpd(lhs.y, rhs.y) &&
            cmpd(lhs.z, rhs.z) &&
            cmpd(lhs.w, rhs.w));
}
*/

std::ostream& operator<<(std::ostream& os, const Tuple& p)
{
    return os << "(" << p.x << "," << p.y << "," << p.z << "," << p.w << ")"; 
}


double Vector::Magnitude()
{
    return sqrt(x * x +
                y * y +
                z * z +
                w * w);
}

Vector Vector::Normalize()
{
    double mag = Magnitude();
    Vector v = Vector(x / mag,
                      y / mag,
                      z / mag);
    v.w = w / mag;

    return v;
}

double Vector::Dot(Vector v)
{
    return x * v.x +
           y * v.y +
           z * v.z +
           w * v.w;
}

Vector Vector::Cross(Vector v)
{
    return Vector(
                  y * v.z - z * v.y,
                  z * v.x - x * v.z,
                  x * v.y - y * v.x
                 );
}