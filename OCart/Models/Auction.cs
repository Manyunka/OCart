using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCart.Models
{
    public class Auction : Activity
    {
        [Column(TypeName = "Money")]
        public Decimal InitialBetCost { get; set; }
        
        public ICollection<Bet> Bets { get; set; }
        //public ICollection<AuctionComment> AuctionComments { get; set; }
        //public ICollection<AuctionPicture> AuctionPictures { get; set; }
    }
}
