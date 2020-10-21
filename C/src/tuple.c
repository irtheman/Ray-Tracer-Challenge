#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
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
