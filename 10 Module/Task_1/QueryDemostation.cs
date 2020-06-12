using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Interface;
using Task_1.Model;

namespace Task_1
{
    public class QueryDemostation
    {
        public void ListProductsWithCategorySupplier(IDataConnection connection)
        {
            var productWCWS = connection.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier);
            Console.WriteLine("Products");
            foreach (var product in productWCWS)
            {
                Console.WriteLine($"Name:{product.ProductName}, Category:{product.Category.CategoryName}, Supplier:{product.Supplier.CompanyName}");
            }
        }

        public void EmployeesWithRegion(IDataConnection connection)
        {
            var employees = from e in connection.Employees
                            join et in connection.EmployeeTerritorys on e.EmployeeID equals et.EmployeeID into eet
                            from t in eet.DefaultIfEmpty()
                            join et in connection.Territorys on t.TerritoryID equals et.TerritoryID into ter
                            from te in ter.DefaultIfEmpty()
                            join re in connection.Regions on te.RegionID equals re.RegionID into reg
                            from empReg in reg.DefaultIfEmpty()
                            select new { e.LastName, empReg };

            foreach (var employee in employees)
            {
                Console.WriteLine($"Last name: {employee.LastName} Employee region: {employee.empReg.RegionDescription}");
            }
        }

        public void RegionWithCountEmployee(IDataConnection connection)
        {
            var regionsWithCountEmp = from r in connection.Regions
                                      join t in connection.Territorys on r.RegionID equals t.RegionID into rts
                                      from rt in rts.DefaultIfEmpty()
                                      join et in connection.EmployeeTerritorys on rt.TerritoryID equals et.TerritoryID into emters
                                      from emter in emters.DefaultIfEmpty()
                                      select new { Region = r.RegionDescription, EmpID = emter.EmployeeID };

            var regCount = from row in regionsWithCountEmp.Distinct()
                           group row by row.Region into g
                           select new { RegionName = g.Key, EmpCount = g.Count(e => e.EmpID != 0) };

            foreach (var item in regCount.ToList())
            {
                Console.WriteLine($"Region name: {item.RegionName} Count: {item.EmpCount}");
            }
        }

        public void EmployeesWithShippers (IDataConnection connection)
        {
            var empWithShipper = (from e in connection.Employees
                                  join o in connection.Order on e.EmployeeID equals o.EmployeeID into eos
                                  from eo in eos.DefaultIfEmpty()
                                  join s in connection.Shippers on eo.Shippers.ShipperID equals s.ShipperID into zl
                                  from z in zl.DefaultIfEmpty()
                                  select new { e.EmployeeID, z.CompanyName }).Distinct().OrderBy(t => t.EmployeeID);
            foreach (var record in empWithShipper.ToList())
            {
                Console.WriteLine($"Employee ID: {record.EmployeeID} Shipper: {record.CompanyName}");
            }

        }

        public void MoveProcuctToNewCategory(IDataConnection connection, int oldIDCategory, int newIDCategory)
        {
            var countProductBefore = connection.Products.Where(x => x.CategoryID == newIDCategory).Count();
            Console.WriteLine($"Count product with category {newIDCategory} is {countProductBefore}");

            connection.Products.Where(x => x.CategoryID == oldIDCategory).Set(x => x.CategoryID, newIDCategory).Update();

            var countProductAfter = connection.Products.Where(x => x.CategoryID == newIDCategory).Count();
            Console.WriteLine($"Count product with category {newIDCategory} is {countProductAfter}");
        }

        public void AddNewEpmloyeeWithTerretory(NorthwindDataConnection connection, Employee emp, int regionID)
        {
            try
            {
                connection.BeginTransaction();
                var newID = Convert.ToInt32(connection.InsertWithIdentity(emp));
                Console.WriteLine($"Add new employee with id {newID}");
                connection.Territorys.Where(x => x.RegionID == regionID)
                    .Insert(connection.EmployeeTerritorys,
                    t => new EmployeeTerritory { EmployeeID = newID, TerritoryID = t.TerritoryID });

                connection.CommitTransaction();
            }
            catch (Exception e)
            {
                connection.RollbackTransaction();
            }
        }

        public void AddProducts(NorthwindDataConnection connection, List<Product> products)
        {
            try
            {
                connection.BeginTransaction();
                foreach (var product in products)
                {
                    var category = connection.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                    product.CategoryID = category?.CategoryID ?? Convert.ToInt32(connection.InsertWithIdentity(
                                             new Category
                                             {
                                                 CategoryName = product.Category.CategoryName
                                             }));
                    var supplier = connection.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
                    product.SupplierID = supplier?.SupplierID ?? Convert.ToInt32(connection.InsertWithIdentity(
                                             new Supplier
                                             {
                                                 CompanyName = product.Supplier.CompanyName
                                             }));
                }

                connection.BulkCopy(products);
                connection.CommitTransaction();
            }
            catch
            {
                connection.RollbackTransaction();
            }
        }

        public void ReplaceProductInNotShippedOrder(IDataConnection connection)
        {
            var orderDetails = connection.OrderDetails.LoadWith(od => od.OrderDetailsOrder)
                                .Where(od => od.OrderDetailsOrder.ShippedDate == null).ToList();

            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"product ID = {orderDetail.ProductID} in order with id = {orderDetail.OrderID}");
                connection.OrderDetails.LoadWith(od => od.OrderDetailsProduct).Update(od => od.ProductID == orderDetail.ProductID
                && od.OrderID == orderDetail.OrderID, od => new OrderDetail
                {
                    ProductID = connection.Products.First(p => !connection.OrderDetails.Where(t => t.OrderID == od.OrderID)
                         .Any(t => t.ProductID == p.ProductID) && p.CategoryID == od.OrderDetailsProduct.CategoryID).ProductID
                });

            }
            Console.WriteLine();
            orderDetails = connection.OrderDetails.LoadWith(od => od.OrderDetailsOrder)
                           .Where(od => od.OrderDetailsOrder.ShippedDate == null).ToList();
            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"product ID = {orderDetail.ProductID} in order with id = {orderDetail.OrderID}");
            }

        }
    }
}
