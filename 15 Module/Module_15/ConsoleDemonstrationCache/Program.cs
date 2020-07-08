using Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemonstrationCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new CustomSystemCache<int>();
            c.Set("key123", 1234, DateTimeOffset.Now.AddMilliseconds(300));
            Console.WriteLine(c.Get("key123"));
            Thread.Sleep(300);
            Console.WriteLine(c.Get("key123"));
        }
    }
}
