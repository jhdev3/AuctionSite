using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Models
{
    public class Bid : DBentity
    {
        [Required(ErrorMessage = "Empty bid is no bid ;)!!")]
        public decimal BidPrice { get; set; }//Decimal beacuse its money :)    

        [ForeignKey("AuctionItem")]//Used to create a relationship with AuctionItem to faster db queries :)
        public Guid AuctionItemId { get; set; }
      //  public Guid BidderId { get; set; } This will be added when I Identity 
    }
}
