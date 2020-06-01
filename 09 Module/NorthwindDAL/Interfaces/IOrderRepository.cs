using NorthwindDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate);
        Order GetOrderById(int id);
        void AddNew(Order newOrder);
        Order Update(Order order);
        void SetInProgress(int id);
        void SetInDone(int id);
    }
}
