using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class AuctionOrderFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AuctionOrderMessageId { get; set; }
        public AuctionOrderMessage AuctionOrderMessage { get; set; }

        public DateTime Created { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Path { get; set; }
    }
}
