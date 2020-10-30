using System;
using CSharp;

namespace Projectile
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
            #region Projectile Test

            Console.WriteLine("Fire the cannon!");
            
            var start = new Point(0, 1, 0);
            var velocity = (new Vector(1, 1.8, 0)).Normalize * 14.5;
            var p = new Projectile(start, velocity);
            
            var gravity = new Vector(0, -0.1, 0);
            var wind = new Vector(0, -0.1, 0);
            var e = new Environment(gravity, wind);

            var c = new Canvas(900, 550);
            var color = new Color(1, 0, 0);
            int ticks = 0;

            do
            {
                Console.WriteLine($"Tick {ticks++}:  {p.Position}");

                var y = c.Height - (int)Math.Round(p.Position.y);
                var x = (int)Math.Round(p.Position.x);
                c[x, y] = color;

                p = Tick(e, p);
            } while (p.Position.y >= 0);

            System.IO.File.WriteAllText("projectile.ppm", c.GetPPM());

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
