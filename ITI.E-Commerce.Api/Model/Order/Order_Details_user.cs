using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Model.Order
{
    public class Order_Details_user
    {
        public string FirstName { get; set; }
        public DateTime ShippedDate { get; set; }
        public double DeliveryCost { get; set; }
        public OrderStatues Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }

        public List<OrderDetailsModel> OrderDetails { get; set; }
    }
}
