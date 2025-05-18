using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Presentation.Models
{
    public class RoleUpdateModel
    {

        //[MaxLength(100)]
        //[MinLength(3)]
        //[Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
