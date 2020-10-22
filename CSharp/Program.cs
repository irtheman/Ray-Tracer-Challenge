using System;

namespace CSharp
{
    #region Projectile Test

    public class Projectile
    {
        public Projectile(Point pos, Vector vel)
        {
            Position = pos;
            Velocity = vel;
        }

        public Point Position { get; private set; }
        public Vector Velocity { get; private set; }
    }

    public class Environment
    {
        public Environment(Vector g, Vector w)
        {
            Gravity = g;
            Wind = w;
        }

        public Vector Gravity { get; private set; }
        public Vector Wind { get; private set; }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            #region Projectile Test

            Console.WriteLine("Fire the cannon!"); 
            var p = new Projectile(new Point(0, 1, 0), (new Vector(1, 1, 0)).Normalize);
            var e = new Environment(new Vector(0, -0.1, 0), new Vector(-0.01, 0, 0));
            int ticks = 0;

            do
            {
                Console.WriteLine($"Tick {ticks++}:  {p.Position}");
                p = Tick(e, p);
            } while (p.Position.y >= 0);

            Console.WriteLine("Done!");

            #endregion
        }

        #region Projectile Test

        public static Projectile Tick(Environment env, Projectile proj)
        {
            var position = proj.Position + proj.Velocity;
            var velocity = proj.Velocity + env.Gravity + env.Wind;

            return new Projectile(new Point(position.x, position.y, position.z),
                                  new Vector(velocity.x, velocity.y, velocity.z));
        }

        #endregion
    }
}
