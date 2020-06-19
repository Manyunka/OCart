﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCart.Data;
using OCart.Models;

namespace OCart.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuctionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Auctions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Auctions.Include(a => a.Category).Include(a => a.Creator);
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auctions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name");
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Auctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatorId,CategoryId,Created,Modified,Title,Description,InitialBet,FinishedBet,Story")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                auction.Id = Guid.NewGuid();
                context.Add(auction);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", auction.CategoryId);
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            return View(auction);
        }

        // GET: Auctions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", auction.CategoryId);
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            return View(auction);
        }

        // POST: Auctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatorId,CategoryId,Created,Modified,Title,Description,InitialBet,FinishedBet,Story")] Auction auction)
        {
            if (id != auction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(auction);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Id))
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
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", auction.CategoryId);
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", auction.CreatorId);
            return View(auction);
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var auction = await context.Auctions.FindAsync(id);
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
