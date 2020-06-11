﻿using System;
using Task01;

namespace Module_10
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
                }
            }
        }
    }
}
