namespace CSharp
{
    public class Point : Tuple
    {
        public Point(double x, double y, double z) : base(x, y, z, 1.0)
        {
            // Nothing else to do
        }
    }
}
