#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include "tuple.h"
#include "rtmath.h"

struct Tuple create_tuple(double X, double Y, double Z, double W) {
    struct Tuple t = {X, Y, Z, W};
    return t;
}

struct Tuple create_point(double X, double Y, double Z) {
    return create_tuple(X, Y, Z, 1.0);
}

struct Tuple create_vector(double X, double Y, double Z) {
    return create_tuple(X, Y, Z, 0.0);
}

bool is_point(struct Tuple t) {
    return CMPD(t.w, 1.0);
}

bool is_vector(struct Tuple t) {
    return CMPD(t.w, 0.0);
}

struct Tuple tadd(struct Tuple a, struct Tuple b)
{
    struct Tuple ret = { a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w };
    return ret;
}

struct Tuple tsub(struct Tuple a, struct Tuple b)
{
    struct Tuple ret = { a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w };
    return ret;
}

struct Tuple tmul(struct Tuple a, double b)
{
    struct Tuple ret = { a.x * b, a.y * b, a.z * b, a.w * b };
    return ret;
}

struct Tuple tdiv(struct Tuple a, double b)
{
    struct Tuple ret = { a.x / b, a.y / b, a.z / b, a.w / b };
    return ret;
}

struct Tuple tneg(struct Tuple t)
{
    struct Tuple ret = { -t.x, -t.y, -t.z, -t.w };
    return ret;
}

double tmag(struct Tuple t)
{
    return sqrt(t.x * t.x +
                t.y * t.y +
                t.z * t.z +
                t.w + t.w);
}

struct Tuple tnorm(struct Tuple t)
{
    double mag = tmag(t);
    struct Tuple ret = {
        t.x / mag,
        t.y / mag,
        t.z / mag,
        t.w / mag
    };

    return ret;
}

double vdot(struct Tuple a, struct Tuple b)
{
    return a.x * b.x +
           a.y * b.y +
           a.z * b.z +
           a.w * b.w;
}

struct Tuple vcross(struct Tuple a, struct Tuple b)
{
    struct Tuple ret = {
        a.y * b.z - a.z * b.y,
        a.z * b.x - a.x * b.z,
        a.x * b.y - a.y * b.x
    };

    return ret;
}

bool is_equal(struct Tuple a, struct Tuple b) {
    return CMPD(a.x, b.x) &&
           CMPD(a.y, b.y) &&
           CMPD(a.z, b.z) &&
           CMPD(a.w, b.w);
}

char* tuple_string(struct Tuple t) {
    char *buffer = (char *) malloc(50);
    sprintf(buffer, "(%f, %f, %f, %f)", t.x, t.y, t.z, t.w);
    return buffer;
}
