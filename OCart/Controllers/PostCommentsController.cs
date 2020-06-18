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
    [Authorize]
    public class PostCommentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;

        public PostCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissions)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
        }

        /*// GET: PostComments
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
        }*/

        // GET: PostComments/Create
        public async Task<IActionResult> Create(Guid? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .SingleOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }

            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id");
            ViewBag.Post = post;
            return View(new PostCommentEditModel());
        }

        // POST: PostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? postId, PostCommentEditModel model)
        {
            if (postId == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .SingleOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }

            var user = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;

                var postComment = new PostComment
                {
                    PostId = post.Id,
                    CreatorId = user.Id,
                    Created = now,
                    Modified = now,
                    Text = model.Text
                };

                context.Add(postComment);
                await context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = post.Id });
            }
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewBag.Post = post;
            return View(model);
        }

        // GET: PostComments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments
                .Include(c => c.Post)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (postComment == null || !userPermissions.CanEditPostComment(postComment))
            {
                return NotFound();
            }

            var model = new PostCommentEditModel
            {
                Text = postComment.Text
            };

            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewBag.Post = postComment.Post;
            return View(model);
        }

        // POST: PostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, PostCommentEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments
                .Include(c => c.Post)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (postComment == null || !userPermissions.CanEditPostComment(postComment))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                postComment.Text = model.Text;
                postComment.Modified = DateTime.UtcNow;
                
                await context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = postComment.PostId });
            }
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", postComment.CreatorId);
            ViewBag.Post = postComment.Post;
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
            if (postComment == null || !userPermissions.CanEditPostComment(postComment))
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
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await context.PostComments
                .Include(p => p.Creator)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null || !userPermissions.CanEditPostComment(postComment))
            {
                return NotFound();
            }
            context.PostComments.Remove(postComment);
            await context.SaveChangesAsync();
            return RedirectToAction("Details", "Posts", new { id = postComment.PostId });
        }
    }
}
