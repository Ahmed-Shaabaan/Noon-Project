using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Model.Order
{
    public class Order_Details_response
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public OrderStatues Status { get; set; } = OrderStatues.Pending;
        public List<OrderDetailsModel> OrderDetails { get; set; }

       
    }
}
