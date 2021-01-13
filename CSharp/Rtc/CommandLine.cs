using System;
using System.Collections.Generic;
using System.Text;

namespace rtc
{
    public class CommandLine : InputArguments
    {
        public CommandLine(string[] args) : base(args)
        {
            // Nothing to do here
        }

        public string Input => GetValue("input0");
        public string Output => GetValue("o") ?? "Output.ppm";
        public int Width => GetIntValue("width", 400);
        public int Height => GetIntValue("height", 200);
        public string Floor => Contains("floor") ? GetValue("floor") ?? "Red" : string.Empty;

        public double RX => GetDoubleValue("rx", 0.0);
        public double RY => GetDoubleValue("ry", 0.0);
        public double RZ => GetDoubleValue("rz", 0.0);
        public bool Help => GetBoolValue("help", false);

        protected int GetIntValue(string key, int defaultValue)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                int res = 0;
                if (int.TryParse(_parsedArguments[adjustedKey], out res))
                    return res;
            }

            return defaultValue;
        }

        protected double GetDoubleValue(string key, double defaultValue)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                double res = 0;
                if (double.TryParse(_parsedArguments[adjustedKey], out res))
                    return res;
            }

            return defaultValue;
        }

        protected bool GetBoolValue(string key, bool defaultValue)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                bool res;
                if (bool.TryParse(_parsedArguments[adjustedKey], out res))
                    return res;
            }

            return defaultValue;
        }
    }
}
