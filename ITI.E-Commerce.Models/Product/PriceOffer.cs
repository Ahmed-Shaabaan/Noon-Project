using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class PriceOffer: BaseEntity
    {
        public string PromotionalText { get; set; }
        public int NewPrice { get; set; }
        public int ProductID { get; set; }
    }
}
