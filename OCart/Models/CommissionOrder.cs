using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class CommissionOrder : Order
    {
        public Guid? CommissionId { get; set; }
        public Commission Commission { get; set; }

        //public ICollection<CommissionOrderMessage> OrderMessages { get; set; }
    }
}
