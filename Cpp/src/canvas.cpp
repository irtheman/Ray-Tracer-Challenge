#include <string>
#include <cmath>
#include "canvas.h"
#include "color.h"

Canvas::Canvas(int columns, int rows) : width(columns), height(rows)
{
    canvas = new Color* [columns];

    for (int x = 0; x < columns; x++)
    {
        canvas[x] = new Color [rows];

        for (int y = 0; y < rows; y++)
        {
            canvas[x][y] = Color(0, 0, 0);
        }
    }
}

int Canvas::Width() { return width; }
int Canvas::Height() { return height;}

Color& Canvas::operator() (int column, int row)
{
    if ((row < 0) || (row >= height)) return canvas[0][0];
    if ((column < 0) || (column >= width)) return canvas[0][0];

    return canvas[column][row];
}

std::string Canvas::GetPPM()
{
    std::string ppm = "";
    std::string line = "";

    // PPM Header
    ppm.append("P3"); ppm.append("\n");
    ppm.append(std::to_string(width)); ppm.append(" "); ppm.append(std::to_string(height)); ppm.append("\n");
    ppm.append("255"); ppm.append("\n");

    // Build the image
    Color c;
    int* rgb = new int[3];
    std::string num = "";
    int length = 0;
    for (int y = 0; y < height; y++)
    {
        // Build a line
        for (int x = 0; x < width; x++)
        {
            c = canvas[x][y];
            rgb[0] = std::round(c.red * 255);
            rgb[1] = std::round(c.green * 255);
            rgb[2] = std::round(c.blue * 255);

            for (int i = 0; i < 3; i++)
            {
                if (rgb[i] < 0) rgb[i] = 0;
                if (rgb[i] > 255) rgb[i] = 255;

                num = std::to_string(rgb[i]);
                if (length + num.length() + 1 >= 70)
                {
                    line.append("\n");
                    length = 0;
                }
                else if (length > 0)
                {
                    line.append(" ");
                    length += 1;
                }

                line.append(num);
                length += num.length();
            }
        }

        line.append("\n");
        ppm.append(line);

        line = "";
        length = 0;
    }

    return ppm;
}
