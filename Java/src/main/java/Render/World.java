package RTC.Render;

import RTC.Shapes.*;
import RTC.Lights.*;
import RTC.Math.*;
import RTC.Material.*;

import java.util.LinkedList;

public class World {
    private PointLight light;
    private LinkedList<Shape> objects;

    public World() {
        light = null;
        objects = new LinkedList<Shape>();
    }

    public static World getDefault() {
        World w = new World();

        PointLight l = new PointLight(new Point(-10, 10, -10), Color.White);

        Sphere s1 = new Sphere();
        Material m = s1.getMaterial();
        m.setColor(new Color(0.8, 1.0, 0.6));
        m.setDiffuse(0.7);
        m.setSpecular(0.2);

        Sphere s2 = new Sphere();
        s2.setTransform(Matrix.Scaling(0.5, 0.5, 0.5));

        w.setLight(l);
        w.setObject(s1);
        w.setObject(s2);

        return w;
    }

    public PointLight getLight() {
        return light;

    }
    public void setLight(PointLight Light) {
        light = Light;
    }

    public Shape getObject(int index) {
        return objects.get(index);
    }

    public void setObject(Shape obj) {
        objects.add(obj);
    }

    public Intersections intersectWorld(Ray r) {
        Intersections results = new Intersections();

        for (Shape obj : objects) {
            results.add(obj.intersect(r));
        }

        return results;
    }

    public Color shadeHit(Shape obj, Computations comps) {
        boolean shadowed = isShadowed(comps.getOverPoint());
        Material m = comps.getObject().getMaterial();

        return m.lighting(obj, light, comps.getOverPoint(), comps.getEyeVector(), comps.getNormalVector(), shadowed);
    }

    public Color colorAt(Ray ray) {
        Intersections i = intersectWorld(ray);
        Intersection hit = i.hit();
        Color result;

        if (hit == null)
        {
            result = Color.Black;
        }
        else
        {
            Computations comps = hit.prepareComputations(ray);
            result = shadeHit(hit.getObject(), comps);
        }

        return result;
    }

    public boolean isShadowed(Point point) {
        Tuple v = light.getPosition().sub(point);
        double distance = v.mag();
        Tuple direction = v.norm();

        Ray r = new Ray(point, new Vector(direction));
        Intersections xs = intersectWorld(r);

        Intersection h = xs.hit();
        if ((h != null) && (h.getT() < distance)) {
            return true;
        } else {
            return false;
        }
    }
}