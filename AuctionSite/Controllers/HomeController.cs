using AuctionSite.Data;
using AuctionSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AuctionSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext, IEmailSender emailSender)
        {
            _logger = logger;
            _db = dbcontext;    
            _emailSender = emailSender; 
        }

        
        public IActionResult Index()
        {
            IEnumerable<AuctionItem> items = _db.AuctionItems.Include(b => b.bids.OrderByDescending(x => x.BidPrice)).ToList();
            return View(items);
        }

        public IActionResult AboutMe()
        {
            return View();
        }


     
        public IActionResult Contact()
        {
            SendEmail sendEmail = new SendEmail();
            return View(sendEmail);
        }

        [HttpPost]
        public async Task<IActionResult> ContactAsync(SendEmail obj)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(obj.Email, obj.Subject, obj.Message);
                TempData["success"] = $"Thanks for contacting me, expect an replay sone! :)";
                return RedirectToAction("Contact"); ;  
            }
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}