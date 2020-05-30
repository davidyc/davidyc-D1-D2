using System;
using System.Collections.Generic;

namespace NorthwindDAL
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderDetail> Details {get; set;}
    }
}