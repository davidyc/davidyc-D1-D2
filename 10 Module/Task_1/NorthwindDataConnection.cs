using LinqToDB;
using LinqToDB.Data;
using Task_1.Model;

namespace Task01
{
    public class NorthwindDataConnection : DataConnection
    {
        public NorthwindDataConnection() : base("Northwind") { }
        public ITable<Category> Categories { get { return GetTable<Category>(); } }
        public ITable<Product> Products { get { return this.GetTable<Product>(); } }
        public ITable<Supplier> Suppliers { get { return this.GetTable<Supplier>(); } }
        public ITable<OrderDetail> OrderDetails { get { return this.GetTable<OrderDetail>(); } }
        public ITable<Order> Order { get { return this.GetTable<Order>(); } }


    }
}
