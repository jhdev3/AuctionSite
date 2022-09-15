using AuctionSite.Data;
using AuctionSite.Models;
using AuctionSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Areas.AuctionUser.Controllers
{
    [Area("AuctionUser")]
    public class BidController : Controller
    {

        private readonly ApplicationDbContext _db;


        public BidController(ApplicationDbContext dbcontext)
        {
            _db = dbcontext;

        }
        public IActionResult AuctionItemDetails(Guid? id)
        {
            //Could use Explicit loading first getting Acution Item then Getting all the bids 2 round trip to DB here i rather add a list in my AuctionItems.:)
            //for simplicity i use Eager loading beacuse I dont have that many tables and includes this makes it simpler :)
            var item = _db.AuctionItems.Where(a => a.Id == id).Include(b => b.bids).FirstOrDefault();


            return View(item);
        }
    }
}
