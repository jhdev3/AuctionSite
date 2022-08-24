using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Models
{
    /// <summary>
    /// Database class that is good to use for classes with there own table so they get a unique ID :) 
    /// </summary>
    public class DBentity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Letting the DB set the Id
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
