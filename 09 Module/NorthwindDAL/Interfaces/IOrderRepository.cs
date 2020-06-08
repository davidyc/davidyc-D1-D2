using NorthwindDAL.Enums;
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
        Order GetOrderById(int id);
        void AddNew(Order newOrder);
        Order Update(Order order);
        void SetInProgress(int id);
        void SetInDone(int id);
        void DeleteOrderByID(int orderID);
        IEnumerable<CustomerProductDetail> GetCustomerProductDetails(string customerID);
        IEnumerable<CustomerOrdersDetail> GetCustomerOrderDetails(int orderID);
    }
}
