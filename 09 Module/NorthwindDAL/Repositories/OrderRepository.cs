using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using NorthwindDAL.Interfaces;
using NorthwindDAL.Models;
using NorthwindDAL.Enums;
using System.Data.SqlClient;
using System.Linq;
using NorthwindDAL.Attributes;

namespace NorthwindDAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {      
        private IObjectMapper mappingObject;
        public IDbConnectionFactory connectionDB { get; set; }

        public OrderRepository(string connectionString, string provider, IObjectMapper mappingObject, IDbConnectionFactory connection)
        {
            this.connectionDB = connection;
            connection.ConnectionString = connectionString;
            connection.ProviderFactory = DbProviderFactories.GetFactory(provider);
            this.mappingObject = mappingObject;
        }
        public virtual IEnumerable<Order> GetOrders()
        {
            var resultOrders = new List<Order>();
            using (var connection = connectionDB.CreateConnection()) 
            { 
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders";
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                                         
                        while (reader != null && reader.Read())
                        {
                            resultOrders.Add(mappingObject.MapReaderToObject<Order>(reader));
                        }
                    }
                }
            }
            return resultOrders;
        }        
        public virtual Order GetOrderById(int id)
        {
            using (var connection = connectionDB.CreateConnection())
            {
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
                        if (reader == null || !reader.HasRows)
                            return null;
                        reader.Read();

                        var order = mappingObject.MapReaderToObject<Order>(reader);
                        order.Details = new List<OrderDetail>();

                        reader.NextResult();

                        while (reader.Read())
                        {
                            order.Details.Add(mappingObject.MapReaderToObject<OrderDetail>(reader));
                        }
                        return order;
                    }

                }
            }

        }
        public virtual void AddNew(Order newOrder)
        {
            using (var connection = connectionDB.CreateConnection())
            {
                var MaxID = CreateNewOrder(connection, newOrder);
                foreach (var item in newOrder.Details)
                {
                    CreateDetailsOrder(connection, MaxID, item);
                }
            }

        }                    
        public virtual Order Update(Order order)
        {
            order.SetStatus();
            if(order.Status != Status.newOrder)
                return order;

            using (var connection = connectionDB.CreateConnection())
            {
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
            }
            return order;            
        }
        public virtual void SetInProgress(int id)
        {
            using (var connection = connectionDB.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Orders SET OrderDate = GETDATE() WHERE OrderID = {id};";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public virtual void SetInDone(int id)
        {
            using (var connection = connectionDB.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Orders SET ShippedDate = GETDATE() WHERE OrderID = {id};";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public virtual IEnumerable<CustomerProductDetail> GetCustomerProductDetails(string customerID)
        {
            var customerProductDetail = new List<CustomerProductDetail>();
            using (var connection = connectionDB.CreateConnection())
            {
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
                        while (reader == null ||reader.Read())
                        {
                            customerProductDetail.Add(mappingObject.MapReaderToObject<CustomerProductDetail>(reader));
                        }
                    }
                }
                return customerProductDetail;              
            }
        }
        public virtual IEnumerable<CustomerOrdersDetail> GetCustomerOrderDetails(int orderID)
        {
            var custOrderHists = new List<CustomerOrdersDetail>();
            using (var connection = connectionDB.CreateConnection())
            {
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
                        while (reader == null || reader.Read())
                        {
                            custOrderHists.Add(mappingObject.MapReaderToObject<CustomerOrdersDetail>(reader));
                        }
                    }
                }
            }
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
            using (var connection = connectionDB.CreateConnection())
            {
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
            }
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
    }
}
