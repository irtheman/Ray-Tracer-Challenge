#ifndef TUPLE_H
#define TUPLE_H

#include <stdbool.h>

struct Tuple {
    double x, y, z, w;
};

struct Tuple create_tuple(double X, double Y, double Z, double W);
struct Tuple create_point(double X, double Y, double Z);
struct Tuple create_vector(double X, double Y, double Z);
bool is_point(struct Tuple t);
bool is_vector(struct Tuple t);
struct Tuple tadd(struct Tuple a, struct Tuple b);
struct Tuple tsub(struct Tuple a, struct Tuple b);
struct Tuple tmul(struct Tuple a, double b);
struct Tuple tdiv(struct Tuple a, double b);
struct Tuple tneg(struct Tuple t);
double tmag(struct Tuple t);
struct Tuple tnorm(struct Tuple t);
double vdot(struct Tuple a, struct Tuple b);
struct Tuple vcross(struct Tuple a, struct Tuple b);
bool is_equal(struct Tuple a, struct Tuple b);
char* tuple_string(struct Tuple t);

#endif
