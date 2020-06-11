using System;
using Task01;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new NorthwindDataConnection())
            {
                foreach (var item in connection.Categories)
                {
                    Console.WriteLine(item.CategoryName);
                    var prods = item.Products;
                    foreach (var prod in prods)
                    {
                        Console.WriteLine(prod);
                    }
                }
            }
            Console.Read();
        }
    }
}
