#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "tuple.h"
#include "color.h"
#include "canvas.h"

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
    Tuple start = {0, 1, 0};
    Tuple velocity = mult(normv(v), 10);
    Projectile p = { start, velocity };

    Tuple gravity = {0, -0.1, 0};
    Tuple wind = {-0.01, 0, 0 };
    Environment e = { gravity, wind };
    
    Canvas c = create_canvas(900, 550);
    Color color = create_color(1, 0, 0);
    int ticks = 0;

    do
    {
        char *tuple = tuple_string(p.Position);
        printf("Tick %i: %s\r\n", ticks++, tuple);
        free(tuple);
        
        int y = c.height - round(p.Position.y);
        int x = round(p.Position.x);
        setPixel(c, x, y, color);

        p = Tick(e, p);
    } while (p.Position.y >= 0);

    FILE *f = fopen("projectile.ppm", "w");
    fprintf(f, getPPM(c));
    fclose(f);

    printf("Done!\r\n\r\n");

    return 0;
}