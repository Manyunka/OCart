using OCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace OCart.Services
{
    public class UserPermissionsService : IUserPermissionsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public UserPermissionsService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpContext HttpContext => this.httpContextAccessor.HttpContext;


        public bool CanEditArtistComment(ArtistComment artistComment)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == artistComment.CreatorId;
        }

        public bool CanEditPost(Post post)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == post.CreatorId;

        }
        public bool CanEditPostComment(PostComment postComment)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == postComment.CreatorId;
        }

        public bool CanEditAuction(Auction auction)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == auction.CreatorId;
        }
        public bool CanEditAuctionComment(AuctionComment auctionComment)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == auctionComment.CreatorId;
        }

        public bool CanEditCommission(Commission commission)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == commission.CreatorId;
        }
        public bool CanEditCommissionComment(CommissionComment commissionComment)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == commissionComment.CreatorId;
        }

        public bool CanEditDialog(Dialog dialog)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == dialog.UserId;
        }
        public bool CanEditMessage(Message message)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == message.CreatorId;
        }
        public bool CanEditAuctionOrderMessage(AuctionOrderMessage message)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == message.CreatorId;
        }
        public bool CanEditCommissionOrderMessage(CommissionOrderMessage message)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == message.CreatorId;
        }

        public bool CanChangeAuctionOrderStatus(AuctionOrder order)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == order.Auction.CreatorId;
        }
        public bool CanChangeCommissionOrderStatus(CommissionOrder order)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return userManager.GetUserId(httpContextAccessor.HttpContext.User) == order.Commission.CreatorId;
        }

        public bool CanMakeBet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Customers))
            {
                return false;
            }

            return true;
        }
        public bool CanBuy()
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Customers))
            {
                return false;
            }

            return true;
        }

        public bool CanCreatePost()
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return true;
        }

        public bool CanCreateAuction()
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return true;
        }

        public bool CanCreateCommission()
        {
            if (!HttpContext.User.Identity.IsAuthenticated || !HttpContext.User.IsInRole(ApplicationRoles.Artists))
            {
                return false;
            }

            return true;
        }
    }
}
