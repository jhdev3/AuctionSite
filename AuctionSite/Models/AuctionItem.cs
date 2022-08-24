using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models
{
    public class AuctionItem : DBentity
    {
        [Required(ErrorMessage ="Field can't be empty!")]
        public string ItemName { get; set; }
        
        
        //List of bids to track the history :) and show that aswell not only highest one ;) 
        public IList<Bid> Bids { get; set; } = new List<Bid>();//Not making this a virtual type beacuse virtula could open up lazyloading from db which i think unwise due to time complex n+1
    }
}
