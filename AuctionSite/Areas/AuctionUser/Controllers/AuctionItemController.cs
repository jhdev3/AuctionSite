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
            AuctionItemCreateEditVM AuctionItemVM = new()
            {
                AuctionItem = new(),
                CategoryList = categories.Select(i => new SelectListItem //Populate SelectList
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                })
            };

            if (Id == null)//Create View :)
            {
                return View(AuctionItemVM);
            }
            //Edit View
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
        public async Task<IActionResult> CreateEdit(AuctionItemCreateEditVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\auctionItems");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.AuctionItem.ImageUrl != null)//For Updating the image removing old one!
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
                    _db.AuctionItems.Update(obj.AuctionItem);
                }
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //Using TempData to display error message that it was unsuccesfull save to database. Ex message should be logged if and when logger gets implemented 
                    TempData["Error"] = $"Failed to save : {obj.AuctionItem.Title}"; //ex.Message for whole error message or Implement an logger and store it there :)
                    return RedirectToAction("Index");
                }
                TempData["Success"] = $"Edit Success :)";

                await _db.SaveChangesAsync();
                TempData["success"] = $"{obj.AuctionItem.Title} saved succsefully!";
                return RedirectToAction("Index");
            }
            //Not valid modelstate return.
            return View(obj);

        }

        #region API
        //using tempdata and toaster to notify Error or Succses instead of JSON return. want to take advantage of the MVC stuff that can be used:)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? Id)
        {
            //Check if the category exists in database
            var test = await _db.AuctionItems.FindAsync(Id);
            if (test == null) //If it not exists
            {
                TempData["Error"] = "Item already been deleted!";
                return Ok();
            }

            _db.AuctionItems.Remove(test);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"{test.Title} : Deleted Successfully:)";
            return Ok();
        }

        #endregion
    }
}
