using System;

namespace CSharp
{
    public class Material
    {
        public const double Vacuum = 1.0;
        public const double Air = 1.00029;
        public const double Water = 1.333;
        public const double Glass = 1.52;
        public const double Diamond = 2.417;

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

        public Color Lighting(RTObject obj, PointLight light, Point position, Vector eyev, Vector normalv, bool inShadow)
        {
            Color color;
            if (Pattern != null)
            {
                color = Pattern.PatternAtObject(obj, position);
            }
            else if (obj.Parent != null)
            {
                if (obj.Parent is Group)
                {
                    // Build up colors from parent objects
                    var mat = new Material();
                    color = Color.Black;

                    var parent = obj.Parent;
                    while (parent != null)
                    {
                        if (!parent.Material.Equals(mat))
                        {
                            break;
                        }

                        parent = parent.Parent;
                    }

                    if (parent != null)
                    {
                        color = parent.Material.Lighting(parent, light, position, eyev, normalv, inShadow);
                    }
                    else
                    {
                        color = Color;
                    }
                }
                else
                {
                    color = Color;
                }
            }
            else
            {
                color = Color;
            }

            var effectiveColor = color * light.Intensity;
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
