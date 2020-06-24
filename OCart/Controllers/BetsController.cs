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

namespace OCart.Controllers
{
    public class BetsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BetsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /*// GET: Bets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Bets.Include(b => b.Creator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await context.Bets
                .Include(b => b.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bet == null)
            {
                return NotFound();
            }

            return View(bet);
        }*/

        // GET: Bets/Create
        public async Task<IActionResult> Create(Guid? auctionId)
        {
            if (auctionId == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .SingleOrDefaultAsync(p => p.Id == auctionId);
            if (auction == null)
            {
                return NotFound();
            }
            return View(new BetCreateModel());
        }

        // POST: Bets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? auctionId, BetCreateModel model)
        {
            if (auctionId == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions
                .SingleOrDefaultAsync(p => p.Id == auctionId);
            if (auction == null)
            {
                return NotFound();
            }

            //var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;

                var bet = new Bet
                {
                    AuctionId = auction.Id,
                    //CreatorId = user.Id,
                    Cost = model.Cost
                };

                await context.SaveChangesAsync();
                return RedirectToAction("Details", "Auctions", new { id = auction.Id });
            }
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", bet.CreatorId);
            return View(model);
        }

        /*// GET: Bets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await context.Bets.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", bet.CreatorId);
            return View(bet);
        }

        // POST: Bets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AuctionId,CreatorId,Cost")] Bet bet)
        {
            if (id != bet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(bet);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BetExists(bet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", bet.CreatorId);
            return View(bet);
        }

        // GET: Bets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await context.Bets
                .Include(b => b.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bet == null)
            {
                return NotFound();
            }

            return View(bet);
        }

        // POST: Bets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bet = await context.Bets.FindAsync(id);
            context.Bets.Remove(bet);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BetExists(Guid id)
        {
            return context.Bets.Any(e => e.Id == id);
        }*/
    }
}
