namespace AuctionSite.Models
{
    /// <summary>
    /// UR is user Roles, will us it as string constants where i want the to be used. 
    /// Admin is admin and has the highest priority and can do everything,
    /// While AuctionUser is smth u need to be to place a bid create action item etc with some limitations of what u can do and what content that user can manage 
    ///
    /// </summary>
    public class UR
    {
        public const string Role_AuctionUser = "AuctionUser";
        public const string Role_Admin = "Admin";
    }
}
