using LinqToDB.Mapping;


namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "CustomerCustomerDemo")]
	public partial class CustomerCustomerDemo
	{
		[PrimaryKey(1), NotNull] 
		public string CustomerID { get; set; } 

		[PrimaryKey(2), NotNull]
		public string CustomerTypeID { get; set; } 

		[Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = false, KeyName = "FK_CustomerCustomerDemo", BackReferenceName = "CustomerCustomerDemoes")]
		public CustomerDemographic FK_CustomerCustomerDemo { get; set; }

		[Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = false, KeyName = "FK_CustomerCustomerDemo_Customers", BackReferenceName = "CustomerCustomerDemoes")]
		public Customer Customer { get; set; }

	
	}
}
