#include <math.h>
#include "color.h"
#include "rtmath.h"

/***********************************************
 *               Color members
************************************************/

Color::Color() : red(0), green(0), blue(0) { }

Color::Color(double Red, double Green, double Blue) : red(Red), green(Green), blue(Blue)
{
    // Nothing to do here
}

Color Color::operator+(const Color& rhs) const
{
    return Color(red + rhs.red,
                 green + rhs.green,
                 blue + rhs.blue);
}

Color Color::operator-(const Color& rhs) const
{
    return Color(red - rhs.red,
                 green - rhs.green,
                 blue - rhs.blue);
}

Color Color::operator*(const double& rhs) const
{
    return Color(red * rhs,
                 green * rhs,
                 blue * rhs);
}

Color Color::operator*(const Color& rhs) const
{
    return Color(red * rhs.red,
                 green * rhs.green,
                 blue * rhs.blue);
}

bool Color::operator==(const Color& rhs) const
{
    return (this == &rhs) ||
            (cmpd(red, rhs.red) &&
            cmpd(green, rhs.green) &&
            cmpd(blue, rhs.blue));
}

std::ostream& operator<<(std::ostream& os, const Color& p)
{
    return os << "(" << p.red << "," << p.green << "," << p.blue << ")"; 
}
