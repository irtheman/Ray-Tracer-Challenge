#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include "color.h"
#include "rtmath.h"

// Create color

Color create_color(double Red, double Green, double Blue) {
    Color c = {Red, Green, Blue};
    return c;
}

// Some basic color math
Color addc(Color a, Color b)
{
    Color ret = { a.red + b.red, a.green + b.green, a.blue + b.blue };
    return ret;
}

Color subc(Color a, Color b)
{
    Color ret = { a.red - b.red, a.green - b.green, a.blue - b.blue };
    return ret;
}

Color mulcs(Color a, double b)
{
    Color ret = { a.red * b, a.green * b, a.blue * b };
    return ret;
}

Color mulc(Color a, Color b)
{
    Color ret = { a.red * b.red, a.green * b.green, a.blue * b.blue };
    return ret;
}

// Color equality test
bool is_equal_color(Color a, Color b) {
    return CMPD(a.red, b.red) &&
           CMPD(a.green, b.green) &&
           CMPD(a.blue, b.blue);
}

// Convert a color to a string
char* color_string(Color c) {
    char *buffer = (char *) malloc(50);
    sprintf(buffer, "(%f, %f, %f)", c.red, c.green, c.blue);
    return buffer;
}
