namespace CSharp
{
    public class Point : Tuple
    {
        public static readonly Point Zero = new Point(0, 0, 0);
        public static readonly Point Ones = new Point(1, 1, 1);
        public static readonly Point PointX = new Point(1, 0, 0);
        public static readonly Point PointY = new Point(0, 1, 0);
        public static readonly Point PointZ = new Point(0, 0, 1);

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
