using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate);

        Order GetOrderById(int id);

        Order AddNew(Order newOrder);

        Order Update(Order order);
    }
}
