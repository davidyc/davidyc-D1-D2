using System;
using System.Collections.Generic;

namespace Module_09.Models
{   
    public class Shipper
    {
        public Shipper()
        {
            this.Orders = new HashSet<Order>();
        }
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
