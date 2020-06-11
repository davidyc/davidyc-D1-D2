using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Suppliers")]
	public partial class Supplier
	{
		[PrimaryKey, Identity]
		public int SupplierID { get; set; } 

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

		[Column, Nullable] 
		public string HomePage { get; set; } 

		[Association(ThisKey = "SupplierID", OtherKey = "SupplierID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Product> Products { get; set; }
	}

}
