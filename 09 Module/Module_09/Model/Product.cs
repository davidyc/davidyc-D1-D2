using System;
using System.Collections.Generic;

namespace Module_09.Model
{  
    public class Product
    {
        public Product()
        {
            this.Order_Details = new HashSet<Order_Detail>();
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public Category Category { get; set; }
        public ICollection<Order_Detail> Order_Details { get; set; }
        public Supplier Supplier { get; set; }
    }
}
