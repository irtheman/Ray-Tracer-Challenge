using System;
using System.Collections.Generic;

namespace CSharp
{
    public class AreaLight : ILight
    {
        public AreaLight(Point corner, Vector fullUVec, int uSteps, Vector fullVVec, int vSteps, Color intensity)
        {
            Corner = corner;
            UVec = fullUVec / uSteps;
            USteps = uSteps;
            VVec = fullVVec / vSteps;
            VSteps = vSteps;
            Samples = uSteps * vSteps;
            Position = new Point((fullUVec.x + fullVVec.x) / 2, (fullUVec.y + fullVVec.y) / 2, (fullUVec.z + fullVVec.z) / 2);
            Intensity = intensity;
            JitterBy = new Sequence();
        }

        public Point Corner { get; private set; }
        public Vector UVec { get; private set; }
        public int USteps { get; private set; }
        public Vector VVec { get; private set; }
        public int VSteps { get; private set; }
        public int Samples { get; private set; }
        public Point Position { get; private set; }
        public Color Intensity { get; private set; }
        public Sequence JitterBy { get; set; }

        public Point PointOnLight(int u, int v)
        {
            return new Point(Corner + 
                             (UVec * (u + (JitterBy != null ? JitterBy.Next : 0.5))) + 
                             (VVec * (v + (JitterBy != null ? JitterBy.Next : 0.5))));
        }

        public double IntensityAt(RTObject obj, Computations comps, World w)
        {
            var total = 0.0;

            for (int v = 0; v < VSteps; v++)
            {
                for (int u = 0; u < USteps; u++)
                {
                    var lightPosition = PointOnLight(u, v);
                    if (!w.IsShadowed(obj, comps, lightPosition, comps.OverPoint))
                    {
                        total += 1.0;
                    }
                }
            }

            return total / Samples;
        }

        public double IntensityAt(Point point, World w)
        {
            var total = 0.0;

            for (int v = 0; v < VSteps; v++)
            {
                for (int u = 0; u < USteps; u++)
                {
                    var lightPosition = PointOnLight(u, v);
                    if (!w.IsShadowed(lightPosition, point))
                    {
                        total += 1.0;
                    }
                }
            }

            return total / Samples;
        }

        public List<Point> SamplePoints()
        {
            var positions = new List<Point>();

            for (int v = 0; v < VSteps; v++)
            {
                for (int u = 0; u < USteps; u++)
                {
                    var lightPosition = PointOnLight(u, v);
                    positions.Add(lightPosition);
                }
            }

            return positions;
        }
    }
}
