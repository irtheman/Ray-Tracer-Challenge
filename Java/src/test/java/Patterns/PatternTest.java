import RTC.Math.*;
import RTC.Material.*;
import RTC.Shapes.*;
import RTC.Patterns.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class PatternTest {
    @Test
    public void PatternTest()
    {
        Pattern pattern = new StripePattern(Color.White, Color.Black);
        Assertions.assertEquals(pattern.getTransform(), Matrix.Identity());
    }

    @Test
    public void PatternApplyTransform()
    {
        Pattern pattern = new StripePattern(Color.White, Color.Black);
        pattern.setTransform(Matrix.Translation(1, 2, 3));
        Assertions.assertEquals(pattern.getTransform(), Matrix.Translation(1, 2, 3));
    }

    @Test
    public void PatternY()
    {
        Pattern pattern = new StripePattern(Color.White, Color.Black);

        Assertions.assertEquals(pattern.patternAt(Point.Zero), Color.White);
        Assertions.assertEquals(pattern.patternAt(Point.PointY), Color.White);
        Assertions.assertEquals(pattern.patternAt(new Point(0, 2, 0)), Color.White);
    }

    @Test
    public void PatternZ()
    {
        Pattern pattern = new StripePattern(Color.White, Color.Black);

        Assertions.assertEquals(pattern.patternAt(Point.Zero), Color.White);
        Assertions.assertEquals(pattern.patternAt(Point.PointZ), Color.White);
        Assertions.assertEquals(pattern.patternAt(new Point(0, 0, 2)), Color.White);
    }

    @Test
    public void PatternX()
    {
        Pattern pattern = new StripePattern(Color.White, Color.Black);

        Assertions.assertEquals(pattern.patternAt(Point.Zero), Color.White);
        Assertions.assertEquals(pattern.patternAt(new Point(0.9, 0, 0)), Color.White);
        Assertions.assertEquals(pattern.patternAt(Point.PointX), Color.Black);
        Assertions.assertEquals(pattern.patternAt(new Point(-0.1, 0, 0)), Color.Black);
        Assertions.assertEquals(pattern.patternAt(new Point(-1, 0, 0)), Color.Black);
        Assertions.assertEquals(pattern.patternAt(new Point(-1.1, 0, 0)), Color.White);
    }

    @Test
    public void PatternObjectTransform()
    {
        Sphere obj = new Sphere();
        obj.setTransform(Matrix.Scaling(2, 2, 2));
        Pattern pattern = new TestPattern();
        Color c = pattern.patternAtShape(obj, new Point(2, 3, 4));

        Assertions.assertEquals(c, new Color(1, 1.5, 2));
    }

    @Test
    public void PatternPatternTransform()
    {
        Sphere obj = new Sphere();
        Pattern pattern = new TestPattern();
        pattern.setTransform(Matrix.Scaling(2, 2, 2));
        Color c = pattern.patternAtShape(obj, new Point(2, 3, 4));

        Assertions.assertEquals(c, new Color(1, 1.5, 2));
    }

    @Test
    public void PatternObjectPatternTransform()
    {
        Sphere obj = new Sphere();
        obj.setTransform(Matrix.Scaling(2, 2, 2));
        Pattern pattern = new TestPattern();
        pattern.setTransform(Matrix.Translation(0.5, 1, 1.5));
        Color c = pattern.patternAtShape(obj, new Point(2.5, 3, 3.5));

        Assertions.assertEquals(c, new Color(0.75, 0.5, 0.25));
    }
}
