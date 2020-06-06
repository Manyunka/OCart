using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class AuctionOrder
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }

        [Required]
        public String CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public DateTime Created { get; set; }

        public StatusType Status { get; set; }
    }
}
