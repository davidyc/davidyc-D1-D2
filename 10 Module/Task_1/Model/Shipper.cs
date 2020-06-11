using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Shippers")]
	public partial class Shipper
	{
		[PrimaryKey, Identity]
		public int ShipperID { get; set; }

		[Column, NotNull] 
		public string CompanyName { get; set; } 
		
		[Column, Nullable] 
		public string Phone { get; set; } 

		[Association(ThisKey = "ShipperID", OtherKey = "ShipVia", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Order> Orders { get; set; }				
	}
}
