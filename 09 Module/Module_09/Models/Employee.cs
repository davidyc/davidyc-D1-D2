﻿using System;
using System.Collections.Generic;

namespace Module_09.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Employees1 = new HashSet<Employee>();
            this.Orders = new HashSet<Order>();
            this.Territories = new HashSet<Territory>();
        }

        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ReportsTo { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<Employee> Employees1 { get; set; }
        public Employee Employee1 { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Territory> Territories { get; set; }
    }
}
