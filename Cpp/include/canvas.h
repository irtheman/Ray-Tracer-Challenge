#ifndef CANVAS_H
#define CANVAS_H

#include <string>
#include "color.h"

class Canvas
{
public:
    Canvas(int columns, int rows);
    int Width();
    int Height();
    Color& operator() (int column, int row);
    std::string GetPPM();

private:
    Color** canvas;
    int width, height;
};

#endif