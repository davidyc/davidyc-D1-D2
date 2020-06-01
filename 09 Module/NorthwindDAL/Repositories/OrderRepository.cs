using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using NorthwindDAL.Interfaces;
using NorthwindDAL.Models;
using NorthwindDAL.Enums;
using System.Data.SqlClient;

namespace NorthwindDAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbProviderFactory ProviderFactory;
        private readonly string ConnectionString;

        public OrderRepository(string connectionString, string provider)
        {
            ProviderFactory = DbProviderFactories.GetFactory(provider);
            ConnectionString = connectionString;
        }

        public virtual IEnumerable<Order> GetOrders()
        {
            var resultOrders = new List<Order>();

            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {   
                            resultOrders.Add(CreateOrder(reader));
                        }
                    }
                }
            }

            return resultOrders;
        }

        public IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders where OrderID = @id;" +
                        "select OrderID, dbo.[Order Details].UnitPrice, Quantity, ProductName, dbo.[Order Details].ProductID " +
                        "ProductID from dbo.[Order Details], dbo.Products where OrderID = @id" +
                        " and dbo.[Order Details].ProductID = dbo.Products.ProductID";

                    command.CommandType = CommandType.Text;

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;

                    command.Parameters.Add(paramId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return null;

                        reader.Read();

                        var order = CreateOrder(reader);
                        order.Details = new List<OrderDetail>();

                        reader.NextResult();                       

                        while (reader.Read())
                        {   
                            order.Details.Add(CreateOrderDetail(reader));
                        }
                        return order;
                    }
                }
            }
        }

        public void AddNew(Order newOrder)
        {          
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                CreateNewOrder(connection, newOrder);
                var MaxID = GetMaxID(connection);

                foreach (var item in newOrder.Details)
                {
                    CreateDetailsOrder(connection, MaxID, item);
                }
                
            }
        }            
        
        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void SetInProgress(int id)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Orders SET OrderDate = GETDATE() WHERE OrderID = {id};";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SetInDone(int id)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Orders SET ShippedDate = GETDATE() WHERE OrderID = {id};";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        private Order CreateOrder(DbDataReader reader)
        {
            var order =  new Order
            {
                OrderID = reader.GetInt32(0),
                OrderDate = reader["OrderDate"],
                ShipName = reader.GetString(2),
                ShipAddress = reader.GetString(3),
                ShippedDate = reader["OrderDate"]
            };


            order.SetStatus();
            return order;
        }     
        private OrderDetail CreateOrderDetail(DbDataReader reader)
        {
            return new OrderDetail
            {
                UnitPrice = (decimal)reader["unitPrice"],
                Quantity = reader["Quantity"],
                Product = new Product
                {
                    ProductID = (int)reader["ProductID"],
                    ProductName = (string)reader["ProductName"]
                }
            };
        }
        private void CreateNewOrder(DbConnection connection, Order newOrder)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"Insert into  Orders (ShipName, ShipAddress )" +
                $"VALUES('{newOrder.ShipName}', '{newOrder.ShipAddress}')";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
        private object GetMaxID(DbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) from Orders";
                command.CommandType = CommandType.Text;
                return command.ExecuteScalar();
            }
        }
        private void CreateDetailsOrder(DbConnection connection, object MaxID, OrderDetail item)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO [Northwind].[dbo].[Order Details] (OrderID, ProductID, UnitPrice," +
                    $" Quantity) VALUES('{MaxID}', '{item.Product.ProductID}', {item.UnitPrice}, {item.Quantity})";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }
}
