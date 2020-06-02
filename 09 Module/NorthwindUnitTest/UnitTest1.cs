using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindDAL.Models;
using NorthwindDAL.Repositories;

namespace NorthwindUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        const string stringConnection = "data source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
        const string providerName = "System.Data.SqlClient";
        OrderRepository orderRepository = new OrderRepository(stringConnection, providerName);

        [TestMethod]
        public void GetOrders()
        {            
            var orders = orderRepository.GetOrders();
            Assert.IsTrue(1==1);
        }

        [TestMethod]
        public void GetOrderById()
        {
            var orders = orderRepository.GetOrderById(10248);
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void AddNew()
        {
            orderRepository.AddNew(new Order()
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
            });
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void SetInProgress()
        {
            orderRepository.SetInProgress(11089);
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void SetInDone()
        {
            orderRepository.SetInDone(11089);
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void ExcudeaCustOrderHist()
        {
             var x = orderRepository.ExcudeaCustOrderHist("ALFKI");
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void ExcudebCustOrdersDetail()
        {
            var x = orderRepository.ExcudebCustOrdersDetail(10248);
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void DeleteOrderByID()
        {
            orderRepository.DeleteOrderByID(11077);
            Assert.IsTrue(1 == 1);
        }
    }
}

