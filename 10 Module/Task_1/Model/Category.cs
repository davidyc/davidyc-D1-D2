using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Categories")]
	public partial class Category
	{
		[PrimaryKey, Identity]
		public int CategoryID { get; set; } 
		[Column, NotNull]
		public string CategoryName { get; set; } 
		[Column, Nullable]
		public string Description { get; set; }
		[Column, Nullable]
		public byte[] Picture { get; set; } 

	    [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Product> Products { get; set; }

	}
}
