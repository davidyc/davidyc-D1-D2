using LinqToDB.Mapping;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Order Details")]
	public partial class OrderDetail
	{
		[PrimaryKey(1), NotNull] public int OrderID { get; set; } 
		[PrimaryKey(2), NotNull] public int ProductID { get; set; } 
		[Column, NotNull] public decimal UnitPrice { get; set; }
		[Column, NotNull] public short Quantity { get; set; }
		[Column, NotNull] public float Discount { get; set; } 
		
		[Association(ThisKey = "OrderID", OtherKey = "OrderID", CanBeNull = false, KeyName = "FK_Order_Details_Orders", BackReferenceName = "OrderDetails")]
		public Order OrderDetailsOrder { get; set; }

		[Association(ThisKey = "ProductID", OtherKey = "ProductID", CanBeNull = false, KeyName = "FK_Order_Details_Products", BackReferenceName = "OrderDetails")]
		public Product OrderDetailsProduct { get; set; }


	}
}
