using AuctionSite.Data;
using AuctionSite.Models;
using AuctionSite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> AuctionItemDetailsAsync(Guid? id)
        {
            //Could use Explicit loading first getting Acution Item then Getting all the bids 2 round trip to DB here i rather add a list in my AuctionItems.:)
            //for simplicity i use Eager loading beacuse I dont have that many tables and includes this makes it simpler :)
            if(id == null)  
                return BadRequest();

            var item = await _db.AuctionItems.Where(a => a.Id == id).Include(u => u.User)
                                              .Include(b => b.bids.OrderByDescending(x => x.BidPrice))
                                              .ThenInclude(x => x.User)
                                              .FirstOrDefaultAsync();

          
            if (item == null)
                return NotFound();

            var getIdentity = (ClaimsIdentity)User.Identity;//Used as Notification if u got highest bid instead of validating that
            var claim = getIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string highestBidder = item.bids.Count() > 0 ? item.bids.First().UserID : "NoBidders";
            if (claim != null && claim.Value == highestBidder)
            {
                ViewData["HighestBid"] = "true";
            }
          
            AuctionItemBidVM vm = new AuctionItemBidVM{ auctionItem = item};
            
            return View(vm);
        }

        #region API
        // POST: Make a bid
        [Authorize]//Need to be logged in/have an account to Place a bid.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceAbid(AuctionItemBidVM bid)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    ModelError = ModelState.Values.SelectMany(x => x.Errors)
                });
            }

            //Getting the user thats placing the bid :)
            var getIdentity = (ClaimsIdentity)User.Identity;
            var claim = getIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return NotFound("User don't exists");
            }
            //Do some Validation check higestBid
            var heighestBid = await _db.Bids.Where(x => x.AuctionItemId == bid.auctionItem.Id).MaxAsync(x => (int?)x.BidPrice) ?? bid.auctionItem.StartingBid; //Get the current 

            //H :)
            if (bid.placeBid <= heighestBid)
            {
                ModelState.AddModelError(string.Empty, $"You need to place a bid heigher than:$ {heighestBid}");
                return Json(new
                {
                    success = false,
                    ModelError = ModelState.Values.SelectMany(x => x.Errors)
                });

            }
            //Save to db 
            Bid newBid = new Bid{ AuctionItemId = bid.auctionItem.Id, BidPrice = (int)bid.placeBid, UserID = claim.Value};
            _db.Bids.Add(newBid);
            try
            {
                await _db.SaveChangesAsync();
            }catch (Exception ex)
            {
                //Its not a modelstate error but a good way to not reapet code and give user some feedback
                ModelState.AddModelError(string.Empty, "Database error, try again later!");
                return Json(new
                {
                    success = false,
                    ModelError = ModelState.Values.SelectMany(x => x.Errors)
                });
            }
            TempData["success"] = $"You got the highest bid right now!!";
            return Json(new { Success = true});
        }

        #endregion
    }
}

