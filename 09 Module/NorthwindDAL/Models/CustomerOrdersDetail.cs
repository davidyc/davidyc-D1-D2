using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL.Models
{
    public class CustomerOrdersDetail
    {       
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public object Quantity { get; set; }     
        public float Discount { get; set; }
        public decimal ExtendedPrice { get; set; }

    }
}
