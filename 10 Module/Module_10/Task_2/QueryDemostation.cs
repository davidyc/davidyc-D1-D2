using System;
using System.Data.Entity;
using System.Linq;

namespace Task_2
{
    public class QueryDemostation
    {
        public void CustomerWithProductByCategory(NorthwindContext context, int categoryId)
        {
            var orders = context.Orders.Include(ord => ord.Order_Details.Select(p => p.Product))
                    .Include(ord => ord.Customer).Where(or => or.Order_Details.Any(o => o.Product.CategoryID == categoryId))
                    .Select(o => new
                    {
                        Name = o.Customer.ContactName,
                        Order = o.Order_Details.Select(od => new
                        {
                            ProductName = od.Product.ProductName,
                            OrderID = od.OrderID,
                        })
                    });

            foreach (var order in orders)
            {
                Console.WriteLine($"Customer name: {order.Name}");
                foreach (var item in order.Order)
                {
                    Console.WriteLine($" Product name: {item.ProductName}");
                }

            }
        }
        
    }
}
