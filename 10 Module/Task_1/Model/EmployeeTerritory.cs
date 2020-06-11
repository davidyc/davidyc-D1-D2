using LinqToDB.Mapping;

namespace Task_1.Model
{
	[Table(Schema = "dbo", Name = "EmployeeTerritories")]
	public partial class EmployeeTerritory
	{
		[PrimaryKey(1), NotNull]
		public int EmployeeID { get; set; }

		[PrimaryKey(2), NotNull] 
		public string TerritoryID { get; set; }
		
		[Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = false, KeyName = "FK_EmployeeTerritories_Employees", BackReferenceName = "EmployeeTerritories")]
		public Employee Employee { get; set; }

		[Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = false, KeyName = "FK_EmployeeTerritories_Territories", BackReferenceName = "EmployeeTerritories")]
		public Territory Territory { get; set; }
	}
}
