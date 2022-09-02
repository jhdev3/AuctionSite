using AuctionSite.Data;
using AuctionSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        // GET: ManagerCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }


        ////Make This An API Create End Points for the Failure for the stuff :) Asp-Validation all works :). 
        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePartial(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //If Name already exist.
        //        var test = await _db.Categories.Where(c => c.CategoryName == category.CategoryName).FirstOrDefaultAsync();
        //        if (test != null) //If it exists
        //        {
        //            ModelState.AddModelError("", $"Kategorin: {category.CategoryName} finns redan");
        //            TempData["Error"] = $"Kategorin: {category.CategoryName} finns redan";
        //            //return Json(new
        //            //{
        //            //    status = "failure",
        //            //    formErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
        //            //});
        //           return BadRequest(ModelState);

        //        }
        //        _db.Categories.Add(category);
        //        await _db.SaveChangesAsync();
        //        TempData["Success"] = $"Lyckades skapa : {category.CategoryName} :)";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View("_CreatePartial", category);
        //}

        // GET: ManagerCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManagerCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }

    #endregion
}

