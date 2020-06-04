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
            var connection = CreateConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders";
                command.CommandType = CommandType.Text;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() && reader != null)
                    {   
                        resultOrders.Add(CreateOrder(reader));
                    }
                }                
            }
            connection.Close();
            return resultOrders;
        }        
        public virtual Order GetOrderById(int id)
        {
            var connection = CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                        @"select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders where OrderID = @id;
                        select OrderID, dbo.[Order Details].UnitPrice, Quantity, ProductName, dbo.[Order Details].ProductID  
                        ProductID from dbo.[Order Details], dbo.Products where OrderID = @id 
                        and dbo.[Order Details].ProductID = dbo.Products.ProductID";

                command.CommandType = CommandType.Text;
                var paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows || reader == null) 
                        return null;
                    reader.Read();

                    var order = CreateOrder(reader);
                    order.Details = new List<OrderDetail>();

                    reader.NextResult();                      

                    while(reader.Read())
                    {   
                        order.Details.Add(CreateOrderDetail(reader));
                    }
                    connection.Close();
                    return order;
                }
                
            }

        }
        public virtual void AddNew(Order newOrder)
        {
            var connection = CreateConnection();           
            var MaxID = CreateNewOrder(connection, newOrder);
            foreach (var item in newOrder.Details)
            {
                CreateDetailsOrder(connection, MaxID, item);
            }
            connection.Close();

        }                    
        public virtual Order Update(Order order)
        {
            order.SetStatus();
            if(order.Status != Status.newOrder)
                return order;

            var connection = CreateConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE Orders set [ShipName]=@ShipName,
                    [ShipAddress]=@ShipAddress  WHERE OrderID = @id";

                command.CommandType = CommandType.Text;

                var paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = order.OrderID;

                var paramShipName = command.CreateParameter();
                paramShipName.ParameterName = "@ShipName";
                paramShipName.Value = order.ShipName;

                var paramShipAddress = command.CreateParameter();
                paramShipAddress.ParameterName = "@ShipAddress";
                paramShipAddress.Value = order.ShipAddress;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramShipName);
                command.Parameters.Add(paramShipAddress);

                command.ExecuteNonQuery();
            }
            connection.Close();
            return order;            
        }
        public virtual void SetInProgress(int id)
        {
            var connection = CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Orders SET OrderDate = GETDATE() WHERE OrderID = {id};";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public virtual void SetInDone(int id)
        {
            var connection = CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Orders SET ShippedDate = GETDATE() WHERE OrderID = {id};";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public virtual IEnumerable<CustOrderHist> ExcudeaCustOrderHist(string customerID)
        {
            var custOrderHists = new List<CustOrderHist>();
            var connection = CreateConnection();

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
                connection.Close();
                return custOrderHists;              
            }
        }
        public virtual IEnumerable<CustOrdersDetail> ExcudebCustOrdersDetail(int orderID)
        {
            var custOrderHists = new List<CustOrdersDetail>();
            var connection = CreateConnection();
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
            }
            connection.Close();
            return custOrderHists;
        }
        public virtual void DeleteOrderByID(int orderID)
        {
            var order = GetOrderById(orderID);            
            if (order != null && order.Status != Status.Done)
                DeleteOrder(order);
        }
        private void DeleteOrder(Order order)
        {
            var connection = CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"DELETE FROM [Order Details] WHERE [Order Details].[OrderID] = @ID
                    DELETE FROM Orders WHERE Orders.OrderID = @ID";
                var paramId = command.CreateParameter();
                paramId.ParameterName = "@ID";
                paramId.Value = order.OrderID;                
                command.Parameters.Add(paramId);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public DbConnection CreateConnection()
        {
            var connection = ProviderFactory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
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
        private object CreateNewOrder(DbConnection connection, Order newOrder)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"Insert into  Orders (ShipName, ShipAddress )
                VALUES(@ShipName,@ShipAddress);SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";
                command.CommandType = CommandType.Text;
                var paramShipName = command.CreateParameter();
                paramShipName.ParameterName = "@ShipName";
                paramShipName.Value = newOrder.ShipName;
                var paramShipAddress = command.CreateParameter();
                paramShipAddress.ParameterName = "@ShipAddress";
                paramShipAddress.Value = newOrder.ShipAddress;
                command.Parameters.Add(paramShipName);
                command.Parameters.Add(paramShipAddress);
                return command.ExecuteScalar();
            }
        }       
        private void CreateDetailsOrder(DbConnection connection, object MaxID, OrderDetail item)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO [Northwind].[dbo].[Order Details] (OrderID, ProductID, UnitPrice,
                     Quantity) VALUES(@MaxID, @ProductID, @UnitPrice, @Quantity)";
                command.CommandType = CommandType.Text;

                var paramMaxID = command.CreateParameter();
                paramMaxID.ParameterName = "@MaxID";
                paramMaxID.Value = MaxID.ToString();

                var paramSProductIDs = command.CreateParameter();
                paramSProductIDs.ParameterName = "@ProductID";
                paramSProductIDs.Value = item.Product.ProductID;

                var paramUnitPrice = command.CreateParameter();
                paramUnitPrice.ParameterName = "@UnitPrice";
                paramUnitPrice.Value = item.UnitPrice;

                var paramQuantity = command.CreateParameter();
                paramQuantity.ParameterName = "@Quantity";
                paramQuantity.Value = item.Quantity;

                command.Parameters.Add(paramMaxID);
                command.Parameters.Add(paramSProductIDs);
                command.Parameters.Add(paramUnitPrice);
                command.Parameters.Add(paramQuantity);

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
