
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Model;

namespace Task_1.Interface
{
    //не знаю нужли тут делать интерейс, но сделал
    public interface IDataConnection 
    {
        ITable<Category> Categories { get; }
        ITable<Customer> Customers { get; }
        ITable<CustomerCustomerDemo> CustomerCustomerDemos { get; }
        ITable<CustomerDemographic> CustomerDemographics { get; }
        ITable<Employee> Employees { get; }
        ITable<EmployeeTerritory> EmployeeTerritorys { get; }
        ITable<OrderDetail> OrderDetails { get; }
        ITable<Order> Order { get; }
        ITable<Product> Products { get; }
        ITable<Region> Regions { get; }
        ITable<Shipper> Shippers { get; }
        ITable<Supplier> Suppliers { get; }
        ITable<Territory> Territorys { get; }

       
    }
}
