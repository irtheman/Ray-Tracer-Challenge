package RTC;

import RTC.Shapes.*;
import RTC.Render.*;
import RTC.Material.*;
import RTC.Lights.*;
import RTC.Math.*;

import java.io.FileWriter;
import java.io.IOException;

public class Scene
{
    public static void main( String[] args )
    {
        System.out.println("Generating a scene!");

        // Floor of scene
        Sphere floor = new Sphere();
        floor.setTransform(Matrix.Scaling(10, 0.01, 10));
        floor.getMaterial().setColor(new Color(1, 0.9, 0.9));
        floor.getMaterial().setSpecular(0);

        // Left Wall
        Sphere leftWall = new Sphere();
        leftWall.setTransform(Matrix.Translation(0, 0, 5).mul(
                             Matrix.RotationY(-Math.PI / 4)).mul(
                             Matrix.RotationX(Math.PI / 2)).mul(
                             Matrix.Scaling(10, 0.01, 10)));
        leftWall.setMaterial(floor.getMaterial());

        // Right Wall
        Sphere rightWall = new Sphere();
        rightWall.setTransform(Matrix.Translation(0, 0, 5).mul(
                             Matrix.RotationY(Math.PI / 4)).mul(
                             Matrix.RotationX(Math.PI / 2)).mul(
                             Matrix.Scaling(10, 0.01, 10)));
        rightWall.setMaterial(floor.getMaterial());

        // Middle Sphere
        Sphere middle = new Sphere();
        middle.setTransform(Matrix.Translation(-0.5, 1, 0.5));
        middle.getMaterial().setColor(new Color(0.1, 1, 0.5));
        middle.getMaterial().setDiffuse(0.7);
        middle.getMaterial().setSpecular(0.3);

        // Right Sphere
        Sphere right = new Sphere();
        right.setTransform(Matrix.Translation(1.5, 0.5, -0.5).mul(
                          Matrix.Scaling(0.5, 0.5, 0.5)));
        right.getMaterial().setColor(new Color(0.5, 1, 0.1));
        right.getMaterial().setDiffuse(0.7);
        right.getMaterial().setSpecular(0.3);

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
        world.setObject(leftWall);
        world.setObject(rightWall);
        world.setObject(middle);
        world.setObject(right);
        world.setObject(left);

        
        Camera camera = new Camera(100, 50, Math.PI / 3);
        camera.transform(Matrix.ViewTransform(new Point(0, 1.5, -5),
                                                  Point.PointY,
                                                  Vector.VectorY));

        Canvas c = camera.render(world);

        try {
            FileWriter file = new FileWriter("target/scene.ppm");
            file.write(c.toPPM());
            file.close();

        } catch (IOException e) {
            System.out.println("An error occurred...");
            e.printStackTrace();
        }

        System.out.println("Done!");    }
}
