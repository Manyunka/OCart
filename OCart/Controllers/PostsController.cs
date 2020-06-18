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
using Microsoft.AspNetCore.Http;


namespace OCart.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissions;
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly IWebHostEnvironment hostingEnvironment;

        public PostsController(ApplicationDbContext context,
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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.PostPictures);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gallery/Details/5
        [AllowAnonymous]
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
            if (!userPermissions.CanCreatePost())
            {
                return NotFound();
            }

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
            if (!userPermissions.CanCreatePost())
            {
                return NotFound();
            }

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

            var post = await context.Posts
                .Include(p => p.PostPictures)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (post == null || !userPermissions.CanEditPost(post))
            {
                return NotFound();
            }

            var pictures = new List<IFormFile>();
            foreach (var p in post.PostPictures)
            {
                var attachmentPath = Path.Combine(hostingEnvironment.WebRootPath, "posts", post.Id.ToString("N"), p.Name);;
                using var stream = new FileStream(attachmentPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "multipart/form-data"
                };

                pictures.Add(file);
            }

            var model = new PostEditModel()
            {
                CategoryId = post.CategoryId,
                Text = post.Text,
                Pictures = pictures
            };

            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", post.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(model);
        }

        // POST: Gallery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, PostEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .Include(p => p.PostPictures)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (post == null || !userPermissions.CanEditPost(post))
            {
                return NotFound();
            }

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

            if (ModelState.IsValid)
            {
                var filePaths = Directory.GetFiles(Path.Combine(hostingEnvironment.WebRootPath, "posts", post.Id.ToString("N")));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

                var now = DateTime.UtcNow;

                post.Modified = now;
                post.Text = model.Text;

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

                        var pictures = context.PostPictures.Where(p => p.PostId == post.Id);
                        context.PostPictures.RemoveRange(pictures);
                        context.Add(postPicture);
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Name", post.CategoryId);
            //ViewData["CreatorId"] = new SelectList(context.Set<ApplicationUser>(), "Id", "Id", post.CreatorId);
            return View(model);
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
            if (post == null || !userPermissions.CanEditPost(post))
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null || !userPermissions.CanEditPost(post))
            {
                return NotFound();
            }

            var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "posts", post.Id.ToString("N"));
            Directory.Delete(folderPath, true);

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
