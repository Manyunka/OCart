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
    public class PostCommentsController : Controller
    {
        private readonly ApplicationDbContext context;

        public PostCommentsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: PostComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.PostComments.Include(p => p.Creator).Include(p => p.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostComments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments
                .Include(p => p.Creator)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // GET: PostComments/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id");
            ViewData["PostId"] = new SelectList(context.Posts, "Id", "CreatorId");
            return View();
        }

        // POST: PostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,CreatorId,Created,Modified,Text")] PostComment postComment)
        {
            if (ModelState.IsValid)
            {
                postComment.Id = Guid.NewGuid();
                context.Add(postComment);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewData["PostId"] = new SelectList(context.Posts, "Id", "CreatorId", postComment.PostId);
            return View(postComment);
        }

        // GET: PostComments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments.FindAsync(id);
            if (postComment == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewData["PostId"] = new SelectList(context.Posts, "Id", "CreatorId", postComment.PostId);
            return View(postComment);
        }

        // POST: PostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PostId,CreatorId,Created,Modified,Text")] PostComment postComment)
        {
            if (id != postComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(postComment);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCommentExists(postComment.Id))
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
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewData["PostId"] = new SelectList(context.Posts, "Id", "CreatorId", postComment.PostId);
            return View(postComment);
        }

        // GET: PostComments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments
                .Include(p => p.Creator)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // POST: PostComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var postComment = await context.PostComments.FindAsync(id);
            context.PostComments.Remove(postComment);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCommentExists(Guid id)
        {
            return context.PostComments.Any(e => e.Id == id);
        }
    }
}
