using System;
using CSharp;

namespace rtc
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = string.Empty;
            string output = "Output.ppm";

            if (args.Length == 0)
            {
                Console.WriteLine("rtc <yaml file> <output file>");
                return;
            }
            else if (args.Length > 0)
            {
                file = args[0];
                if (args.Length == 2)
                {
                    output = args[1];
                }
                else
                {
                    Console.WriteLine("To many paramters...");
                    Console.WriteLine("rtc <yaml file> <output file>");
                    return;
                }
            }

            var parser = new YamlParser();
            if (parser.Parse(file))
            {
                Console.WriteLine("Parsed!");

                var scene = new SceneBuilder(parser.Root);
                if (scene.Build())
                {
                    Console.WriteLine("Scene Built!");

                    if (scene.Render(output))
                    {
                        Console.WriteLine("Scene Rendered!");
                        Console.WriteLine($"See \"{output}\"");
                    }
                    else
                    {
                        Console.WriteLine("Scene NOT Rendered!");
                    }
                }
                else
                {
                    Console.WriteLine("Where is the scene?");
                }
            }
            else
            {
                Console.WriteLine("Oh no!");
            }

            Console.ReadLine();
        }
    }
}
