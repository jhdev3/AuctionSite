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

        // GET: ManagerCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManagerCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
    }
}
