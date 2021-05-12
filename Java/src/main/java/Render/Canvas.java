package RTC.Render;

import RTC.Material.*;

public class Canvas {
    private int width, height;
    private Color[][] canvas;

    public Canvas(int Width, int Height)
    {
        width = Width;
        height = Height;

        canvas = new Color[width][height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                canvas[x][y] = Color.Black;
            }
        }
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public void writePixel(int x, int y, Color c) {
        canvas[x][y] = c;
    }

    public Color pixelAt(int x, int y) {
        return canvas[x][y];
    }

    public String toPPM() {
        StringBuilder sb = new StringBuilder();
        sb.append("P3\n")
          .append(width).append(" ").append(height).append("\n")
          .append("255\n");
        
        StringBuilder line = new StringBuilder();
        double d = 0.0;
        Color c;
        String s;
        int color;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                c = canvas[x][y];
                for (int i = 0; i < 3; i++)
                {
                    switch (i) {
                        case 0:
                            d = c.getR();
                            break;
                        case 1:
                            d = c.getG();
                            break;
                        case 2:
                            d = c.getB();
                            break;
                    }

                    color = (int) Math.round(d * 255);
                    if (color < 0) color = 0;
                    if (color > 255) color = 255;
                    s = Integer.toString(color);

                    if (line.length() + s.length() + 1 > 70)
                    {
                        sb.append(line).append("\n");
                        line.setLength(0);
                    }

                    if (line.length() > 0) line.append(" ");
                    line.append(s);
                }
            }

            sb.append(line).append("\n");
            line.setLength(0);
        }

        return sb.toString();
    }
}
