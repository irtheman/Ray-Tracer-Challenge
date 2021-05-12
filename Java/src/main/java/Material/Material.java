package RTC.Material;

import RTC.Math.*;
import RTC.Lights.*;
import RTC.Patterns.*;
import RTC.Shapes.*;

import java.util.Objects;

public class Material {
    public static int MaterialId = 0;

    private int id;
    private Pattern pattern;
    private Color color;
    private double ambient;
    private double diffuse;
    private double specular;
    private double shininess;

    public Material() {
        id = MaterialId++;

        pattern = null;
        color = Color.White;
        ambient = 0.1;
        diffuse = 0.9;
        specular = 0.9;
        shininess = 200.0;
    }

    public Pattern getPattern() {
        return pattern;
    }

    public void setPattern(Pattern p) {
        pattern = p;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color c) {
        color = c;
    }
    
    public double getAmbient() {
        return ambient;
    }

    public void setAmbient(double a) {
        ambient = a;
    }

    public double getDiffuse() {
        return diffuse;
    }

    public void setDiffuse(double d) {
        diffuse = d;
    }

    public double getSpecular() {
        return specular;
    }

    public void setSpecular(double s) {
        specular = s;
    }

    public double getShininess() {
        return shininess;
    }

    public void setShininess(double s) {
        shininess = s;
    }

    public Color lighting(Shape obj, PointLight light, Point point, Vector eyev, Vector normalv, boolean inShadow) {
        Color c;
        if (pattern != null) {
            c = pattern.patternAtShape(obj, point);
        } else {
            c = color;
        }

        Color amb, diff, spec;
        Color effectiveColor = c.mul(light.getIntensity());
        Tuple lightv = (light.getPosition().sub(point)).norm();

        amb = effectiveColor.mul(ambient);

        double lightDotNormal = lightv.dot(normalv);
        if (inShadow || (lightDotNormal < 0.0)) {
            diff = Color.Black;
            spec = Color.Black;
        } else {
            diff = effectiveColor.mul(diffuse).mul(lightDotNormal);
            Tuple reflectv = lightv.neg().reflect(normalv);
            double reflectDotEye = reflectv.dot(eyev);
            if (reflectDotEye <= 0.0) {
                spec = Color.Black;
            } else {
                double factor = Math.pow(reflectDotEye, shininess);
                spec = light.getIntensity().mul(specular).mul(factor);
            }
        }

        return amb.add(diff).add(spec);
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof Material)) {
            return false;
        }

        Material m = (Material) o;
        return color.equals(m.getColor()) &&
               Helper.IsEqual(ambient, m.getAmbient()) &&
               Helper.IsEqual(diffuse, m.getDiffuse()) &&
               Helper.IsEqual(specular, m.getSpecular()) &&
               Helper.IsEqual(shininess, m.getShininess());
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, ambient, diffuse, specular, shininess);
    }

    @Override
    public String toString() {
        return String.format("Material %s %f %f %f %f Id: %d", color.toString(), ambient, diffuse, specular, shininess, id);
    }    
}
