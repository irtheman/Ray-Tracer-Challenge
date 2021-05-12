import RTC.Material.*;
import RTC.Render.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class CanvasTest {
    @Test
    public void CreateCanvas() {
        Canvas c = new Canvas(10, 20);

        Assertions.assertEquals(10, c.getWidth());
        Assertions.assertEquals(20, c.getHeight());

        for (int x = 0; x < c.getWidth(); x++)
        {
            for (int y = 0; y < c.getHeight(); y++)
            {
                Assertions.assertEquals(c.pixelAt(x, y), Color.Black);
            }
        }
    }

    @Test
    public void WritePixelToCanvas() {
        Canvas c = new Canvas(10, 20);
        c.writePixel(2, 3, Color.Red);

        Assertions.assertEquals(c.pixelAt(2, 3), Color.Red);
    }

    @Test
    public void ConstructingPPMHeader() {
        Canvas c = new Canvas(5, 3);

        String ppm = c.toPPM();
        String[] lines = ppm.split("[\\r\\n]+");

        Assertions.assertEquals("P3", lines[0]);
        Assertions.assertEquals("5 3", lines[1]);
        Assertions.assertEquals("255", lines[2]);
    }

    @Test
    public void ConstructionPPMPixelData() {
        Canvas c = new Canvas(5, 3);
        Color c1 = new Color(1.5, 0, 0);
        Color c2 = new Color(0, 0.5, 0);
        Color c3 = new Color(-0.5, 0, 1);

        c.writePixel(0, 0, c1);
        c.writePixel(2, 1, c2);
        c.writePixel(4, 2, c3);

        String ppm = c.toPPM();
        String[] lines = ppm.split("[\\r\\n]+");

        Assertions.assertEquals("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", lines[3]);
        Assertions.assertEquals("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", lines[4]);
        Assertions.assertEquals("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", lines[5]);
    }

    @Test
    public void SplittingLongLinesInPPMFiles() {
        Canvas c = new Canvas(10, 2);
        Color color = new Color(1, 0.8, 0.6);
        for (int x = 0; x < c.getWidth(); x++)
        {
            for (int y = 0; y < c.getHeight(); y++)
            {
                c.writePixel(x, y, color);
            }
        }

        String ppm = c.toPPM();
        String lines[] = ppm.split("[\\r\\n]+");

        Assertions.assertEquals("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", lines[3]);
        Assertions.assertEquals("153 255 204 153 255 204 153 255 204 153 255 204 153", lines[4]);
        Assertions.assertEquals("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", lines[5]);
        Assertions.assertEquals("153 255 204 153 255 204 153 255 204 153 255 204 153", lines[6]);
    }

    @Test
    public void PPMFileEndsWithNewLine()
    {
        Canvas c = new Canvas(5, 3);
        String ppm = c.toPPM();

        Assertions.assertEquals('\n', ppm.charAt(ppm.length() - 1));
    }
}
