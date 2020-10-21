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

bool Tuple::operator==(const Tuple& rhs) const
{
    return (this == &rhs) ||
            (cmpd(x, rhs.x) &&
            cmpd(y, rhs.y) &&
            cmpd(z, rhs.z) &&
            cmpd(w, rhs.w));
}

bool operator==(const Tuple& lhs, const Tuple& rhs)
{
    return (&lhs == &rhs) ||
            (cmpd(lhs.x, rhs.x) &&
            cmpd(lhs.y, rhs.y) &&
            cmpd(lhs.z, rhs.z) &&
            cmpd(lhs.w, rhs.w));
}

std::ostream& operator<<(std::ostream& os, const Tuple& p)
{
    return os << "(" << p.x << "," << p.y << "," << p.z << "," << p.w << ")"; 
}
