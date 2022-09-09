using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionSite.Models.ViewModels
{
    /// <summary>
    /// Class used as a View Model to Create a dropdown list and not useing viewbags etcs
    /// </summary>
    public class AuctionItemVM
    {
        public AuctionItem AuctionItem { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }//Get the Category list
    }
}
