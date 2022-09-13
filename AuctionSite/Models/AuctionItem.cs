using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Models
{
    public class AuctionItem : DBentity
    {
        [Required(ErrorMessage = "Field can't be empty!")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "{0} need to be postive with max 2 decimals")]
        [Range(0, Int32.MaxValue)]
        public decimal BidPrice { get; set; } = 0; //Sets to 0 if no other value is added  

        [ValidateNever]//Dont wont to validate beacuse in edit etc this field should be null etc
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]//Database fetch not userInput
        public Category Category { get; set; }

        //Later Fix to Set Creator on the AuctionItem i.e Who is auction this away;)

    }   
}