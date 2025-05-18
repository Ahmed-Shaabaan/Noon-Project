using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class ProductImages: BaseEntity
    {

        public int ProductID { get; set; }
        
        public string Path { get; set; }

        public virtual Product Products { get; set; }

    }
}
