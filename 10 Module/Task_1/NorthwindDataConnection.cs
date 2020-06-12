using LinqToDB;
using LinqToDB.Data;
using Task_1.Interface;
using Task_1.Model;

namespace Task_1
{
    public class NorthwindDataConnection : DataConnection, IDataConnection
    {
        public NorthwindDataConnection() : base("Northwind") { }
        public ITable<Category> Categories { get { return GetTable<Category>(); } }
        public ITable<Customer> Customers { get { return this.GetTable<Customer>(); } }
        public ITable<CustomerCustomerDemo> CustomerCustomerDemos { get { return this.GetTable<CustomerCustomerDemo>(); } }
        public ITable<CustomerDemographic> CustomerDemographics { get { return this.GetTable<CustomerDemographic>(); } }
        public ITable<Employee> Employees { get { return GetTable<Employee>(); } }
        public ITable<EmployeeTerritory> EmployeeTerritorys { get { return this.GetTable<EmployeeTerritory>(); } }
        public ITable<OrderDetail> OrderDetails { get { return this.GetTable<OrderDetail>(); } }
        public ITable<Order> Order { get { return this.GetTable<Order>(); } }
        public ITable<Product> Products { get { return this.GetTable<Product>(); } }
        public ITable<Region> Regions { get { return this.GetTable<Region>(); } }
        public ITable<Shipper> Shippers { get { return this.GetTable<Shipper>(); } }
        public ITable<Supplier> Suppliers { get { return this.GetTable<Supplier>(); } }
        public ITable<Territory> Territorys { get { return this.GetTable<Territory>(); } }
    }
}
