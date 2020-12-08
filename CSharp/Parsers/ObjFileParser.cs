using System;
using System.Collections.Generic;
using System.IO;

namespace CSharp
{
    public class ObjFileParser
    {
        public ObjFileParser(string fullPath)
        {
            FullPath = fullPath;

            DefaultGroup = new Group();
            Groups = new List<Group>();

            Vertices = new List<Point>();
            Vertices.Add(Point.Zero);

            Normals = new List<Vector>();
            Normals.Add(Vector.Zero);

            Min = new Point(double.MaxValue, double.MaxValue, double.MaxValue);
            Max = new Point(double.MinValue, double.MinValue, double.MinValue);

            Parse(fullPath);
        }

        public string FullPath { get; }
        public long Ignored { get; private set; }
        public List<Point> Vertices { get; }
        public List<Vector> Normals { get; }
        public Group DefaultGroup { get; }
        public List<Group> Groups { get; }
        public Group ObjToGroup => DefaultGroup;
        public Point Min { get; private set; }
        public Point Max { get; private set; }
        public Point Center => new Point(((Max - Min) / 2.0) + Min);

        public bool Parse(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                return false;
            }

            var lines = File.ReadAllLines(fullPath);

            Group group = DefaultGroup;
            string[] parts;
            Point point;
            Vector vector;
            Triangle tri;
            double x, y, z;
            int a, b, c, d, e, f, g, h, i;
            foreach (var line in lines)
            {
                var ln = line.Trim().Replace("  ", " ");
                parts = ln.Split(' ');

                if (parts[0] == "v")
                {
                    // Vertices
                    if (parts.Length == 4)
                    {
                        x = double.Parse(parts[1]);
                        y = double.Parse(parts[2]);
                        z = double.Parse(parts[3]);

                        point = new Point(x, y, z);

                        if (x < Min.x || y < Min.y || z < Min.z)
                        {
                            Min = new Point(Math.Min(x, Min.x), Math.Min(y, Min.y), Math.Min(z, Min.z));
                        }
                        if (x > Max.x || y > Max.y || z > Max.z)
                        {
                            Max = new Point(Math.Max(x, Max.x), Math.Max(y, Max.y), Math.Max(z, Max.z));
                        }

                        Vertices.Add(point);
                    }
                }
                else if (parts[0] == "vn")
                {
                    // Normals
                    if (parts.Length == 4)
                    {
                        x = double.Parse(parts[1]);
                        y = double.Parse(parts[2]);
                        z = double.Parse(parts[3]);

                        vector = new Vector(x, y, z);

                        Normals.Add(vector);
                    }

                }
                else if (parts[0] == "f")
                {
                    // Fan Triangulation
                    for (int j = 2; j < parts.Length - 1; j++)
                    {
                        if (parts[1].IndexOf('/') == -1)
                        {
                            // Triangle
                            a = int.Parse(parts[1]);
                            b = int.Parse(parts[j]);
                            c = int.Parse(parts[j + 1]);

                            tri = new Triangle(Vertices[a], Vertices[b], Vertices[c]);
                        }
                        else
                        {
                            // Smooth Triangle
                            var fields = parts[1].Split('/');
                            if (!int.TryParse(fields[0], out a))
                                a = 0;
                            if (!int.TryParse(fields[1], out d))
                                d = 0;
                            if (!int.TryParse(fields[2], out g))
                                g = 0;
                            
                            fields = parts[j].Split('/');
                            if (!int.TryParse(fields[0], out b))
                                b = 0;
                            if (!int.TryParse(fields[1], out e))
                                e = 0;
                            if (!int.TryParse(fields[2], out h))
                                h = 0;

                            fields = parts[j + 1].Split('/');
                            if (!int.TryParse(fields[0], out c))
                                c = 0;
                            if (!int.TryParse(fields[1], out f))
                                f = 0;
                            if (!int.TryParse(fields[2], out i))
                                i = 0;

                            if (g == 0 || h == 0 || i == 0)
                                tri = new Triangle(Vertices[a], Vertices[b], Vertices[c]);
                            else
                                tri = new SmoothTriangle(Vertices[a], Vertices[b], Vertices[c], Normals[g], Normals[h], Normals[i]);
                        }

                        group.Add(tri);
                    }
                }
                else if (parts[0] == "g")
                {
                    // Groups
                    group = new Group();
                    Groups.Add(group);
                    DefaultGroup.Add(group);
                }
                else if (!string.IsNullOrWhiteSpace(ln))
                {
                    Ignored += 1;
                }
                }

                return true;
            }
        }
    }
