using System;
using System.Text;

namespace CSharp
{
    public class Canvas
    {
        Color[,] canvas;

        public Canvas(int columns, int rows)
        {
            IgnoreRange = true;
            Width = columns;
            Height = rows;

            canvas = new Color[columns, rows];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    canvas[x, y] = Color.Black;
                }
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public bool IgnoreRange { get; set; }

        public Color this[int column, int row]
        {
            get
            {
                if (IgnoreRange)
                {
                    if (!InRange(row, column))
                        return null;
                }
                else
                {
                    Limiters(ref row, ref column);
                }

                return canvas[column, row];
            }

            set
            {
                if (IgnoreRange)
                {
                    if (!InRange(row, column))
                        return;
                }
                else
                {
                    Limiters(ref row, ref column);
                }

                canvas[column, row] = value;
            }
        }

        public string GetPPM()
        {
            StringBuilder ppm = new StringBuilder();
            StringBuilder line = new StringBuilder();

            // PPM Header
            ppm.Append("P3").Append('\n');
            ppm.Append(Width).Append(" ").Append(Height).Append('\n');
            ppm.Append("255").Append('\n');

            // Build the image
            Color c;
            int[] rgb = new int[3];
            string num = string.Empty;
            int length = 0;
            for (int y = 0; y < Height; y++)
            {
                // Build a line
                for (int x = 0; x < Width; x++)
                {
                    c = canvas[x, y];
                    rgb[0] = (int)Math.Round(Limiters(c.red) * 255);
                    rgb[1] = (int)Math.Round(Limiters(c.green) * 255);
                    rgb[2] = (int)Math.Round(Limiters(c.blue) * 255);

                    for (int i = 0; i < rgb.Length; i++)
                    {
                        num = rgb[i].ToString();
                        if (length + num.Length + 1 >= 70)
                        {
                            line.Append('\n');
                            length = 0;
                        }
                        else if (length > 0)
                        {
                            line.Append(" ");
                            length += 1;
                        }

                        line.Append(num);
                        length += num.Length;
                    }
                }

                line.Append('\n');
                ppm.Append(line);

                line.Clear();
                length = 0;
            }

            return ppm.ToString();
        }

        public static Canvas SetPPM(string ppm)
        {
            int index = 0;
            ppm = ppm.Replace("\r", string.Empty);

            // Header
            string s = GetToken(ppm, ref index);
            if (!s.Equals("P3", StringComparison.OrdinalIgnoreCase))
                return null;

            int width, height, scale;
            s = GetToken(ppm, ref index);
            if (!int.TryParse(s, out width))
                return null;

            s = GetToken(ppm, ref index);
            if (!int.TryParse(s, out height))
                return null;

            s = GetToken(ppm, ref index);
            if (!int.TryParse(s, out scale))
                return null;

            var c = new Canvas(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    c[x, y] = GetColor(scale, ppm, ref index);
                }
            }

            return c;
        }

        private static Color GetColor(int scale, string ppm, ref int i)
        {
            int r, g, b;

            var s = GetToken(ppm, ref i);
            if (!int.TryParse(s, out r))
                return Color.Black;

            s = GetToken(ppm, ref i);
            if (!int.TryParse(s, out g))
                return Color.Black;
            
            s = GetToken(ppm, ref i);
            if (!int.TryParse(s, out b))
                return Color.Black;

            return new Color(r / (double)scale, g / (double)scale, b / (double)scale);
        }

        private static string GetToken(string s, ref int i)
        {
            SkipWhiteSpace(s, ref i);
            SkipComment(s, ref i);

            int start = i;
            int count = 0;

            while ((i < s.Length) && (s[i] != ' ') && (s[i] != '\n'))
            {
                i++;
                count++;
            }

            if (count > 0)
            {
                return s.Substring(start, count);
            }

            return string.Empty;
        }

        private static void SkipWhiteSpace(string s, ref int i)
        {
            while ((i < s.Length) && ((s[i] == ' ')) || (s[i] == '\n'))
                i++;
        }

        private static void SkipComment(string s, ref int i)
        {
            while ((i < s.Length) && (s[i] == '#'))
            {
                while ((i < s.Length) && (s[i] != '\n'))
                    i++;

                SkipWhiteSpace(s, ref i);
            }
        }

        private bool InRange(int row, int column)
        {
            return ((row >= 0) && (row < Height)) &&
                   ((column >= 0) && (column < Width));
        }

        private void Limiters(ref int row, ref int column)
        {
            if (row < 0) row = 0;
            if (row >= Height) row = Height - 1;
            if (column < 0) column = 0;
            if (column >= Width) column = Width - 1;
        }

        private static double Limiters(double color)
        {
            if (color < 0.0) return 0.0;
            if (color > 1.0) return 1.0;
            return color;
        }
    }
}
