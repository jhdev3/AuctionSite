using Microsoft.AspNetCore.Identity.UI.Services;

namespace AuctionSite.Models.Services
{
    /*Just a dummy class at the moment in case u want to send without smtp, you could use MimeKit/MailKit
     As nugget packages and add string Email Send from or UserEmail incase of logged in user, then have your site email as sendTo :)
     */
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">Send From</param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Could add for example here         emailMessage.To.Add(new MailboxAddress("", "support@auctionSite.hej")); if using mimekit/mailkit

            return Task.CompletedTask;  
        }
    }
}
