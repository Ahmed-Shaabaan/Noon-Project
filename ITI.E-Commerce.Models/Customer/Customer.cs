using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class Customer : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List <Order> Orders { get; set; }
        public virtual List<Order> Orders_Shipper { get; set; }
    }
}
