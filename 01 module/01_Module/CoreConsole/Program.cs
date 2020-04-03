using System;
using HelloWorldClassLiblary;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input name --> ");
            string name = Console.ReadLine();
            string str = CreaterString.CreateString(name);
            Console.WriteLine(str);
        }
    }
}
