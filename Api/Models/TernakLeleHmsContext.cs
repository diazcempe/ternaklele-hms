using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class TernakLeleHmsContext : DbContext
    {
        public TernakLeleHmsContext(DbContextOptions<TernakLeleHmsContext> options)
            : base(options) { }
        
        public TernakLeleHmsContext() : base() { }

        public DbSet<Inventory> Inventories { get; set; }

    }
}
