using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Validators;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ITI.E_Commerce.Presentation.Models
{
    public class OrderCreateModel
    {

        //[Display(Name = "CustomerID")]
        //[Required(ErrorMessage = "CustomerID Is Required")]
        //public string CustomerID { get; set; }


        [Required, Display(Name = "ShipperID")]
        public int ShipperID { get; set; }


        [Required, Display(Name = "AddressID")]
        public int AddressID { get; set; }

        //[Required, Display(Name = "OrderDate")]
        //public DateTime OrderDate { get; set; }

        [Required, Display(Name = "Status")]
        public OrderStatues Status { get; set; }


        [Required, Display(Name = "ShippedDate")]
        public DateTime ShippedDate { get; set; }

        //[Required, Display(Name = "ShippingAddress")]
        //public Address ShippingAddress { get; set; }


        [Required, Display(Name = "DeliveryCost")]
        public double DeliveryCost { set; get; }

        //[Required, Display(Name = "TotalPrice")]
        //public decimal TotalPrice { get; set; }

        //[Required, Display(Name = "orderDetails")]
        //public List<OrderDetails> orderDetails { get; set; }



    }
}
