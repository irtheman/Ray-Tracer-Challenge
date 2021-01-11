using System;

namespace CSharp
{
    public class Material
    {
        public Material()
        {
            Color = Color.White;
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200.0;
            Reflective = 0.0;
            Transparency = 0.0;
            RefractiveIndex = 1.0;
        }

        public Color Color { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public double Reflective { get; set; }
        public double Transparency { get; set; }
        public double RefractiveIndex { get; set; }
        public RTPattern Pattern { get; set; }

        public Color Lighting(RTObject obj, ILight light, Point position, Vector eyev, Vector normalv, double intensity)
        {
            Color color;
            if (Pattern != null)
            {
                color = Pattern.PatternAtObject(obj, position);
            }
            else if (obj.Parent != null)
            {
                color = obj.Parent.Material.Lighting(obj.Parent, light, position, eyev, normalv, intensity);
            }
            else
            {
                color = Color;
            }

            var effectiveColor = color * light.Intensity;
            var ambient = effectiveColor * Ambient;
            var samples = light.SamplePoints();

            Color sum = Color.Black;

            foreach (var sample in samples)
            {
                Color diffuse;
                Color specular;

                var lightV = (new Vector(sample - position)).Normalize;
                var lightDotNormal = lightV.Dot(normalv);

                if ((lightDotNormal < 0) || intensity.IsEqual(0.0))
                {
                    diffuse = Color.Black; // inShadow ? Color.Green : Color.Black;
                    specular = Color.Black; // inShadow ? Color.Green : Color.Black;
                }
                else
                {
                    diffuse = effectiveColor * Diffuse * lightDotNormal;
                    var reflectV = (-lightV).Reflect(normalv);
                    var reflectDotEye = reflectV.Dot(eyev);

                    if (reflectDotEye <= 0)
                    {
                        specular = Color.Black;
                    }
                    else
                    {
                        var factor = Math.Pow(reflectDotEye, Shininess);
                        specular = light.Intensity * Specular * factor;
                    }
                }

                sum += diffuse;
                sum += specular;
            }

            return ambient + ((sum / light.Samples) * intensity);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Material;
            return ((other != null) &&
                    (Color.Equals(other.Color)) &&
                    (Ambient.IsEqual(other.Ambient)) &&
                    (Diffuse.IsEqual(other.Diffuse)) &&
                    (Specular.IsEqual(other.Specular)) &&
                    (Shininess.IsEqual(other.Shininess)) &&
                    (Reflective.IsEqual(other.Reflective)) &&
                    (Transparency.IsEqual(other.Transparency)) &&
                    (RefractiveIndex.IsEqual(other.RefractiveIndex)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ambient, Diffuse, Specular, Shininess, Reflective, Transparency, RefractiveIndex);
        }

        public override string ToString()
        {
            return $"{Color}: ({Ambient},{Diffuse},{Specular},{Shininess}, {Reflective}, {Transparency}, {RefractiveIndex})";
        }
    }
}
