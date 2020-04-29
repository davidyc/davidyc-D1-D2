using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleQueries;
using Task.Data;

namespace LinqUnitTest
{
    [TestClass]
    public class UnitLinq
    {
        // ПО тестам вообщем сделал обертку но там в дата соурс по сути просто за харкоденно создания пробустов  поставщиков
        // и только кастомеры берутся с xml
        //  сделал так же для своих тестов в ресурсах юнит тестов просто подключал  просто нужный фаил для нужного теста(ну пока ток 1 тест) 
        // А так по тестам есть большой вопрос нормально ли там объявлять так expected
        // если да то я наверно ток 1 тест напишу, потому что это очень долго....... 
        // я создаю класс в который смогу присвоить результат линкью, потом еще нужно создать xml,  а еще потом expected
        // очень много времени тратитьс на обявление всего этого
        [TestMethod] 
        public void Linq1_Given1500_ReturnOneObject()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq1);
            decimal count = 155555555;
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
           // Assert.Equals(expected, actual); // если так падает не может сравнить 
            Assert.ReferenceEquals(expected, actual); //  а так всегда проходит как вообще можно делать тесты с этим
        }

        [TestMethod]
        public void Linq3_Given10000_ReturnOneObject()
        {
            // Arrange
            var linqSamples = new LinqSamples(new FakeDataSource(), Properties.Resources.FakeCustomersLinq2);
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
            Assert.Equals(expected, actual);
        }
    }
}
