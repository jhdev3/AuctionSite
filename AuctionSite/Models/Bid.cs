using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Models
{
    public class Bid : DBentity
    {
        [Required(ErrorMessage = "Empty bid is no bid ;)!!")]
        [Range(0, Int32.MaxValue, ErrorMessage = "{0} Can not be a negative value!")]
        public int BidPrice { get; set; }//Decimal beacuse its money :)    


        public Guid AuctionItemId { get; set; }
        [ForeignKey("AuctionItemId")] //Used to create a relationship with AuctionItem to faster db queries :)
        [ValidateNever]//Database fetch not userInput
        public AuctionItem AuctionItem { get; set; }
       

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]//Database fetch not userInput
        public IdentityUser User { get; set; } //maybe this should be Readonly  or only get doesnt matter i only set from db anyway.


    }
}
