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

        [Display(Name = "Starting bid  $")]
        [Range(0, Int32.MaxValue, ErrorMessage = "{0} Can not be a negative value!")]
        public decimal StartingBid { get; set; } = 0; //Sets to 0 if no other value is added  

        [ValidateNever]//Dont wont to validate beacuse in edit etc this field could be null and will be changed when file is added.
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]//Database fetch not userInput
        public Category Category { get; set; }

            
        public IList<Bid> bids { get; set; }    
        //Add User that created The item :) Need to add that

        //Later Fix to Set Creator on the AuctionItem i.e Who is auction this away;)

    }   
}