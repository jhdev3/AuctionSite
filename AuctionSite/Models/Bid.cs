using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models
{
    public class Bid : DBentity
    {
        [Required(ErrorMessage = "Empty bid is no bid ;)!!")]
        public decimal BidPrice { get; set; }//Decimal beacuse its money :)    

        public Guid BidderId { get; set; }
    }
}
