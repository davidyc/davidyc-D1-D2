using NorthwindDAL;
using NorthwindDAL.Models;
using NorthwindDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUnitTest
{
    public class Helpers
    {
        const string stringConnection = "data source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
        const string providerName = "System.Data.SqlClient";
        static OrderRepository  orderRepository = new OrderRepository(stringConnection, providerName, new MapObject()
            , new Connection());

        static Helpers()
        {
            OrderRepository orderRepository = new OrderRepository(stringConnection, providerName, new MapObject(),
                new Connection());
        }
        public static int ExcuteSelectCountOrders()
        {
            int count = 0;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(OrderID) FROM Orders";
                count = (int)command.ExecuteScalar();
            }
            connection.Close();
            return count;
        }
        public static int ExcuteSelectMaxOrders()
        {
            int count = 0;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders";
                count = (int)command.ExecuteScalar();
            }
            connection.Close();
            return count;
        }

        public static Order GetNewOrder()
        {
            return new Order()
            {
                OrderDate = new DateTime(1996, 2, 2),
                ShipName = "Name",
                ShipAddress = "Adress",
                Details = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        Product = new Product{ProductID = 11},
                        Quantity = 10,
                        UnitPrice = 14
                    },
                    new OrderDetail
                    {
                        Product = new Product{ProductID = 10},
                        Quantity = 10,
                        UnitPrice = 14
                    },
                }
            };
        }

        public static int ExcuteSelectMaxOrdersNewStatus()
        {
            int id = 0;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders WHERE OrderDate is null";
                id = (int)command.ExecuteScalar();
            }
            connection.Close();
            return id;
        }      

        public static int ExcuteSelectOrderDateByID()
        {
            int id = 0;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders WHERE OrderDate is null";
                id = (int)command.ExecuteScalar();
            }
            connection.Close();
            return id;
        }

        public static object ExcuteSelectOrderDateValue(int id)
        {
            object value = null;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT OrderDate FROM Orders WHERE OrderID = {id}";
                value = command.ExecuteScalar();
            }
            connection.Close();
            return value;
        }

        public static int ExcuteSelectMaxOrdersInProgress()
        {
            int id = 0;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders WHERE OrderDate is not null and ShippedDate is null";
                id = (int)command.ExecuteScalar();
            }
            connection.Close();
            return id;
        }     

        public static object ExcuteSelectShippedDateValue(int id)
        {
            object value = null;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT ShippedDate FROM Orders WHERE OrderID = {id}";
                value = command.ExecuteScalar();
            }
            connection.Close();
            return value;
        }

        public static IEnumerable<CustOrderHist> GetTestCustOrderHist()
        {
            return new List<CustOrderHist>
            {
                new CustOrderHist {ProductName = "Aniseed Syrup", Total=6},
                new CustOrderHist {ProductName = "Chartreuse verte", Total=21},
                new CustOrderHist {ProductName = "Escargots de Bourgogne", Total=40},
                new CustOrderHist {ProductName = "Flotemysost", Total=20},
                new CustOrderHist {ProductName = "Grandma's Boysenberry Spread", Total=16},
                new CustOrderHist {ProductName = "Lakkalikööri", Total=15},
                new CustOrderHist {ProductName = "Original Frankfurter grüne Soße", Total=2},
                new CustOrderHist {ProductName = "Raclette Courdavault", Total=15},
                new CustOrderHist {ProductName = "Rössle Sauerkraut", Total=17},
                new CustOrderHist {ProductName = "Spegesild", Total=2},
                new CustOrderHist {ProductName = "Vegie-spread", Total=20}
            };
        }

        public static IEnumerable<CustOrdersDetail> GetTestCustOrdersDetail()
        {
            return new List<CustOrdersDetail>
            {
                new CustOrdersDetail {ProductName = "Queso Cabrales", UnitPrice=10, Quantity = 10, Discount = 0, ExtendedPrice = 100},
                new CustOrdersDetail {ProductName = "Singaporean Hokkien Fried Mee", UnitPrice=10, Quantity = 10, Discount = 0, ExtendedPrice = 100},
                new CustOrdersDetail {ProductName = "Mozzarella di Giovanni", UnitPrice=10, Quantity = 10, Discount = 0, ExtendedPrice = 100}
            };
        }

        public static Order GetOrderForUpdate(string ShipName)
        {
            return new Order()
            {
                OrderID = 11092,
                OrderDate = null,
                ShipName = ShipName,
                ShipAddress = "Adress",
                Details = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        Product = new Product{ProductID = 11},
                        Quantity = 10,
                        UnitPrice = 14
                    },
                    new OrderDetail
                    {
                        Product = new Product{ProductID = 10},
                        Quantity = 10,
                        UnitPrice = 14
                    },
                }
            };
        }

        public static object ExcuteSelectShipNameValue(int id)
        {
            object value = null;
            var connection = orderRepository.connectionDB.CreateConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT ShipName FROM Orders  where OrderID = {id}";
                value = command.ExecuteScalar();
            }
            connection.Close();
            return value;
        }

        public static Order GetTestOrder()
        {
            return new Order()
            {
                OrderID = 11108,
                ShipName = "Name",
                ShipAddress = "Adress",
                Details = new List<OrderDetail>()
               
            };
        }
    }
}
