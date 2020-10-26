#include <stdio.h>
#include "tuple.h"

struct Projectile {
    struct Tuple Position;
    struct Tuple Velocity;
};

struct Environment
{
    struct Tuple Gravity;
    struct Tuple Wind;
};

struct Projectile Tick(struct Environment env, struct Projectile proj)
{
    struct Tuple position = tadd(proj.Position, proj.Velocity);
    struct Tuple velocity = tadd(proj.Velocity, tadd(env.Gravity, env.Wind));

    struct Projectile a = {
                            { position.x, position.y, position.z },
                            { velocity.x, velocity.y, velocity.z }
                          };
    return a;
}

int main (int argc, char *argv[]) {

    printf("Fire the cannon!\r\n");
 
    struct Tuple v = {1, 1, 0};
    struct Projectile p = { {0, 1, 0}, tnorm(v) };
    struct Environment e = { {0, -0.1, 0}, { -0.01, 0, 0} };
    int ticks = 0;

    do
    {
        printf("Tick %i: %s\r\n", ticks++, tuple_string(p.Position));
        p = Tick(e, p);
    } while (p.Position.y >= 0);

    printf("Done!");

    return 0;
}