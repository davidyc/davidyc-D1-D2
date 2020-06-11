using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Task_1.Model
{

	[Table(Schema = "dbo", Name = "Employees")]
	public partial class Employee
	{
		[PrimaryKey, Identity] 
		public int EmployeeID { get; set; }

		[Column, NotNull] 
		public string LastName { get; set; } 

		[Column, NotNull]
		public string FirstName { get; set; } 

		[Column, Nullable] 
		public string Title { get; set; } 

		[Column, Nullable]
		public string TitleOfCourtesy { get; set; } 

		[Column, Nullable]
		public DateTime? BirthDate { get; set; } 

		[Column, Nullable]
		public DateTime? HireDate { get; set; } 

		[Column, Nullable]
		public string Address { get; set; } 

		[Column, Nullable]
		public string City { get; set; }

		[Column, Nullable] public string Region { get; set; } 

		[Column, Nullable]
		public string PostalCode { get; set; } 

		[Column, Nullable] 
		public string Country { get; set; }

		[Column, Nullable] 
		public string HomePhone { get; set; } 

		[Column, Nullable]
		public string Extension { get; set; } 

		[Column, Nullable] 
		public byte[] Photo { get; set; }

		[Column, Nullable] 
		public string Notes { get; set; } 

		[Column, Nullable]
		public int? ReportsTo { get; set; } 

		[Column, Nullable] 
		public string PhotoPath { get; set; } 

		[Association(ThisKey = "ReportsTo", OtherKey = "EmployeeID", CanBeNull = true, KeyName = "FK_Employees_Employees", BackReferenceName = "FK_Employees_Employees_BackReferences")]
		public Employee FK_Employees_Employee { get; set; }

		[Association(ThisKey = "EmployeeID", OtherKey = "ReportsTo", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Employee> FK_Employees_Employees_BackReferences { get; set; }

		[Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<Order> Orders { get; set; }

		[Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = true, IsBackReference = true)]
		public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }

	
	}
}
