using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models
{
    public class Category : DBentity
    {
        [Required(ErrorMessage = "Field can't be empty!")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "{0} cannot exceed {1} characters and not be smaller then {2} characters")]
        public string CategoryName { get; set; }    

    }
}
