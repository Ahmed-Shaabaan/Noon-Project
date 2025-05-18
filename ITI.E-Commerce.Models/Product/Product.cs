using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class Product: BaseEntity
    {
        
        public string Name { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public int Quantity { get; set; }

        public string Description { get; set; }

        public double Discount { get; set; }
        
        public string Color { get; set; }

        public string Size { get; set; }

        public int CategoryID { get; set; } 

        public int BrandID { get; set; }
        
        public virtual PriceOffer PriceOffer { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<ProductImages> ProductImages { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual List <Rate> Rates { get; set; }

       
    }
}
