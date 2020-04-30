﻿using System;
using System.Collections.Generic;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleQueries;
using Task.Data;

namespace LinqUnitTest
{
    [TestClass]
    public class UnitLinq
    {
       //Сделал 4 теста так как это сложно и долго создавать объекты которые я жду, ну и по именно тестов опять проблемы
        [TestMethod] 
        public void Linq1_Given1500_DeepEqualTrue()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq1);
            decimal count = 1500;
            IEnumerable<CustomerTotalOrderSum> expected = new List<CustomerTotalOrderSum>()
            {
                new CustomerTotalOrderSum()
                {
                    Customer = new Customer()
                    {
                        CustomerID = "ALFKI",
                        CompanyName = "Alfreds Futterkiste",
                        Address = "Obere Str. 57",
                        City = "Berlin",
                        Region = null,
                        PostalCode = "12209",
                        Country = "Germany",
                        Phone = "030-0074321",
                        Fax = "030-0076545",
                        Orders = new Order[]
                        {
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 800
                            },
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 800
                            }
                        }
                    },
                    TotalSum = 1600
                }
            };
            // Act
            var actual = linqSamples.Linq1(count);            

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }       

        [TestMethod]
        public void Linq3_Given10000_DeepEqualTrue()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq3);
            int count = 8700;
            IEnumerable<Customer> expected = new List<Customer>()
            {
                new Customer()
                    {
                        CustomerID = "ALFKI",
                        CompanyName = "Alfreds Futterkiste",
                        Address = "Obere Str. 57",
                        City = "Berlin",
                        Region = null,
                        PostalCode = "12209",
                        Country = "Germany",
                        Phone = "030-0074321",
                        Fax = "030-0076545",
                        Orders = new Order[]
                        {
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 8800
                            },
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 800
                            }
                        }
                    }
            };
        
            // Act
            var actual = linqSamples.Linq3(count);

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }

        [TestMethod]
        public void Linq4_CustormersData_DeepEqualTrue()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq4);
            var expected = new List<FirstOrder>()
            {
                new FirstOrder()
                {
                    CustomerID = "ALFKI",
                    Date = new DateTime(1997,08,25,0,0,0)
                },
                new FirstOrder()
                {
                    CustomerID ="ANATR",
                    Date = new DateTime(1996,09,18,0,0,0)
                }
            };

            // Act
            var actual = linqSamples.Linq4();

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }

        [TestMethod]
        public void Linq6_CustormersData_DeepEqualTrue()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq6);
            IEnumerable<Customer> expected = new List<Customer>()
            {
                new Customer()
                {
                        CustomerID = "ALFKI",
                        CompanyName = "Alfreds Futterkiste",
                        Address = "Obere Str. 57",
                        City = "Berlin",
                        Region = "BC",
                        PostalCode = "12209",
                        Country = "Germany",
                        Phone = "030-0074321",
                        Fax = "030-0076545",
                        Orders = new Order[]
                        {
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 800
                            },
                            new Order()
                            {
                               OrderID = 10643,
                               OrderDate = new DateTime(1997,08,25,00,00,00),
                               Total = 800
                            }
                        }
                }
            };
            // Act
            var actual = linqSamples.Linq6();

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }
    }
}
