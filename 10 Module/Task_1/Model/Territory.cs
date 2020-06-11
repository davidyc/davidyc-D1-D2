using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "Territories")]
	public partial class Territory
	{
		[PrimaryKey, NotNull] 
		public string TerritoryID { get; set; } 

		[Column, NotNull] 
		public string TerritoryDescription { get; set; } 

		[Column, NotNull] 
		public int RegionID { get; set; } 

		[Association(ThisKey = "RegionID", OtherKey = "RegionID", CanBeNull = false, KeyName = "FK_Territories_Region", BackReferenceName = "Territories")]
		public Region Region { get; set; }
	

		[Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
	}
}
