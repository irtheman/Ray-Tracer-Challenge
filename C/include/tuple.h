#ifndef TUPLE_H
#define TUPLE_H

#include <stdbool.h>

typedef struct {
    double x, y, z, w;
} Tuple;

// Create tuples, points and vectors
Tuple create_tuple(double X, double Y, double Z, double W);
Tuple create_point(double X, double Y, double Z);
Tuple create_vector(double X, double Y, double Z);
bool is_point(Tuple t);
bool is_vector(Tuple t);

// Tuple basic math
Tuple addt(Tuple a, Tuple b);
Tuple subt(Tuple a, Tuple b);
Tuple mult(Tuple a, double b);
Tuple divt(Tuple a, double b);
Tuple tneg(Tuple t);

// Vector basic math
double magv(Tuple t);
Tuple normv(Tuple t);
double dotv(Tuple a, Tuple b);
Tuple crossv(Tuple a, Tuple b);

// Tuple equality test
bool is_equal(Tuple a, Tuple b);

// Tuple string conversion
char* tuple_string(Tuple t);

#endif
