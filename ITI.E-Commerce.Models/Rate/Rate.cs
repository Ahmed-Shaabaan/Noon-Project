using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class Rate:BaseEntity
    {
        public int ProductID { get; set; }
        public int NumberOfStart { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string CustomerID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
