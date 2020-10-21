#include <stdio.h>
#include <stdlib.h>
#include "tuple.h"

int main (int argc, char *argv[]) {

    printf("Hello World!\r\n");

    struct Tuple t = {1, 2, 3, 4};
    char* tuple = tuple_string(t);
    printf("Tuple: %s\r\n", tuple);
    free(tuple);

    return 0;
}
