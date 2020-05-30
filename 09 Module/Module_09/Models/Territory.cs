using System;
using System.Collections.Generic;

namespace Module_09.Models
{   
    public class Territory
    {
        public Territory()
        {
            this.Employees = new HashSet<Employee>();
        }

        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }

        public Region Region { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
