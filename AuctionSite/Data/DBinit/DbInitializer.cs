using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models;

namespace AuctionSite.Data.DBinit
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;


        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
             ILogger<DbInitializer> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }


        public void Initialize()
        {
            //Appling migrations if they are not already applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate(); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); //Just logging Error message incase something happens :)
            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(UR.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UR.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UR.Role_AuctionUser)).GetAwaiter().GetResult();
                //Seeding an Admin User if there is no roles and appling the admin role to it.
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "SiteAdmin",
                    Email = "SiteAdmin@ausctionsite.dev",
                }, "@Test1234").GetAwaiter().GetResult();

                IdentityUser user =  _userManager.FindByEmailAsync("SiteAdmin@ausctionsite.dev").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, UR.Role_Admin).GetAwaiter().GetResult();
            }
        }
    }
}
