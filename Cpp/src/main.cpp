#include <iostream>
#include "tuple.h"

int main (int argc, char *argv[]) {

    std::cout << "Hello World!" << std::endl;

    Tuple t = Tuple(1,2,3,4);
    std::cout << "Tuple: " << t << std::endl;

    return 0;
}
