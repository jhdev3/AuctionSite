using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models.ViewModels
{
    /// <summary>
    /// View Model for View and 
    /// </summary>
    public class AuctionItemBidVM
    {

        [Required]
        public int? placeBid { get; set; } //Setting it nullable to not have 0 displayed in input field hopefully required is working to validate null

      [ValidateNever]
       public AuctionItem auctionItem { get; set; }

    }
}
