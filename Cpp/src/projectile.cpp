#include <iostream>
#include "tuple.h"

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

    Projectile p = Projectile(Point(0, 1, 0), Vector(1, 1, 0).Normalize());
    Environment e = Environment(Vector(0, -0.1, 0), Vector(-0.01, 0, 0));
    int ticks = 0;

    do
    {
        std::cout << "Tick " << ticks++ << ": " << p.Position << std::endl;
        p = Tick(e, p);
    } while (p.Position.y >= 0);

    std::cout << "Done!" << std::endl;

    return 0;
}