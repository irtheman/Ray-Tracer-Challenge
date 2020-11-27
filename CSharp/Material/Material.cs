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
        }

        public Color Color { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }

        public Color Lighting(PointLight light, Point position, Vector eyev, Vector normalv, bool inShadow)
        {
            var effectiveColor = Color * light.Intensity;
            var lightV = (new Vector(light.Position - position)).Normalize;
            var lightDotNormal = lightV.Dot(normalv);
            var ambient = effectiveColor * Ambient;
            Color diffuse;
            Color specular;

            if ((lightDotNormal < 0) || inShadow)
            {
                diffuse = Color.Black;
                specular = Color.Black;
            }
            else
            {
                diffuse = effectiveColor * Diffuse * lightDotNormal;
                var reflectV = -lightV.Reflect(normalv);
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

            return ambient + diffuse + specular;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Material;
            return ((other != null) &&
                    (Color.Equals(other.Color)) &&
                    (Ambient.IsEqual(other.Ambient)) &&
                    (Diffuse.IsEqual(other.Diffuse)) &&
                    (Specular.IsEqual(other.Specular)) &&
                    (Shininess.IsEqual(other.Shininess)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ambient, Diffuse, Specular, Shininess);
        }

        public override string ToString()
        {
            return $"{Color}: ({Ambient},{Diffuse},{Specular},{Shininess})";
        }
    }
}
