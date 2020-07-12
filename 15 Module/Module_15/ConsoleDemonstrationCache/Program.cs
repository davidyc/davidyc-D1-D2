using Cache;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleDemonstrationCache
{
    class Program
    {
        static void Main(string[] args)
        {
            fibonacciDemonstarion();
            MemoryCache();
            //RedisCache();
           // SqlMonitors();

            Console.WriteLine();
        }

        static void fibonacciDemonstarion()
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

        }

        static void MemoryCache()
        {
            var entitiesManager = new EntitiesManager<Category>(new CustomSystemCache<IEnumerable<Category>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }

            entitiesManager = new EntitiesManager<Category>(new CustomSystemCache<IEnumerable<Category>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        static void RedisCache()
        {
            var entitiesManager = new EntitiesManager<Category>(new RedisCache<IEnumerable<Category>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }

            entitiesManager = new EntitiesManager<Category>(new RedisCache<IEnumerable<Category>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        static void SqlMonitors()
        {
            var entitiesManager = new MemoryEntitiesManager<Supplier>(new CustomSystemCache<IEnumerable<Supplier>>(),
                 @"SELECT [SupplierID]
                  ,[CompanyName]
                  ,[ContactName]
                  ,[ContactTitle]
                  ,[Address]
                  ,[City]
                  ,[Region]
                  ,[PostalCode]
                  ,[Country]
                  ,[Phone]
                  ,[Fax]
                  ,[HomePage]
              FROM [Northwind].[dbo].[Suppliers]");

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }
    }
}
