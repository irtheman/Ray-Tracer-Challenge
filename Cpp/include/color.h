#ifndef COLOR_H
#define COLOR_H

#include <iostream>

// Color Class
class Color
{
public:
    Color();
    Color(double Red, double Green, double Blue);
    double red, green, blue;

    Color operator+(const Color& rhs) const;
    Color operator-(const Color& rhs) const;
    Color operator*(const double& rhs) const;
    Color operator*(const Color& rhs) const;
    bool operator==(const Color& rhs) const;
    friend std::ostream& operator<<(std::ostream& os, const Color& p);
};

#endif
