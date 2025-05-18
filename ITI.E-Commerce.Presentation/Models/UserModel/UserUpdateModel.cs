using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Presentation.Models
{
    public class UserUpdateModel
    {
        //[Required, MaxLength(200), MinLength(3)]
        //[ Display(Name = "First Name")]
        public string FirstName { get; set; }
        //[Required, MaxLength(200), MinLength(3)] 
        //[Display(Name = "Last Name")]
        public string LastName { get; set; }
        //[Required, MaxLength(15), MinLength(3)]
        //[Display(Name = "User Name")]
        public string username { get; set; }
        //[Required, EmailAddress]
        //[Display(Name = "Email Address")]
        public string email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
