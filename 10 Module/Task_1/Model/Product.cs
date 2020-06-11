using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Products")]
	public partial class Product
	{
		[PrimaryKey, Identity]
		public int ProductID { get; set; } 

		[Column, NotNull] 
		public string ProductName { get; set; } 

		[Column, Nullable] 
		public int? SupplierID { get; set; } 

		[Column, Nullable] 
		public int? CategoryID { get; set; } 

		[Column, Nullable] 
		public string QuantityPerUnit { get; set; } 
		[Column, Nullable] 
		public decimal? UnitPrice { get; set; } 

		[Column, Nullable]
		public short? UnitsInStock { get; set; } 

		[Column, Nullable]
		public short? UnitsOnOrder { get; set; }

		[Column, Nullable] 
		public short? ReorderLevel { get; set; } 

		[Column, NotNull] 
		public bool Discontinued { get; set; } 	
		
		[Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true, KeyName = "FK_Products_Categories", BackReferenceName = "Products")]
		public Category Category { get; set; }

		[Association(ThisKey = "SupplierID", OtherKey = "SupplierID", CanBeNull = true, KeyName = "FK_Products_Suppliers", BackReferenceName = "Products")]
		public Supplier Supplier { get; set; }
		
		[Association(ThisKey = "ProductID", OtherKey = "ProductID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<OrderDetail> OrderDetails { get; set; }		
	}
}
