using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public enum OrderStatues
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Confirmed")]
        Confirmed,
    }
}
