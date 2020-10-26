#ifndef TUPLE_H
#define TUPLE_H

#include <iostream>

// Tuple Class
class Tuple
{
public:

    Tuple(double X, double Y, double Z, double W);
    double x, y, z, w;
    
    bool IsPoint();
    bool IsVector();

    Tuple operator+(const Tuple& rhs) const;
    Tuple operator-(const Tuple& rhs) const;
    Tuple operator-() const;
    Tuple operator*(const double& rhs) const;
    Tuple operator/(const double& rhs) const;
    bool operator==(const Tuple& rhs) const;
    //friend bool operator==(const Tuple& lhs, const Tuple& rhs);
    friend std::ostream& operator<<(std::ostream& os, const Tuple& p);
};

// Point Class
class Point : public Tuple
{
public:
    Point(double x, double y, double z) : Tuple(x, y, z, 1.0) { }
    Point(Tuple t) : Tuple(t) {}
};

// Vector Class
class Vector : public Tuple
{
public:
    Vector(double x, double y, double z) : Tuple(x, y, z, 0.0) { }
    Vector(Tuple t) : Tuple(t) { }
    double Magnitude();
    Vector Normalize();
    double Dot(Vector v);
    Vector Cross(Vector v);
};

#endif
