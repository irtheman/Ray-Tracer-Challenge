from rt.tuple import *
from rt.color import *
from rt.canvas import *

class Projectile(object):
    def __init__(self, pos, vel):
        self.Position = pos
        self.Velocity = vel

class Environment(object):
    def __init__(self, g, w):
        self.Gravity = g
        self.Wind = w

def Tick(env, proj):
    p = proj.Position + proj.Velocity
    v = proj.Velocity + env.Gravity + env.Wind

    return Projectile(Point(p.x, p.y, p.z), Vector(v.x, v.y, v.z))

if __name__ == '__main__':
    print('Fire the cannon!')

    start = Point(0, 1, 0)
    velocity = Vector(1, 1, 0).normalize * 10
    p = Projectile(start, velocity)

    gravity = Vector(0, -0.1, 0)
    wind = Vector(-0.01, 0, 0)
    e = Environment(gravity, wind)

    c = Canvas(900, 550)
    color = Color(1, 0, 0)
    ticks = 0

    while (p.Position.y >= 0):
        print(f'Tick {ticks}:  {p.Position}')

        y = c.Height - int(round(p.Position.y))
        x = int(round(p.Position.x))
        c[[x, y]] = color

        ticks += 1
        p = Tick(e, p)

    file = open(r'projectile.ppm', 'w')
    file.write(c.GetPPM())
    file.close()

    print('Done!')
