namespace AuctionSite.Models.ViewModels
{
    /// <summary>
    /// View Model for View and 
    /// </summary>
    public class AuctionItemBidVM
    {
        AuctionItem auctionItem { get; set; }        

        //Dont make virtual then it could be used for  lazy loading.that creates n+1 quries :(
        //Also could set  lazyloading to  false in AppDbContext
        public IList<Bid> bids { get; set; }
    }
}
