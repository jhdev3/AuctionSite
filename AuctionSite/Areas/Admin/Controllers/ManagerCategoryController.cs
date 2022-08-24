using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Areas.Admin.Controllers
{
    public class ManagerCategoryController : Controller
    {
        //private readonly DBContext _DBContext;


        //public ManagerCategoryController(IUnitOfWork context)
        //{
        //    _UnitOfWork = context;
        //}


        // GET: A list of all Categories and a way to mange them     
        public async Task<IActionResult> Index()
        {
            return View();
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
