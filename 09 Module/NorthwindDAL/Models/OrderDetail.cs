namespace NorthwindDAL.Models
{
    public class OrderDetail
    {
        public decimal UnitPrice { get; set; }
        public object Quantity { get; set; }
        public Product Product { get; set; }
    }
}