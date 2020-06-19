using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models.ViewModels
{
    public class AuctionEditModel
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public decimal InitialBet { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
