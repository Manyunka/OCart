using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCart.Models.ViewModels
{
    public class AuctionViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public decimal InitialBetCost { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
