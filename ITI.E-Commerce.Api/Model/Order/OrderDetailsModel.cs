using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Model
{
    public class OrderDetailsModel
    {
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public string Product_Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }

}
