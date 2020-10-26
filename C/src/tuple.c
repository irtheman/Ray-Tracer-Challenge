#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include "tuple.h"
#include "rtmath.h"

// Create tuples, points and vectors

Tuple create_tuple(double X, double Y, double Z, double W) {
    Tuple t = {X, Y, Z, W};
    return t;
}

Tuple create_point(double X, double Y, double Z) {
    return create_tuple(X, Y, Z, 1.0);
}

Tuple create_vector(double X, double Y, double Z) {
    return create_tuple(X, Y, Z, 0.0);
}

bool is_point(Tuple t) {
    return CMPD(t.w, 1.0);
}

bool is_vector(Tuple t) {
    return CMPD(t.w, 0.0);
}

// Some basic tuple math
Tuple addt(Tuple a, Tuple b)
{
    Tuple ret = { a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w };
    return ret;
}

Tuple subt(Tuple a, Tuple b)
{
    Tuple ret = { a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w };
    return ret;
}

Tuple mult(Tuple a, double b)
{
    Tuple ret = { a.x * b, a.y * b, a.z * b, a.w * b };
    return ret;
}

Tuple divt(Tuple a, double b)
{
    Tuple ret = { a.x / b, a.y / b, a.z / b, a.w / b };
    return ret;
}

Tuple tneg(Tuple t)
{
    Tuple ret = { -t.x, -t.y, -t.z, -t.w };
    return ret;
}

// Vector basic math
double magv(Tuple t)
{
    return sqrt(t.x * t.x +
                t.y * t.y +
                t.z * t.z +
                t.w + t.w);
}

Tuple normv(Tuple t)
{
    double mag = magv(t);
    Tuple ret = {
        t.x / mag,
        t.y / mag,
        t.z / mag,
        t.w / mag
    };

    return ret;
}

double dotv(Tuple a, Tuple b)
{
    return a.x * b.x +
           a.y * b.y +
           a.z * b.z +
           a.w * b.w;
}

Tuple crossv(Tuple a, Tuple b)
{
    Tuple ret = {
        a.y * b.z - a.z * b.y,
        a.z * b.x - a.x * b.z,
        a.x * b.y - a.y * b.x
    };

    return ret;
}

// Tuple equality test
bool is_equal(Tuple a, Tuple b) {
    return CMPD(a.x, b.x) &&
           CMPD(a.y, b.y) &&
           CMPD(a.z, b.z) &&
           CMPD(a.w, b.w);
}

// Convert a tuple to a string
char* tuple_string(Tuple t) {
    char *buffer = (char *) malloc(50);
    sprintf(buffer, "(%f, %f, %f, %f)", t.x, t.y, t.z, t.w);
    return buffer;
}
