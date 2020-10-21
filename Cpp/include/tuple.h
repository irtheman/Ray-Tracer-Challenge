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

    bool operator==(const Tuple& rhs) const;
    friend bool operator==(const Tuple& lhs, const Tuple& rhs);
    friend std::ostream& operator<<(std::ostream& os, const Tuple& p);
};

// Point Class
class Point : public Tuple
{
public:
    Point(double x, double y, double z) : Tuple(x, y, z, 1.0) { }
};

// Vector Class
class Vector : public Tuple
{
public:
    Vector(double x, double y, double z) : Tuple(x, y, z, 0.0) { }
};

#endif
