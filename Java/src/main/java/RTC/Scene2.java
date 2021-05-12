package RTC;

import RTC.Shapes.*;
import RTC.Render.*;
import RTC.Material.*;
import RTC.Patterns.*;
import RTC.Lights.*;
import RTC.Math.*;

import java.io.FileWriter;
import java.io.IOException;

public class Scene2
{
    public static void main( String[] args )
    {
        System.out.println("Generating a plane with patterns scene!");

        // Floor of scene
        Shape floor = new Plane();
        floor.setTransform(Matrix.Translation(0, -0.1, 0));
        floor.getMaterial().setSpecular(0);
        floor.getMaterial().setPattern(new CheckerPattern(Color.Green, Color.White));

        // Middle Sphere
        Sphere middle = new Sphere();
        middle.setTransform(Matrix.Translation(-0.5, 1, 0.5));
        middle.getMaterial().setDiffuse(0.7);
        middle.getMaterial().setSpecular(0.3);
        middle.getMaterial().setPattern(new GradientPattern(Color.Red, Color.Blue));
        middle.getMaterial().getPattern().setTransform(Matrix.Scaling(2, 2, 2).mul(Matrix.Translation(-0.5, 0, 0)));

        // Right Sphere
        Sphere right = new Sphere();
        right.setTransform(Matrix.Translation(1.5, 0.5, -0.5).mul(
                          Matrix.Scaling(0.5, 0.5, 0.5)));
        right.getMaterial().setDiffuse(0.7);
        right.getMaterial().setSpecular(0.3);
        right.getMaterial().setPattern(new RingPattern(Color.White, Color.Red));
        right.getMaterial().getPattern().setTransform(Matrix.RotationZ(Math.PI / 2).mul(Matrix.Scaling(0.15, 0.15, 0.15)));

        // Left Sphere
        Sphere left = new Sphere();
        left.setTransform(Matrix.Translation(-1.5, 0.33, -0.75).mul(
                          Matrix.Scaling(0.33, 0.33, 0.33)));
        left.getMaterial().setColor(new Color(1, 0.8, 0.1));
        left.getMaterial().setDiffuse(0.7);
        left.getMaterial().setSpecular(0.3);

        World world = new World();
        world.setLight(new PointLight(new Point(-10, 10, -10), Color.White));
        world.setObject(floor);
        world.setObject(middle);
        world.setObject(right);
        world.setObject(left);

        
        Camera camera = new Camera(400, 200, Math.PI / 3);
        camera.transform(Matrix.ViewTransform(new Point(0, 1.5, -5),
                                                  Point.PointY,
                                                  Vector.VectorY));

        Canvas c = camera.render(world);

        try {
            FileWriter file = new FileWriter("target/scene2.ppm");
            file.write(c.toPPM());
            file.close();

        } catch (IOException e) {
            System.out.println("An error occurred...");
            e.printStackTrace();
        }

        System.out.println("Done!");    }
}
