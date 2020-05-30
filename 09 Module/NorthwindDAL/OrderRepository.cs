using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace NorthwindDAL
{
    internal static class SQLQueries
    {
        public const string GetOrders = "";
    }

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
                    command.CommandText = "select OrderID, OrderDate from dbo.Orders";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order();
                            order.OrderID = reader.GetInt32(0);
                            order.OrderDate = reader.GetDateTime(1);

                            resultOrders.Add(order);
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
                        "select OrderID, OrderDate from dbo.Orders where OrderID = @id; " +
                        "select * from dbo.[Order Details] where OrderID = @id";
                    command.CommandType = CommandType.Text;

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;

                    command.Parameters.Add(paramId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return null;

                        reader.Read();

                        var order = new Order();
                        order.OrderID = reader.GetInt32(0);
                        order.OrderDate = reader.GetDateTime(1);

                        reader.NextResult();
                        order.Details = new List<OrderDetail>();

                        while (reader.Read())
                        {
                            var detail = new OrderDetail();
                            detail.UnitPrice = (decimal)reader["unitPrice"];
                            detail.Quantity = (int)reader["Quantity"];

                            order.Details.Add(detail);
                        }

                        return order;
                    }
                }
            }
        }

        public Order AddNew(Order newOrder)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
