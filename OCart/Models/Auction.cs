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

        [Required]
        [MaxLength(200)]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }

        public Decimal InitialBet { get; set; }
        public Decimal FinishedBet { get; set; }
        public String Story { get; set; }

        // Добавить коллекцию тегов
        public ICollection<AuctionComment> AuctionComments { get; set; }
        public ICollection<AuctionPicture> AuctionPictures { get; set; }
    }
}
