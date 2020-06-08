using System;
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindDAL;
using NorthwindDAL.Models;
using NorthwindDAL.Repositories;

namespace NorthwindUnitTest
{
    [TestClass]
    public class NorthwindUnitTest
    {
        const string stringConnection = "data source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
        const string providerName = "System.Data.SqlClient";
        OrderRepository orderRepository = new OrderRepository(stringConnection, providerName, new ObjectMapper(), new NorthwindDbConnectionFactory());

        [TestMethod]
        public void AddNew_CountBefore_CountAfter()
        {
            var countBefore = Helpers.ExcuteSelectCountOrders();          
            orderRepository.AddNew(Helpers.GetNewOrder());
            var countAfter = Helpers.ExcuteSelectCountOrders();           
            Assert.IsTrue(countBefore + 1 == countAfter);
        }
        [TestMethod]
        public void DeleteOrderByID_CountBefore_CountAfter()
        {
            var LastRowID = Helpers.ExcuteSelectMaxOrders();
            var countBefore = Helpers.ExcuteSelectCountOrders();
            orderRepository.DeleteOrderByID(LastRowID);
            var countAfter = Helpers.ExcuteSelectCountOrders();
            Assert.IsTrue(countBefore - 1 == countAfter);
        }

        [TestMethod]
        public void GetOrders_CountBefore_CountAfter()
        {
            var count = Helpers.ExcuteSelectCountOrders();
            var orders = orderRepository.GetOrders();
            Assert.IsTrue(count == orders.Count());
        }      

        [TestMethod]
        public void SetInProgress_OrderDateIsNull_OrderDateHasDate()
        {
            var id = Helpers.ExcuteSelectMaxOrdersNewStatus();
            var value = Helpers.ExcuteSelectOrderDateValue(id);
            Assert.IsTrue(value.ToString().Equals(String.Empty));
            orderRepository.SetInProgress(id);
            value = Helpers.ExcuteSelectOrderDateValue(id);
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void SetInDone_ShippedDateISNull_ShippedDateHasValue()
        {
            var id = Helpers.ExcuteSelectMaxOrdersInProgress();
            var value = Helpers.ExcuteSelectShippedDateValue(id);
            Assert.IsTrue(value.ToString().Equals(String.Empty));
            orderRepository.SetInDone(id);
            value = Helpers.ExcuteSelectShippedDateValue(id);
            Assert.IsNotNull(value);        
        }

        [TestMethod]
        public void ExcuteCustOrderHist_ALFKI_IEnumerableWithCustOrderHist()
        {
            var ec = Helpers.GetTestCustOrderHist();
            var actual = orderRepository.GetCustomerProductDetails("ALFKI");
            Assert.IsTrue(actual.IsDeepEqual(ec));
        }

        [TestMethod]
        public void ExcudebCustOrdersDetail_10248_IEnumerableWithCustOrdersDetail()
        {
            var ec = Helpers.GetTestCustOrdersDetail();
            var actual = orderRepository.GetCustomerOrderDetails(10248);
            Assert.IsTrue(actual.IsDeepEqual(ec));
        }

        [TestMethod]
        public void Update_OrderForUpdateWithID11092_OrderWIthNewValue()
        {
            orderRepository.Update(Helpers.GetOrderForUpdate("TestValue1"));
            var actual = Helpers.ExcuteSelectShipNameValue(11092);
            Assert.IsTrue(actual.Equals("TestValue1"));
            orderRepository.Update(Helpers.GetOrderForUpdate("TestValue2"));
            actual = Helpers.ExcuteSelectShipNameValue(11092);
            Assert.IsTrue(actual.Equals("TestValue2"));
        }

        [TestMethod]
        public void GetOrderById_11108_OrderWithID11108()
        {
            var ec = Helpers.GetTestOrder();
            var actual = orderRepository.GetOrderById(11108);
            Assert.IsTrue(actual.IsDeepEqual(ec));
        }



    }     
}

