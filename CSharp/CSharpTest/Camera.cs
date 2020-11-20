namespace CSharpTest
{
    internal class Camera
    {
        private int hsize;
        private int vsize;
        private double fieldOfView;

        public Camera(int hsize, int vsize, double fieldOfView)
        {
            this.hsize = hsize;
            this.vsize = vsize;
            this.fieldOfView = fieldOfView;
        }
    }
}