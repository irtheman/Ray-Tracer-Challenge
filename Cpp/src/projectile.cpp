#include <fstream>
#include <iostream>
#include <cmath>
#include "tuple.h"
#include "color.h"
#include "canvas.h"

/**************************************************************
 * Using the raytracer library for creating a projectile test.
 **************************************************************/

class Projectile
{
public:
    Projectile(Point pos, Vector vel) : Position(pos), Velocity(vel)
    {
        // Nothing to do
    }

    Point Position;
    Vector Velocity;
};

class Environment
{
public:
    Environment(Vector g, Vector w) : Gravity(g), Wind(w)
    {
        // Nothing to do
    }

    Vector Gravity;
    Vector Wind;
};

Projectile Tick(Environment env, Projectile proj)
{
    Point position = proj.Position + proj.Velocity;
    Vector velocity = proj.Velocity + env.Gravity + env.Wind;

    return Projectile(Point(position.x, position.y, position.z),
                      Vector(velocity.x, velocity.y, velocity.z));
}

int main (int argc, char *argv[]) {

    std::cout << "Fire the cannon!" << std::endl;

    Tuple t = Tuple(1,2,3,4);
    std::cout << "Tuple: " << t << std::endl;

    Point start = Point(0, 1, 0);
    Vector velocity = Vector(1, 1, 0).Normalize() * 10;
    Projectile p = Projectile(start, velocity);

    Vector gravity = Vector(0, -0.1, 0);
    Vector wind = Vector(-0.01, 0, 0);
    Environment e = Environment(gravity, wind);

    int ticks = 0;

    Canvas c = Canvas(900, 550);
    Color color = Color(1, 0, 0);

    do
    {
        std::cout << "Tick " << ticks++ << ": " << p.Position << std::endl;

        int y = c.Height() - round(p.Position.y);
        int x = round(p.Position.x);
        c(x, y) = color;

        p = Tick(e, p);
    } while (p.Position.y >= 0);

    std::ofstream out("projectile.ppm");
    out << c.GetPPM();
    out.close();

    std::cout << "Done!" << std::endl;

    return 0;
}