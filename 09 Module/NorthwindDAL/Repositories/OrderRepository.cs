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
            using (var connection = CreateConnection()) 
            { 
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, OrderDate, ShipName, ShipAddress, ShippedDate from dbo.Orders";
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        if(reader == null)
                            return null;                        
                        while (reader.Read())
                        {
                            resultOrders.Add(MappinObject<Order>(reader, typeof(Order)));
                        }
                    }
                }
            }
            return resultOrders;
        }        
        public virtual Order GetOrderById(int id)
        {
            using (var connection = CreateConnection())
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
                        if (reader == null)
                            return null;                       
                        if (!reader.HasRows)
                            return null;
                        reader.Read();

                        var order = MappinObject<Order>(reader, typeof(Order));
                        order.Details = new List<OrderDetail>();

                        reader.NextResult();

                        while (reader.Read())
                        {
                            order.Details.Add(MappinObject<OrderDetail>(reader, typeof(OrderDetail)));
                        }
                        return order;
                    }

                }
            }

        }
        public virtual void AddNew(Order newOrder)
        {
            using (var connection = CreateConnection())
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

            using (var connection = CreateConnection())
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
            using (var connection = CreateConnection())
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
            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Orders SET ShippedDate = GETDATE() WHERE OrderID = {id};";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public virtual IEnumerable<CustOrderHist> ExcudeaCustOrderHist(string customerID)
        {
            var custOrderHists = new List<CustOrderHist>();
            using (var connection = CreateConnection())
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
                        if (reader == null)
                            return null;
                        while (reader.Read())
                        {
                            custOrderHists.Add(MappinObject<CustOrderHist>(reader, typeof(CustOrderHist)));
                        }
                    }
                }
                return custOrderHists;              
            }
        }
        public virtual IEnumerable<CustOrdersDetail> ExcudebCustOrdersDetail(int orderID)
        {
            var custOrderHists = new List<CustOrdersDetail>();
            using (var connection = CreateConnection())
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
                        if (reader == null)
                            return null;
                        while (reader.Read())
                        {
                            custOrderHists.Add(MappinObject<CustOrdersDetail>(reader, typeof(CustOrdersDetail)));
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
            using (var connection = CreateConnection())
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
        public DbConnection CreateConnection()
        {
            var connection = ProviderFactory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
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
        private T MappinObject<T>(DbDataReader reader, Type type)
        {
            var properties = type.GetProperties();
            T instance = (T)Activator.CreateInstance(type);
            for (int i = 0; i < properties.Length; i++)
            {
                var Attributes = properties[i].CustomAttributes.FirstOrDefault(x=>x.AttributeType == typeof(IgnoreMapping));
                if(Attributes != null)
                {
                    break;
                }
                var value = reader[properties[i].Name];
                if (value.ToString().Equals(String.Empty))
                    properties[i].SetValue(instance, null);
                else
                    properties[i].SetValue(instance, value);
            }
            return instance;
        } 
    }
}
