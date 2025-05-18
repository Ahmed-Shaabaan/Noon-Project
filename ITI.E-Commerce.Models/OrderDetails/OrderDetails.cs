using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class OrderDetails
    {
        public int OrderID { get; set; } //composit primary key 

        public int ProductID { get; set; } // // // // // // // 

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; } 
    
        public virtual Order Orders { get; set; } 

        public virtual Product Products { get; set; }
    }
}
