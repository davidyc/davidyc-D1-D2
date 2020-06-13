using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var qd = new QueryDemostation();
            using (var connection = new NorthwindContext())
            {
                qd.CustomerWithProductByCategory(connection, 3);
           
            }

            Console.Read();
        }
    }
}
