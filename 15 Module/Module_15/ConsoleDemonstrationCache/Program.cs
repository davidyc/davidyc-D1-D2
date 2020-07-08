using Cache;
using System;
using System.Threading;

namespace ConsoleDemonstrationCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibonacci = new Fibonachi(new CustomSystemCache<int>());

            for (var i = 1; i < 20; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonachi(i));
                Thread.Sleep(100);
            }

            fibonacci = new Fibonachi(new RedisCache<int>());

            for (var i = 1; i < 20; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonachi(i));
                Thread.Sleep(100);
            }

            Console.WriteLine();

        }
    }
}
