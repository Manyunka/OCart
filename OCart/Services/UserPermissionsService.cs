using OCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCart.Services
{
    public class UserPermissionsService : IUserPermissionsService
    {
        public bool CanBuy(Commission commission)
        {
            throw new NotImplementedException();
        }

        public bool CanChangeAuctionOrderStatus(AuctionOrder order)
        {
            throw new NotImplementedException();
        }

        public bool CanChangeCommissionOrderStatus(CommissionOrder order)
        {
            throw new NotImplementedException();
        }

        public bool CanEditAuction(Auction auction)
        {
            throw new NotImplementedException();
        }

        public bool CanEditAuctionComment(AuctionComment auctionComment)
        {
            throw new NotImplementedException();
        }

        public bool CanEditAuctionOrderMessage(AuctionOrderMessage message)
        {
            throw new NotImplementedException();
        }

        public bool CanEditCommission(Commission commission)
        {
            throw new NotImplementedException();
        }

        public bool CanEditCommissionComment(CommissionComment commissionComment)
        {
            throw new NotImplementedException();
        }

        public bool CanEditCommissionOrderMessage(CommissionOrderMessage message)
        {
            throw new NotImplementedException();
        }

        public bool CanEditMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public bool CanEditPost(Post post)
        {
            throw new NotImplementedException();
        }

        public bool CanEditPostComment(PostComment postComment)
        {
            throw new NotImplementedException();
        }

        public bool CanMakeBet(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
