using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDbContext : DbContext
    {
        //contructor passing the options inherited from DbContext to the base class
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        //Dbset (act as table in sqlserver)
        public DbSet<Card> Cards { get; set; }
    }
}
