using System;

namespace rtc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tuple = new CSharp.Tuple(1, 2, 3, 4);
            Console.WriteLine($"Tuple is {tuple}.");
            Console.WriteLine();
        }
    }
}
