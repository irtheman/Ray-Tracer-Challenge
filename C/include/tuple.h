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
bool is_equal(struct Tuple a, struct Tuple b);
char* tuple_string(struct Tuple t);

#endif
