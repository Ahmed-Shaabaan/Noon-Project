﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class Brand: BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual List <Product> Products { get; set; }
    }
}
