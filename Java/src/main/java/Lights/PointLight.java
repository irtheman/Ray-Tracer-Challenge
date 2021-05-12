package RTC.Lights;

import RTC.Math.*;
import RTC.Material.*;
import java.util.Objects;

public class PointLight {
    private static int LightId = 0;

    private int id;
    private Point position;
    private Color intensity;

    public PointLight(Point pos, Color color) {
        id = LightId++;
        position = pos;
        intensity = color;
    }

    public Point getPosition() {
        return position;
    }

    public Color getIntensity() {
        return intensity;
    }

    @Override
    public boolean equals(Object o)
    {
        if (o == this) {
            return true;
        }

        if ((o == null) || !(o instanceof PointLight)) {
            return false;
        }

        PointLight p = (PointLight) o;
        return intensity.equals(p.getIntensity()) &&
               position.equals(p.getPosition());
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, position, intensity);
    }

    @Override
    public String toString() {
        return String.format("PointLight %s %s Id: %d", intensity.toString(), position.toString(), id);
    }    }
