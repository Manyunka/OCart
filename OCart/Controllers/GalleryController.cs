using System;
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
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext context;

        public GalleryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Gallery
        public async Task<IActionResult> Index()
        {
            var categories = await context.Categories.ToListAsync();

            var posts = await context.Posts
                //.Include(p => p.PostPictures)
                .ToListAsync();
            /*var pictures = new Dictionary<Guid, string>();
            foreach (var c in categories)
            {
                var post = posts.LastOrDefault(p => p.CategoryId == c.Id);
                if(post != null)
                    pictures[c.Id] = post.PostPictures.FirstOrDefault().Path;
            }

            ViewBag.Pictures = pictures;*/
            return View(categories);
        }

        // GET: Gallery/Details/5
        public async Task<IActionResult> Details(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var category = await context.Categories
                .FirstOrDefaultAsync(m => m.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var posts = await context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                //.Include(p => p.PostPictures)
                .ToListAsync();

            return View(posts);
        }

        /*// GET: Gallery/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CreatorId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Gallery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatorId,CategoryId,Created,Modified,Text")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["CreatorId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(post);
        }

        // GET: Gallery/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["CreatorId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(post);
        }

        // POST: Gallery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatorId,CategoryId,Created,Modified,Text")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["CreatorId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(post);
        }

        // GET: Gallery/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }*/
    }
}
