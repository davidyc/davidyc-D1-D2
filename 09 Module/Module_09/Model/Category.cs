using System;
using System.Collections.Generic;

namespace Module_09.Model
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}