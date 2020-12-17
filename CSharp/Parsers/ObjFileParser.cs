using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSharp
{
    public class ObjFileParser
    {
        private bool cleanVertices;

        public ObjFileParser(string fullPath)
        {
            cleanVertices = true;
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

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Ignored += 1;
                    continue;
                }

                parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts[0] == "v")
                {
                    AddVertice(parts);
                }
                else if (parts[0] == "vn")
                {
                    AddNormal(parts);
                }
                else if (parts[0] == "f")
                {
                    if (cleanVertices)
                    {
                        // Vertices are complete and can be processed
                        ProcessVertices();
                        cleanVertices = false;
                    }

                    AddFace(group, parts);
                }
                else if (parts[0] == "g")
                {
                    group = AddGroup(parts);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    Ignored += 1;
                }
            }

            return true;
        }

        private Group AddGroup(string[] parts)
        {
            var group = new Group();
            Groups.Add(group);
            DefaultGroup.Add(group);

            return group;
        }

        private void AddFace(Group group, string[] parts)
        {
            int count = 0, index = -1;
            while ((index = parts[1].IndexOf('/', index + 1)) > -1)
                count += 1;

            int a, b, c, d, e, f, g, h, i;
            string[] fields1, fields2, fields3;
            Triangle tri;
            bool smooth = false;
            a = b = c = d = e = f = g = h = i = 0;

            fields1 = parts[1].Split('/');

            // Fan Triangulation
            for (int j = 2; j < parts.Length - 1; j++)
            {
                fields2 = parts[j].Split('/');
                fields3 = parts[j + 1].Split('/');

                switch (count)
                {
                    case 0:  // Triangle
                    case 1:  // Triangle with texture
                        a = int.Parse(fields1[0]);
                        b = int.Parse(fields2[0]);
                        c = int.Parse(fields3[0]);
                        break;

                    case 2:  // Triangle with texture and normal (smooth triangle)
                        smooth = true;

                        if (!int.TryParse(fields1[0], out a))
                            a = 0;
                        if (!int.TryParse(fields1[1], out d))
                            d = 0;
                        if (!int.TryParse(fields1[2], out g))
                            g = 0;

                        if (!int.TryParse(fields2[0], out b))
                            b = 0;
                        if (!int.TryParse(fields2[1], out e))
                            e = 0;
                        if (!int.TryParse(fields2[2], out h))
                            h = 0;

                        if (!int.TryParse(fields3[0], out c))
                            c = 0;
                        if (!int.TryParse(fields3[1], out f))
                            f = 0;
                        if (!int.TryParse(fields3[2], out i))
                            i = 0;
                        break;

                    default:
                        throw new Exception("Unknown Face Arrangement!");
                }
            }

            if (!smooth)
                tri = new Triangle(Vertices[a], Vertices[b], Vertices[c]);
            else
                tri = new SmoothTriangle(Vertices[a], Vertices[b], Vertices[c], Normals[g], Normals[h], Normals[i]);

            group.Add(tri);
        }

        private void AddNormal(string[] parts)
        {
            // Normals
            if (parts.Length >= 4)
            {
                var x = double.Parse(parts[1]);
                var y = double.Parse(parts[2]);
                var z = double.Parse(parts[3]);

                var vector = new Vector(x, y, z);

                Normals.Add(vector);
            }
        }

        private void AddVertice(string[] parts)
        {
            if (parts.Length >= 4)
            {
                var x = double.Parse(parts[1]);
                var y = double.Parse(parts[2]);
                var z = double.Parse(parts[3]);

                var point = new Point(x, y, z);

                Vertices.Add(point);
            }
        }

        /// <summary>
        /// Reduce all vertices to -1 to 1 on all axis
        /// </summary>
        private void ProcessVertices()
        {
            var box = new BoundingBox();
            
            foreach (var vert in Vertices)
            {
                box.Add(vert);
            }

            var sx = box.Max.x - box.Min.x;
            var sy = box.Max.y - box.Min.y;
            var sz = box.Max.z - box.Min.z;

            var scale = Math.Max(sx, Math.Max(sy, sz)) / 2.0;
            double x, y, z;
            Point v;

            Parallel.For(0, Vertices.Count, i =>
            {
                v = Vertices[i];
                x = (v.x - (box.Min.x + sx / 2.0)) / scale;
                y = (v.y - (box.Min.y + sy / 2.0)) / scale;
                z = (v.z - (box.Min.z + sz / 2.0)) / scale;

                Vertices[i] = new Point(x, y, z);
            });

            Min = new Point(box.Min / scale);
            Max = new Point(box.Max / scale);
        }
    }
}
