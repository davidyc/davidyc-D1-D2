using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "CustomerDemographics")]
	public partial class CustomerDemographic
	{
		[PrimaryKey, NotNull]
		public string CustomerTypeID { get; set; } 

		[Column, Nullable] 
		public string CustomerDesc { get; set; } 

		[Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }
	}
}
