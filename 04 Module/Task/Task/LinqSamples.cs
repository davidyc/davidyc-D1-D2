// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{
		// так в целом как писать юнит тесты, для таких запросов это кейсов? мокать истоточных данных
		// но тогда разве это не тоже самое что на реалном гонять 
		
		private DataSource dataSource = new DataSource();

		[Category("Module 4")]
		[Title("Task 1")]
		[Description("Give a list of all customers whose total turnover (the sum of all orders) exceeds a certain value of X. Demonstrate the execution of the request with different X (think about whether you can do without copying the request several times)")]
		public void Linq1()
		{
			//подумал как можно сделать без многих запросов хз ваще. Я про это (think about whether you can do without copying the request several times)
			var count = 15000;

			var clients = dataSource.Customers.Where(x => x.Orders.Sum(o => o.Total) > count)
				.Select(c=> 
				new { 
					CustemID = c.CustomerID, 
					TotalSum = c.Orders.Sum(o=> o.Total) 
				});

			foreach (var item in clients)
			{
				Console.WriteLine($" id = {item.CustemID} sum = {item.TotalSum}");
			}
		}

		[Category("Module 4")]
		[Title("Task 2")]
		[Description("For each client, make a list of suppliers located in the same country and the same city. Do tasks with and without grouping.")]
		public void Linq2()
		{
			// Do tasks with and without grouping. тут еще подумаю
			var suppliersForCustomerGroup =
				dataSource.Customers
				.GroupBy(cust => dataSource.Suppliers
				.Where(supl => supl.Country == cust.Country && supl.City == cust.City));

			foreach (var group in suppliersForCustomerGroup)
			{
				foreach (var cust in group)
				{
					Console.WriteLine(  $"Company name: {cust.CompanyName} country: {cust.Country} city: {cust.City}");
					foreach (var supp in group.Key)
					{
						Console.WriteLine($"  Supplier name: {supp.SupplierName} country: {supp.Country} city: {supp.City}");
					}
				}
			}
		}

		[Category("Module 4")]
		[Title("Task 3")]
		[Description("Find all customers who have orders exceeding the total value of X")]
		public void Linq3()
		{
			var orderSum = 10000;
			var clients = dataSource.Customers.Where(cust => cust.Orders.Any(order => order.Total > orderSum));

			foreach (var client in clients)
			{
				Console.WriteLine($"Customer ID = {client.CustomerID}");
			}			
		}

		[Category("Module 4")]
		[Title("Task 4")]
		[Description("Issue a list of customers indicating the month from which year they became customers (accept the month and year of the very first order as such)")]
		public void Linq4()
		{

			var customers = dataSource.Customers.Where(c => c.Orders.Any())
				.Select(cust => new
				{
					Customer = cust.CustomerID,
					Date = cust.Orders.Min(order=>order.OrderDate)
				});
			
			foreach (var customer in customers)
			{
				Console.WriteLine($"Customer ID: {customer.Customer } Date: {customer.Date}");
			}
		}

		[Category("Module 4")]
		[Title("Task 5")]
		[Description("Do the previous task, but issue a list sorted by year, month, customer turnover (maximum to minimum) and customer name")]
		public void Linq5()
		{
			var customers = dataSource.Customers.Where(cust => cust.Orders.Any())
				.Select(cust => new
				{
					Customer = cust.CustomerID,
					Order = cust.Orders.Min(order => order.OrderDate),
					Total = cust.Orders.Sum(order => order.Total)
				}).OrderByDescending(time => time.Order.Year)
				.ThenByDescending(time => time.Order.Month)
				.ThenByDescending(time => time.Total)
				.ThenByDescending(time => time.Customer);

			foreach (var item in customers)
			{
				Console.WriteLine($"Customer ID = {item.Customer} Order = {item.Order} Sum = {item.Total}");
			}
		}

		[Category("Module 4")]
		[Title("Task 6")]
		[Description(" Indicate all customers who have a non-digital postal code or a region is missing or the operator’s code is not indicated on the phone (for simplicity we consider this to be equivalent to “no parentheses at the beginning”).")]
		public void Linq6()
		{
			var custoners = dataSource.Customers.Where(
					cust => cust.PostalCode != null && cust.PostalCode.Any(symbol => symbol < '0' || symbol > '9')
				   || string.IsNullOrWhiteSpace(cust.Region)
				   || cust.Phone.FirstOrDefault() != '('
				);

			foreach (var custoner in custoners)
			{
				Console.WriteLine($"Custom ID = {custoner.CustomerID}");
			}
		}

		[Category("Module 4")]
		[Title("Task 7")]
		[Description("Group all products by categories, inside - by stock status, inside the last group sort by cost")]
		public void Linq7()
		{
			// можно ли использовать в линкью g -> group  в люмдах 
			var products = dataSource.Products.GroupBy(g => g.Category)
				.Select(c =>
			  new
			  {
				  Category = c.Key,
				  CountUnit = c.GroupBy(u => u.UnitsInStock > 0)
					.Select(u =>
				new
				{
					UnitInStock = u.Key,
					Price = u.OrderBy(p => p.UnitPrice)
				})
			 });
				

			foreach (var product in products)
			{
				Console.WriteLine($"Category = {product.Category}");
				foreach (var count in product.CountUnit)
				{
					Console.WriteLine($"  Has unit {count.UnitInStock}");
					foreach (var price in count.Price)
					{
						Console.WriteLine($"    Product name = {price.ProductName}  price = {price.UnitPrice}");
					}
				}
			}
		}

		[Category("Module 4")]
		[Title("Task 8")]
		[Description("Group the products in the groups “cheap”, “average price”, “expensive”. Set the boundaries of each group yourself.")]
		public void Linq8()
		{
			decimal cheapPrice = 10;
			decimal budgetPrice = 50;

			var productCategories = dataSource.Products
				.GroupBy(prod => prod.UnitPrice < cheapPrice ? "Cheap Price"
				: prod.UnitPrice < budgetPrice ? "Budget Price" : "Expensive Price");

			foreach (var productCategory in productCategories)
			{
				Console.WriteLine($"Category =  {productCategory.Key}");
				foreach (var product in productCategory)
				{
					Console.WriteLine($"  Product name = {product.ProductName}  Price = {product.UnitPrice}");
				}
			}
		}

		[Category("Module 4")]
		[Title("Task 9")]
		[Description("Calculate the average profitability of each city (average order amount for all customers from a given city) and average intensity (average number of orders per client from each city)")]
		public void Linq9()
		{
			var orders = dataSource.Customers.GroupBy(cust => cust.City)
				.Select(city =>
				   new {
					   City = city.Key,
					   Avarage = city.Average(a => a.Orders.Sum(s => s.Total)),
					   AvarageCount = city.Average(a => a.Orders.Length)
				   });

			foreach (var order in orders)
			{
				Console.WriteLine($"City = {order.City}  Avarage count = {order.AvarageCount}  avarage = {order.Avarage}");
			}
		}

		[Category("Module 4")]
		[Title("Task 10")]
		[Description("Make the average annual statistics of customer activity by month (excluding the year), statistics by year, by year and month (that is, when one month in different years has its own value).")]
		public void Linq10()
		{
			var avarageStatics = dataSource.Customers
			   .Select(c => new
			   {
				   CustomerID = c.CustomerID,
				   MonthsStat = c.Orders.GroupBy(o => o.OrderDate.Month)
									   .Select(g => new { Month = g.Key, OrdersCount = g.Count() }),
				   YearsStat = c.Orders.GroupBy(o => o.OrderDate.Year)
									   .Select(g => new { Year = g.Key, OrdersCount = g.Count() }),
				   YearMonthStat = c.Orders
									   .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
									   .Select(g => new { g.Key.Year, g.Key.Month, OrdersCount = g.Count() })
			   });

			foreach (var avarageStatic in avarageStatics)
			{
				Console.WriteLine($"ID = {avarageStatic.CustomerID}");
				Console.WriteLine("Statistic mounth");
				foreach (var item in avarageStatic.MonthsStat)
				{
					Console.WriteLine($"Mounth: {item.Month} count: {item.OrdersCount}");
				}
			}
		}
	}
}
