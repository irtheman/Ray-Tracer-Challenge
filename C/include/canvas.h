#ifndef CANVAS_H
#define CANVAS_H

#include "color.h"

typedef struct {
    int width, height;
    Color * data;
} Canvas;

Canvas create_canvas(int columns, int rows);
Color getPixel(Canvas c, int column, int row);
void setPixel(Canvas c, int column, int row, Color color);
char* getPPM(Canvas c);

#endif