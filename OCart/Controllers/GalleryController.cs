using System;
using System.IO;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;


namespace OCart.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly IWebHostEnvironment hostingEnvironment;

        public GalleryController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserPermissionsService userPermissions,
            IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissions = userPermissions;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Gallery
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.PostPictures);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gallery/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.PostPictures)
                .Include(p => p.PostComments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Gallery/Create
        public async Task<IActionResult> Create()
        {
            var categories = await context.Categories.OrderBy(x => x.Name).ToListAsync();
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name");
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id");
            return View(new PostCreateModel());
        }

        // POST: Gallery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateModel model)
        {
            foreach (var p in model.Pictures)
            {
                if (p != null)
                {
                    var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(p.ContentDisposition).FileName.Value.Trim('"'));
                    var fileExt = Path.GetExtension(fileName);
                    if (!AllowedExtensions.Contains(fileExt))
                    {
                        ModelState.AddModelError(nameof(p), "This file type is prohibited");
                    }
                }
            }

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;

                var post = new Post
                {
                    CreatorId = user.Id,
                    Created = now,
                    Modified = now,
                    CategoryId = model.CategoryId,
                    Text = model.Text
                };

                Directory.CreateDirectory(Path.Combine(hostingEnvironment.WebRootPath, "posts", post.Id.ToString("N")));
                foreach (var p in model.Pictures)
                {
                    if (p != null)
                    {
                        var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(p.ContentDisposition).FileName.Value.Trim('"'));
                        var fileExt = Path.GetExtension(fileName);

                        var postPicture = new PostPicture
                        {
                            Created = DateTime.UtcNow,
                            PostId = post.Id,
                            Name = fileName
                        };

                        var attachmentPath = Path.Combine(hostingEnvironment.WebRootPath, "posts", post.Id.ToString("N"), fileName);
                        postPicture.Path = $"/posts/{post.Id:N}/{fileName}";

                        using var fileStream = new FileStream(attachmentPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);
                        await p.CopyToAsync(fileStream);

                        context.Add(postPicture);
                    }
                }

                context.Add(post);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = await context.Categories.OrderBy(x => x.Name).ToListAsync();
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", model.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(model);
        }

        // GET: Gallery/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", post.CategoryId);
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
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
                    context.Update(post);
                    await context.SaveChangesAsync();
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
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", post.CategoryId);
            ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(post);
        }

        // GET: Gallery/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
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
            var post = await context.Posts.FindAsync(id);
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return context.Posts.Any(e => e.Id == id);
        }
    }
}
