using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCart.Models
{
    public class Commission : Activity
    {
        [Column(TypeName = "Money")]
        public Decimal Price { get; set; }

        //public ICollection<CommissionComment> CommissionComments { get; set; }
        //public ICollection<CommissionPicture> CommissionPictures { get; set; }
        public ICollection<CommissionOrder> CommissionOrders { get; set; }
    }
}
