using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Model
{
    public class OrderCreateModelApi
    {
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
            //public decimal TotalPrice { get; set; }
        public OrderStatues Status { get; set; } = OrderStatues.Pending;
        public List<OrderDetailsList> OrderDetails { get; set; }
    }
}
