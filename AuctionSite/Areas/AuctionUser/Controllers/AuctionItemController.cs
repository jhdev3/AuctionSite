using AuctionSite.Data;
using AuctionSite.Models;
using AuctionSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace AuctionSite.Areas.AuctionUser.Controllers
{
    [Area("AuctionUser")]
    public class AuctionItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;//To mange upload images.


        public AuctionItemController(ApplicationDbContext dbcontext, IWebHostEnvironment hostEnvironment = null)
        {
            _db = dbcontext;
            _hostEnvironment = hostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AuctionItem> AuctionItems = await _db.AuctionItems.Include(c => c.Category).ToListAsync();  

            return View(AuctionItems);
        }

        // GET: Create and Edit Veiw
        public async Task<IActionResult> CreateEdit(Guid? Id)
        {
            var categories = await _db.Categories.ToListAsync();
            AuctionItemVM AuctionItemVM = new()
            {
                AuctionItem = new(),
                CategoryList = categories.Select(i => new SelectListItem //Populate SelectList
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                })
            };

            if (Id == null)
            {
                return View(AuctionItemVM);
            }
            else
            {

            }
            AuctionItemVM.AuctionItem = await _db.AuctionItems.FindAsync(Id);
            if (AuctionItemVM.AuctionItem == null)
            {
                return NotFound(Id);//Extra check :)
            }

            return View(AuctionItemVM);
        }



        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(AuctionItemVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\auctionItems");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.AuctionItem.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.AuctionItem.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.AuctionItem.ImageUrl = @"\images\auctionItems\" + fileName + extension;

                }
                if (obj.AuctionItem.Id == Guid.Empty)
                {
                    _db.AuctionItems.Add(obj.AuctionItem);

                }
                else
                {
                    //This needs to be changed :)
                    _db.AuctionItems.Update(obj.AuctionItem);
                }
                await _db.SaveChangesAsync();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
