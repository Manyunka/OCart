using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCart.Data;
using OCart.Models;
using OCart.Models.ViewModels;
using OCart.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace OCart.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;

        public AuctionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
        }

        // GET: Auctions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Auctions
                .Include(a => a.Category)
                .Include(a => a.Creator)
                .OrderByDescending(a => a.Created);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Auctions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .Include(a => a.Category)
                .Include(a => a.Creator)
                //.Include(a => a.WinBet)
                //.Include(a => a.AuctionPictures)
                //.Include(a => a.AuctionComments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auctions/Create
        public async Task<IActionResult> Create()
        {
            if (!userPermissions.CanCreateAuction())
            {
                return NotFound();
            }

            var categories = await context.Categories.OrderBy(x => x.Name).ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id");
            //ViewData["WinBetId"] = new SelectList(context.Bets, "Id", "CreatorId");
            return View(new AuctionViewModel());
        }

        // POST: Auctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionViewModel model)
        {
            if (!userPermissions.CanCreateAuction())
            {
                return NotFound();
            }

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;

                var auction = new Auction
                {
                    CreatorId = user.Id,
                    Created = now,
                    Modified = now,
                    //Finished = now.AddDays(1),
                    CategoryId = model.CategoryId,
                    Title = model.Title,
                    Description = model.Description,
                    InitialBetCost = model.InitialBetCost
                };

                context.Add(auction);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = await context.Categories.OrderBy(x => x.Name).ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", model.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            //ViewData["WinBetId"] = new SelectList(context.Bets, "Id", "CreatorId", auction.WinBetId);
            return View(model);
        }

        // GET: Auctions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .SingleOrDefaultAsync(p => p.Id == id);
            if (auction == null || !userPermissions.CanEditAuction(auction))
            {
                return NotFound();
            }

            var model = new AuctionViewModel()
            {
                CategoryId = auction.CategoryId,
                Title = auction.Title,
                Description = auction.Description,
                InitialBetCost = auction.InitialBetCost
            };
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", auction.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            //ViewData["WinBetId"] = new SelectList(context.Bets, "Id", "CreatorId", auction.WinBetId);

            return View(model);
        }

        // POST: Auctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, AuctionViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .SingleOrDefaultAsync(p => p.Id == id);
            if (auction == null || !userPermissions.CanEditAuction(auction))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;

                auction.Modified = now;
                auction.Title = model.Title;
                auction.Description = model.Description;
                auction.InitialBetCost = model.InitialBetCost;

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", auction.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            //ViewData["WinBetId"] = new SelectList(context.Bets, "Id", "CreatorId", auction.WinBetId);
            return View(model);
        }

        // GET: Auctions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .Include(a => a.Category)
                .Include(a => a.Creator)
                //.Include(a => a.WinBet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null || !userPermissions.CanEditAuction(auction))
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .Include(a => a.Category)
                .Include(a => a.Creator)
                //.Include(a => a.WinBet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null || !userPermissions.CanEditAuction(auction))
            {
                return NotFound();
            }
            context.Auctions.Remove(auction);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionExists(Guid id)
        {
            return context.Auctions.Any(e => e.Id == id);
        }
    }
}
