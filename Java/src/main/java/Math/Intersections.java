package RTC.Math;

import java.util.LinkedList;

public class Intersections {
    LinkedList<Intersection> list;

    public Intersections() {
        list = new LinkedList<Intersection>();
    }

    public Intersections(Intersection...intersects)
    {
        this();
        for (Intersection i : intersects) {
            add(i);
        }
    }

    public int size() {
        return list.size();
    }

    public void add(Intersections i) {
        for (int j = 0; j < i.size(); j++) {
            add(i.get(j));
        }
    }

    public void add(Intersection i) {
        int index = -1;

        for (int j = 0; j < list.size(); j++)
        {
            Intersection x = list.get(j);
            if (i.getT() <= x.getT()) {
                index = j;
                break;
            }
        }

        if (index == -1) {
            list.add(i);
        } else {
            list.add(index, i);
        }
    }

    public Intersection get(int index) {
        return list.get(index);
    }

    public Intersection hit() {
        Intersection result = null;

        for (int j = 0; j < list.size(); j++)
        {
            Intersection x = list.get(j);
            if (x.getT() >= 0.0) {
                result = x;
                break;
            }
        }

        return result;
    }
}
