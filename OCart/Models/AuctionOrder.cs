using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class AuctionOrder : Order
    {
        public Guid? AuctionId { get; set; }
        public Auction Auction { get; set; }

        //public ICollection<AuctionOrderMessage> OrderMessages { get; set; }
    }
}
