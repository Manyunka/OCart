using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "Money")]
        public Decimal Cost { get; set; }
    }
}
