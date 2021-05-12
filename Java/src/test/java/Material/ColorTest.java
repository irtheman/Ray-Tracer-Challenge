import RTC.Material.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

public class ColorTest 
{
    @Test
    public void CreateColor()
    {
        Color c = new Color(-0.5, 0.4, 1.7);

        Assertions.assertEquals(-0.5, c.getR());
        Assertions.assertEquals(0.4, c.getG());
        Assertions.assertEquals(1.7, c.getB());
    }

    @Test
    public void AddingTwoColors()
    {
        Color a1 = new Color(0.9, 0.6, 0.75);
        Color a2 = new Color(0.7, 0.1, 0.25);
        Color expected = new Color(1.6, 0.7, 1.0);
        Color actual = a1.add(a2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void SubtractingTwoColors()
    {
        Color a1 = new Color(0.9, 0.6, 0.75);
        Color a2 = new Color(0.7, 0.1, 0.25);
        Color expected = new Color(0.2, 0.5, 0.5);
        Color actual = a1.sub(a2);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void MultiplyColorByScalar() {
        Color a = new Color(0.2, 0.3, 0.4);
        Color expected = new Color(0.4, 0.6, 0.8);
        Color actual = a.mul(2);

        Assertions.assertEquals(expected, actual);
    }
    
    @Test
    public void MultiplyColorByColor()
    {
        Color c1 = new Color(1, 0.2, 0.4);
        Color c2 = new Color(0.9, 1, 0.1);
        Color expected = new Color(0.9, 0.2, 0.04);
        Color actual = c1.mul(c2);

        Assertions.assertEquals(expected, actual);
    }
}
