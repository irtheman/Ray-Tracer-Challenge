#ifndef COLOR_H
#define COLOR_H

#include <stdbool.h>

typedef struct {
    double red, green, blue;
} Color;

// Create color
Color create_color(double Red, double Green, double Blue);

// Color basic math
Color addc(Color a, Color b);
Color subc(Color a, Color b);
Color mulcs(Color a, double b);
Color mulc(Color a, Color b);

// Tuple equality test
bool is_equal_color(Color a, Color b);

// Tuple string conversion
char* color_string(Color t);

#endif
