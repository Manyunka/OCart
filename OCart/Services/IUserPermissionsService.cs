using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCart.Models;

namespace OCart.Services
{
    public interface IUserPermissionsService
    {
        bool CanCreatePost();
        bool CanEditPost(Post post);
        //bool CanEditPostComment(PostComment postComment);

        bool CanEditArtistComment(ArtistComment artistComment);

        bool CanCreateAuction();
        bool CanEditAuction(Auction auction);
       //bool CanEditAuctionComment(AuctionComment auctionComment);

        bool CanCreateCommission();
        bool CanEditCommission(Commission commission);
        //bool CanEditCommissionComment(CommissionComment commissionComment);

        //bool CanEditDialog(Dialog dialog);
        //bool CanEditMessage(Message message);
        bool CanEditAuctionOrderMessage(AuctionOrderMessage message);
        bool CanEditCommissionOrderMessage(CommissionOrderMessage message);

        bool CanChangeAuctionOrderStatus(AuctionOrder order);
        bool CanChangeCommissionOrderStatus(CommissionOrder order);

        bool CanMakeBet();
        bool CanBuy();
    }

}
