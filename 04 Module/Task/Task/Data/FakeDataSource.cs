using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task.Data
{	public class FakeDataSource : IDataSource
    {
		private List<Product> productList;
		private List<Customer> customerList;
		private List<Supplier> supplierList;

		public string Resource { get; set; }

		public List<Product> Products
		{
			get
			{
				if (productList == null)
					createListsProducts();

				return productList;
			}
			set
			{
				productList = value;
			}
		}

		public List<Supplier> Suppliers
		{
			get
			{
				if (supplierList == null)
					createListsSuppliers();

				return supplierList;
			}
			set
			{
				supplierList = value;
			}
		}

		public List<Customer> Customers
		{
			get
			{
				if (customerList == null)
					createListsCustomers();

				return customerList;
			}
			set
			{
				customerList = value;
			}
		}

		private void createListsProducts()
		{
			productList =
				new List<Product> {
					new Product { ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 39 },
					new Product { ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.0000M, UnitsInStock = 17 },
					new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.0000M, UnitsInStock = 13 },
					new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.0000M, UnitsInStock = 53 },
					new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.3500M, UnitsInStock = 0 },
					new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", UnitPrice = 25.0000M, UnitsInStock = 120 },
					new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", UnitPrice = 30.0000M, UnitsInStock = 15 },
					new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", UnitPrice = 40.0000M, UnitsInStock = 6 },
					new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", UnitPrice = 97.0000M, UnitsInStock = 29 },
					new Product { ProductID = 10, ProductName = "Ikura", Category = "Seafood", UnitPrice = 31.0000M, UnitsInStock = 31 },
					new Product { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", UnitPrice = 21.0000M, UnitsInStock = 22 },
					new Product { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", UnitPrice = 38.0000M, UnitsInStock = 86 },
					new Product { ProductID = 13, ProductName = "Konbu", Category = "Seafood", UnitPrice = 6.0000M, UnitsInStock = 24 },
					new Product { ProductID = 14, ProductName = "Tofu", Category = "Produce", UnitPrice = 23.2500M, UnitsInStock = 35 },
					new Product { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", UnitPrice = 15.5000M, UnitsInStock = 39 },
				};
		}		
		private void createListsSuppliers()
		{
			supplierList = new List<Supplier>(){
					new Supplier {SupplierName = "Exotic Liquids", Address = "49 Gilbert St.", City = "London", Country = "UK"},
					new Supplier {SupplierName = "New Orleans Cajun Delights", Address = "P.O. Box 78934", City = "New Orleans", Country = "USA"},
					new Supplier {SupplierName = "Grandma Kelly's Homestead", Address = "707 Oxford Rd.", City = "Ann Arbor", Country = "USA"},
					new Supplier {SupplierName = "Tokyo Traders", Address = "9-8 Sekimai Musashino-shi", City = "Tokyo", Country = "Japan"},
					new Supplier {SupplierName = "Cooperativa de Quesos 'Las Cabras'", Address = "Calle del Rosal 4", City = "Oviedo", Country = "Spain"},
					new Supplier {SupplierName = "Mayumi's", Address = "92 Setsuko Chuo-ku", City = "Osaka", Country = "Japan"},
					new Supplier {SupplierName = "Pavlova, Ltd.", Address = "74 Rose St. Moonie Ponds", City = "Melbourne", Country = "Australia"},
					new Supplier {SupplierName = "Specialty Biscuits, Ltd.", Address = "29 King's Way", City = "Manchester", Country = "UK"},
					new Supplier {SupplierName = "PB Knäckebröd AB", Address = "Kaloadagatan 13", City = "Göteborg", Country = "Sweden"},
					new Supplier {SupplierName = "Refrescos Americanas LTDA", Address = "Av. das Americanas 12.890", City = "Sao Paulo", Country = "Brazil"},
					new Supplier {SupplierName = "Heli Süßwaren GmbH & Co. KG", Address = "Tiergartenstraße 5", City = "Berlin", Country = "Germany"},
					new Supplier {SupplierName = "Plutzer Lebensmittelgroßmärkte AG", Address = "Bogenallee 51", City = "Frankfurt", Country = "Germany"},
					new Supplier {SupplierName = "Nord-Ost-Fisch Handelsgesellschaft mbH", Address = "Frahmredder 112a", City = "Cuxhaven", Country = "Germany"},
					new Supplier {SupplierName = "Formaggi Fortini s.r.l.", Address = "Viale Dante, 75", City = "Ravenna", Country = "Italy"},
					new Supplier {SupplierName = "Norske Meierier", Address = "Hatlevegen 5", City = "Sandvika", Country = "Norway"},
					new Supplier {SupplierName = "Bigfoot Breweries", Address = "3400 - 8th Avenue Suite 210", City = "Bend", Country = "USA"},
					new Supplier {SupplierName = "Svensk Sjöföda AB", Address = "Brovallavägen 231", City = "Stockholm", Country = "Sweden"},
					new Supplier { SupplierName = "Norske Meierier", Address = "Hatlevegen 5", City = "Sandvika", Country = "Norway" },
			};
		}
		private void createListsCustomers()
		{
			customerList = (
				from e in XDocument.Parse(Resource).
						  Root.Elements("customer")
				select new Customer
				{
					CustomerID = (string)e.Element("id"),
					CompanyName = (string)e.Element("name"),
					Address = (string)e.Element("address"),
					City = (string)e.Element("city"),
					Region = (string)e.Element("region"),
					PostalCode = (string)e.Element("postalcode"),
					Country = (string)e.Element("country"),
					Phone = (string)e.Element("phone"),
					Fax = (string)e.Element("fax"),
					Orders = (
						from o in e.Elements("orders").Elements("order")
						select new Order
						{
							OrderID = (int)o.Element("id"),
							OrderDate = (DateTime)o.Element("orderdate"),
							Total = (decimal)o.Element("total")
						})
						.ToArray()
				})
				.ToList();
		}
	}
}
