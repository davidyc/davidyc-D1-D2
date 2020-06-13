using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Task_1.Model;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var qd = new QueryDemostation();
            using (var connection = new NorthwindDataConnection())
            {
                // qd.ListProductsWithCategorySupplier(connection);
                // qd.EmployeesWithRegion(connection);
                // qd.RegionWithCountEmployee(connection);
                // qd.EmployeesWithShippers(connection);
                // qd.MoveProcuctToNewCategory(connection, 1, 2);
                // qd.AddNewEpmloyeeWithTerretory(connection, new Employee { FirstName = "Sergey", LastName = "Davydov" }, 1);
                //qd.AddProducts(connection, new List<Product>
                //{
                //new Product
                //{
                //    ProductName = "Pipelace",
                //    Category = new Category {CategoryName = "Vehicles"},
                //    Supplier = new Supplier {CompanyName = "SD industries"}
                //},
                //new Product
                //{
                //    ProductName = "Pipelace with Gravicapa",
                //    Category = new Category {CategoryName = "Vehicles"},
                //    Supplier = new Supplier {CompanyName = "SD industries"}
                //}
                //});
                // qd.ReplaceProductInNotShippedOrder(connection);


                    Console.Read();
            }
           
        }
    }
}
