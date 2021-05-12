package RTC.Render;

import RTC.Math.*;
import RTC.Material.*;

public class Camera {
    private int hSize, vSize;
    private double fov, pixelSize, halfWidth, halfHeight;
    private Matrix transform;
    private Matrix inverseTransform;

    public Camera(int HSize, int VSize, double FieldOfView) {
        hSize = HSize;
        vSize = VSize;
        fov = FieldOfView;

        transform = Matrix.Identity();
        inverseTransform = transform;

        double halfView = Math.tan(fov / 2.0);
        double aspect = HSize / (double) VSize;

        if (aspect >= 1.0)
        {
            halfWidth = halfView;
            halfHeight = halfView / aspect;
        }
        else
        {
            halfWidth = halfView * aspect;
            halfHeight = halfView;
        }

        pixelSize = (halfWidth * 2.0) / HSize;
    }

    public int getHSize() {
        return hSize;
    }

    public int getVSize() {
        return vSize;
    }

    public double getFOV() {
        return fov;
    }

    public double getPixelSize() {
        return pixelSize;
    }

    public void transform(Matrix m) {
        transform = m;
        inverseTransform = m.inverse();
    }

    public Ray rayForPixel(int x, int y)
    {
        double xOffset = (x + 0.5) * pixelSize;
        double yOffset = (y + 0.5) * pixelSize;
        double worldX = halfWidth - xOffset;
        double worldY = halfHeight - yOffset;
        Tuple pixel = inverseTransform.mul(new Point(worldX, worldY, -1));

        Point origin = new Point(inverseTransform.mul(Point.Zero));
        Vector direction = new Vector(pixel.sub(origin).norm());

        return new Ray(origin, direction);
    }

    public Canvas render(World w)
    {
        Canvas image = new Canvas(hSize, vSize);

        // Debug per pixel
        // Ray temp1 = RayForPixel(250, 160);
        // Color temp2 = w.ColorAt(temp1);

        for (int y = 0; y < vSize; y++)
        { 
            for (int x = 0; x < hSize; x++)
            {
                Ray ray = rayForPixel(x, y);
                Color color = w.colorAt(ray);
                image.writePixel(x, y, color);
            }
        }

        return image;
    }
}
