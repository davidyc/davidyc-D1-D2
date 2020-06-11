using LinqToDB.Mapping;
using System;
using System.Collections.Generic;;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Customers")]
	public partial class Customer
	{
		[PrimaryKey, NotNull] 
		public string CustomerID { get; set; } 

		[Column, NotNull] 
		public string CompanyName { get; set; } 

		[Column, Nullable] 
		public string ContactName { get; set; } 

		[Column, Nullable] 
		public string ContactTitle { get; set; } 

		[Column, Nullable] 
		public string Address { get; set; } 

		[Column, Nullable] 
		public string City { get; set; } 

		[Column, Nullable] 
		public string Region { get; set; } 

		[Column, Nullable] 
		public string PostalCode { get; set; } 

		[Column, Nullable] 
		public string Country { get; set; } 

		[Column, Nullable]
		public string Phone { get; set; } 

		[Column, Nullable] 
		public string Fax { get; set; } 

		[Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Order> Orders { get; set; }

		[Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }

	
	}

}
