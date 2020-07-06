using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OCart.Models
{
    public class Auction : Activity
    {
        public Decimal InitialBet { get; set; }

        public ICollection<Bet> Bets { get; set; }
        //public ICollection<AuctionComment> AuctionComments { get; set; }
        //public ICollection<AuctionPicture> AuctionPictures { get; set; }
    }
}
