using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class Bet
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public Decimal Cost { get; set; }
    }
}
