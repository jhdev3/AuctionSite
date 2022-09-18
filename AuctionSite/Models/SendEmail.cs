using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models
{
    public class SendEmail
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string Email { get; set; }   
        [Required(ErrorMessage = "Please include a subject!")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please don't send empty Mails!")]
        [Display(Name = "Message:")]
        public string Message { get; set; }    
    }
}
