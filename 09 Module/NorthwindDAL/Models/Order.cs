using NorthwindDAL.Attributes;
using NorthwindDAL.Enums;
using System;
using System.Collections.Generic;

namespace NorthwindDAL.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public Nullable<DateTime> OrderDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        [IgnoreMapping]
        public Status Status { get; set; }
        public Nullable<DateTime> ShippedDate { get; set; }
        [IgnoreMapping]
        public List<OrderDetail> Details { get; set; }

        public void SetStatus()
        {
            if(OrderDate == null)
            {
                Status = Status.newOrder;
            }
            else
            {
                if (ShippedDate != null)
                    Status = Status.Done;
                else
                    Status = Status.InProgress;  
            }
        }
    }
}