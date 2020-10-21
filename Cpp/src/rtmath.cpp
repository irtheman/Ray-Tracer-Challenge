#include <stdlib.h>
#include "rtmath.h"

bool cmpd(double a, double b, double epsilon) {
    return (abs(a - b) < epsilon);
}
