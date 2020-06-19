using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

        public Decimal InitialCostBet { get; set; }

        public Guid? FinishedBetId { get; set; }
        public Bet FinishedBet { get; set; }

        // Добавить коллекцию тегов
        public ICollection<AuctionComment> AuctionComments { get; set; }
        public ICollection<AuctionPicture> AuctionPictures { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
