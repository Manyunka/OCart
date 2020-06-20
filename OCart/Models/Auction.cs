using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCart.Models
{
    public class Auction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Finished { get; set; }

        [Required]
        [MaxLength(200)]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }

        [Column(TypeName = "Money")]
        public Decimal InitialCostBet { get; set; }

        public Guid? WinBetId { get; set; }
        public Bet WinBet { get; set; }

        // Добавить коллекцию тегов
        public ICollection<AuctionComment> AuctionComments { get; set; }
        public ICollection<AuctionPicture> AuctionPictures { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
