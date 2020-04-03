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

            var createrString = new HelloCreaterString(new SystemTime());
            string str = createrString.HelloCreateString(name);
            Console.WriteLine(str);
        }
    }
}
