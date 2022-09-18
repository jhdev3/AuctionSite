using AuctionSite.Data;
using AuctionSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UR.Role_Admin)]//Setting all this to Admin Accsess only
    public class ManagerCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ManagerCategoryController(ApplicationDbContext dbcontext)
        {
            _db = dbcontext;
        }


        // GET: A list of all Categories and a way to mange them     
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            return View(categories);
        }

        // GET: ManagerCategoryController/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {
            var category = await _db.Categories.FindAsync(Id);
            if (category == null)
            {
                return NotFound(Id);
            }

            return PartialView(category);
        }

        #region API CALLS
        /// <summary>
        /// Post function of creat a Category and save it to database,
        /// will return JSON object of succes and incase of insucces modelstate errors with that.
        /// </summary>
        /// <param name="category">Model Category </param>
        /// <returns>Json object{ succes, ModelError}</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //Check if category already exist.
                var test = await _db.Categories.Where(c => c.CategoryName == category.CategoryName).FirstOrDefaultAsync();
                if (test != null) //If it exists
                {
                    ModelState.AddModelError("", $"The Category: {category.CategoryName} already exists");
                    return Json(new
                    {
                        success = false,
                        ModelError = ModelState.Values.SelectMany(x => x.Errors)
                    });
                }
                category.CategoryName = char.ToUpper(category.CategoryName[0]) + category.CategoryName.Substring(1).ToLower(); //Make Categoriged like a title. :)
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                return Json (new { Success = true });
            }
            return Json(new
            {
                success = false,
                ModelError = ModelState.Values.SelectMany(x => x.Errors)
            });
        }
        /// <summary>
        /// Delete a category and uses TempData to view message if it got deleted or not
        /// </summary>
        /// <param name="Id"> Category Id</param>
        /// <returns> Ok() = Server recived the request</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid?  Id)
        {                    
                //Check if the category exists in database
            var test = await _db.Categories.FindAsync(Id);
            if (test == null) //If it not exists
            {
                TempData["Error"] = $"Category already been deleted!";
                return Ok();
            }

            _db.Categories.Remove(test);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Successfully removed : {test.CategoryName} !";
            return Ok();       
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //Check if Category name already Exist so not Edit will create 2 of the same name :)
                var test = await _db.Categories.Where(c => c.CategoryName == category.CategoryName).FirstOrDefaultAsync();
                if (test != null) //If it exists
                {
                    ModelState.AddModelError("", $"The Category: {category.CategoryName} already exists");
                    return Json(new
                    {
                        success = false,
                        Id = category.Id,
                        ModelError = ModelState.Values.SelectMany(x => x.Errors)
                    });
                }
                _db.Categories.Update(category);
                //Concurrency exeptions -Can be applied here if for example someone deletes the Category you are tring to Edit. 
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //want to reaload page so incase u want to try again or the category already been update it will show. Here is good to logg ex.Message for futher investigation and bugg checks if this would be a real app ;)
                    TempData["Error"] = $"Failed to update : {category.CategoryName}"; //ex.Message for whole error message or Implement an logger and store it there :)
                    return Json(new { Success = true }); //True so i can reaload the window ;) and show the Failure message passed throw to TempData
                }
                TempData["Success"] = $"Edit Success :)";

                return Json(new { Success = true });
            }
            return Json(new
            {
                success = false,
                Id = category.Id,
                ModelError = ModelState.Values.SelectMany(x => x.Errors)
            });
        }
        #endregion

    }
}

