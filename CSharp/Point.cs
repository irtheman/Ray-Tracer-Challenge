namespace CSharp
{
    public class Point : Tuple
    {
        public Point(double x, double y, double z) : base(x, y, z, 1.0)
        {
            // Nothing else to do
        }

        public Point(Tuple t) : base(t.x, t.y, t.z, 1.0)
        {
            // Nothing else to do
        }
    }
}
