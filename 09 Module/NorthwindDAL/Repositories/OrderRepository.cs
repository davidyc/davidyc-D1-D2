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
        public IEnumerable<CustOrderHist> ExcudeaCustOrderHist(string customerID)
        {
            var custOrderHists = new List<CustOrderHist>();
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CustOrderHist";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@CustomerID",
                        Value = customerID
                    };
                   
                    command.Parameters.Add(nameParam);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            
                            custOrderHists.Add(CreateCustOrderHist(reader));
                        }
                    }
                    return custOrderHists;
                }
            }
        }
        public IEnumerable<CustOrdersDetail> ExcudebCustOrdersDetail(int orderID)
        {
            var custOrderHists = new List<CustOrdersDetail>();
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CustOrdersDetail";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@OrderID",
                        Value = orderID
                    };

                    command.Parameters.Add(nameParam);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            custOrderHists.Add(CreateCustOrdersDetail(reader));
                        }
                    }
                    return custOrderHists;
                }
            }
        }
        public void DeleteOrderByID(int orderID)
        {
            var order = GetOrderById(orderID);
            if (order.Status != Status.Done)
                DeleteOrder(order);
        }

        private void DeleteOrder(Order order)
        {
            // DELETE FROM[Order Details] WHERE[Order Details].[OrderID] in
            //(SELECT OrderID FROM Orders WHERE Orders.CustomerID is null)
            //DELETE FROM Orders WHERE Orders.CustomerID is null

            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM [Order Details] WHERE [Order Details].[OrderID] = {order.OrderID}" +
                        $"DELETE FROM Orders WHERE Orders.OrderID =  {order.OrderID}";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        private Order CreateOrder(DbDataReader reader)
        {
            var order = new Order
            {
                OrderID = reader.GetInt32(0),
                OrderDate = GetDateTimeFrom(reader, 1),
                ShipName = reader.GetString(2),
                ShipAddress = reader.GetString(3),
                ShippedDate = GetDateTimeFrom(reader, 4)
            };
           
            order.SetStatus();
            return order;
        }     
        private Nullable<DateTime> GetDateTimeFrom(DbDataReader reader, int index)
        {
            var value = reader.GetValue(index);
            if (value.ToString().Equals(String.Empty))
                return null;
            return (DateTime)value;

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
        private CustOrderHist CreateCustOrderHist(DbDataReader reader)
        {
            return new CustOrderHist
            {
                ProductName = reader.GetString(0),
                Total = reader.GetInt32(1)
            };
        }
        private CustOrdersDetail CreateCustOrdersDetail(DbDataReader reader)
        {
            var tmp =  new CustOrdersDetail
            {                
               
            };
            tmp.ProductName = reader.GetString(0);
            tmp.UnitPrice = reader.GetDecimal(1);
            tmp.Quantity = reader["Quantity"];
            tmp.Discount = reader.GetInt32(3);
            tmp.ExtendedPrice = reader.GetDecimal(4);

            return tmp;

        }
    }
}
