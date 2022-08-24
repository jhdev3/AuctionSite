using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models
{
    public class Category : DBentity
    {
        [Required(ErrorMessage = "Field can't be empty!")]
        public string CategoryName { get; set; }    

    }
}
