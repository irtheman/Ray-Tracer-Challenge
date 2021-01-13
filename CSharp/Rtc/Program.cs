using System;
using CSharp;

namespace rtc
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine cl = new CommandLine(args);
            if ((cl.Count == 0) || cl.Help || (string.IsNullOrWhiteSpace(cl.Input)))
            {
                Console.WriteLine("rtc <yaml file> [-o <output.ppm>]");
                Console.WriteLine("rtc <obj file> [-o <output.ppm>] [-width <400>] [-height <200>] [-floor <color>] [-rx <0>] [-ry <0>] [-rz <0>]");
                return;
            }

            SceneBuilder scene = null;
            if (cl.Input.EndsWith(".yml") || cl.Input.EndsWith(".yaml"))
            {
                scene = ParseYamlFile(cl);
            }
            else if (cl.Input.EndsWith(".obj"))
            {
                scene = ParseObjFile(cl);
            }
            else
            {
                Console.WriteLine("Input file not recognized.");
            }

            string output = cl.Output;

            if ((scene != null) && scene.Build())
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

            Console.ReadLine();
        }

        private static SceneBuilder ParseObjFile(CommandLine cl)
        {
            return new SceneBuilder(cl.Input, cl.Width, cl.Height, cl.Floor, cl.RX, cl.RY, cl.RZ);
        }

        private static SceneBuilder ParseYamlFile(CommandLine cl)
        {
            string file = cl.Input;

            var parser = new YamlParser();
            if (parser.Parse(file))
            {
                Console.WriteLine("Parsed!");

                return new SceneBuilder(parser.Root);
            }
            else
            {
                Console.WriteLine("Oh no!");
            }

            return null;
        }
    }
}
