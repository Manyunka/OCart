using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class AuctionOrderMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AuctionOrderId { get; set; }
        public AuctionOrder AuctionOrder { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        [Required]
        public String Text { get; set; }

        public ICollection<AuctionOrderFile> OrderFiles { get; set; }
    }
}
