from rt.tuple import *

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

    p = Projectile(Point(0, 1, 0), Vector(1, 1, 0).normalize)
    e = Environment(Vector(0, -0.1, 0), Vector(-0.01, 0, 0))
    ticks = 0

    while (p.Position.y >= 0):
        print(f'Tick {ticks}:  {p.Position}')
        ticks += 1
        p = Tick(e, p)

    print('Done!')
