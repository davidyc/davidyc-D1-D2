using Cache;
using System;

namespace ConsoleDemonstrationCache
{
    class Program
    {
        static void Main(string[] args)
        {
            //var c = new CustomSystemCache<int>();
            //c.Set("key123", 1234, DateTimeOffset.Now.AddMilliseconds(300));
            //Console.WriteLine(c.Get("key123"));
            //Thread.Sleep(300);
            //Console.WriteLine(c.Get("key123"));
            var cc = new RedisCache<int>();
            cc.Set("key123", 1234, DateTimeOffset.Now.AddMilliseconds(300));
            Console.WriteLine(cc.Get("key123"));
            Console.WriteLine();

        }
    }
}
