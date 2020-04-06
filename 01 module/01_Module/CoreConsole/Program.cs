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
            var datetime = DateTime.Now;

            string str = HelloCreaterString.HelloCreateString(name, datetime);
            Console.WriteLine(str);
        }
    }
}
