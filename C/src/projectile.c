#include <stdio.h>
#include <stdlib.h>
#include "tuple.h"

/***********************************************
 * A project that uses the ray tracer challenge
 * library to simulate a projectile.
 ***********************************************/

typedef struct {
    Tuple Position;
    Tuple Velocity;
} Projectile;

typedef struct
{
    Tuple Gravity;
    Tuple Wind;
} Environment;

Projectile Tick(Environment env, Projectile proj)
{
    Tuple position = addt(proj.Position, proj.Velocity);
    Tuple velocity = addt(proj.Velocity, addt(env.Gravity, env.Wind));

    Projectile a = {
                        { position.x, position.y, position.z },
                        { velocity.x, velocity.y, velocity.z }
                    };
    return a;
}

int main (int argc, char *argv[]) {

    printf("Fire the cannon!\r\n");
 
    Tuple v = {1, 1, 0};
    Projectile p = { {0, 1, 0}, normv(v) };
    Environment e = { {0, -0.1, 0}, { -0.01, 0, 0} };
    int ticks = 0;

    do
    {
        char *tuple = tuple_string(p.Position);
        printf("Tick %i: %s\r\n", ticks++, tuple);
        free(tuple);
        
        p = Tick(e, p);
    } while (p.Position.y >= 0);

    printf("Done!\r\n\r\n");

    return 0;
}