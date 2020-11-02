#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include "canvas.h"
#include "color.h"

Canvas create_canvas(int columns, int rows)
{
    Canvas c = {
        columns,
        rows, 
        (Color *) calloc(columns * rows, sizeof(Color))
    };

    return c;
}

int calculate_index(Canvas c, int column, int row) 
{
    return (column * c.height) + row;
}

Color getPixel(Canvas c, int column, int row)
{
    int index = calculate_index(c, column, row);

    if ((row < 0) || (row >= c.height)) return c.data[0];
    if ((column < 0) || (column >= c.width)) return c.data[0];

    return c.data[index];
}

void setPixel(Canvas c, int column, int row, Color color)
{
    int index = calculate_index(c, column, row);

    if ((row < 0) || (row >= c.height)) return;
    if ((column < 0) || (column >= c.width)) return;

    c.data[index] = color;
}

char* getPPM(Canvas c)
{
    char* ppm;
    char* p;

    // Header is 50
    // Color is 3 + 1 + 3 + 1 + 3 + 1 = 12
    ppm = (char *)calloc(50 + (c.width * c.height * 12), sizeof(char));
    p = ppm;

    // PPM Header
    sprintf(p, "P3\n%d %d\n255\n", c.width, c.height);
    p = ppm + strlen(ppm);

    // Build the image
    Color color;
    int* rgb = (int*)malloc(3 * sizeof(int));
    char num[50];
    int numlen = 0;
    int length = 0;
    for (int y = 0; y < c.height; y++)
    {
        // Build a line
        for (int x = 0; x < c.width; x++)
        {
            color = getPixel(c, x, y);
            rgb[0] = round(color.red * 255);
            rgb[1] = round(color.green * 255);
            rgb[2] = round(color.blue * 255);

            for (int i = 0; i < 3; i++)
            {
                if (rgb[i] < 0) rgb[i] = 0;
                if (rgb[i] > 255) rgb[i] = 255;

                sprintf(num, "%d\0", rgb[i]);
                numlen = strlen(num);

                if (length + numlen + 1 >= 70)
                {
                    sprintf(p, "\n");
                    p += 1;
                    length = 0;
                }
                else if (length > 0)
                {
                    sprintf(p, " ");
                    p += 1;
                    length += 1;
                }

                sprintf(p, "%s", num);
                p += numlen;

                length += numlen;
            }
        }

        sprintf(p, "\n");
        p += 1;
        length = 0;
    }

    return ppm;
}
