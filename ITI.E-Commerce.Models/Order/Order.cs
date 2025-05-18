using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class Order
    {
        public int ID { get; set; } // primary key

        public string CustomerID { get; set; } //Fk for Customer 

        public string ShipperID { get; set; }  //FK for Shippers

        public int AddressID { get; set; }   
        
        public DateTime OrderDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public double DeliveryCost { get; set; }
        public OrderStatues Status { get; set; } = OrderStatues.Pending;

        public decimal TotalPrice { get; set; }
        public virtual Customer Customers { get; set; }

        public virtual Customer Shippers { get; set; } 
  


        public virtual List<OrderDetails> OrderDetails { get; set; }

        public virtual Address ShippingAddress { get; set; }

    }
}
