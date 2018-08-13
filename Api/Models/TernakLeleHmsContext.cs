using System.Collections.Generic;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class TernakLeleHmsContext : DbContext
    {
        public TernakLeleHmsContext(DbContextOptions<TernakLeleHmsContext> options)
            : base(options)
        {
        }

        //public TernakLeleHmsContext() : base() { }

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureInventories(modelBuilder);
        }

        private static void ConfigureInventories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasKey(t => t.InventoryId);
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory
                {
                    InventoryId = 1,
                    CrossReference = "Test",
                    DistributionUnit = DistributionUnit.Ampoule,
                    Description = "fwefwe",
                    InventoryType = InventoryType.Medication,
                    Name = "cdscsdc",
                    Price = new decimal(5.25),
                    Quantity = 2,
                    Rank = Rank.B,
                    ReorderPoint = 3
                },
                new Inventory
                {
                    InventoryId = 2,
                    CrossReference = "vdfvd",
                    DistributionUnit = DistributionUnit.Bag,
                    Description = "bdbd",
                    InventoryType = InventoryType.Supply,
                    Name = "vdfvf",
                    Price = new decimal(7.34),
                    Quantity = 6,
                    Rank = Rank.C,
                    ReorderPoint = 11
                }
            );
        }

        private static IEnumerable<Inventory> InventorySeeds()
        {
            return new List<Inventory>
            {
                new Inventory()
                {
                    CrossReference = "Test",
                    DistributionUnit = DistributionUnit.Ampoule,
                    Description = "fwefwe",
                    InventoryType = InventoryType.Medication,
                    Name = "cdscsdc",
                    Price = new decimal(5.25),
                    Quantity = 2,
                    Rank = Rank.B,
                    ReorderPoint = 3
                },
                new Inventory()
                {
                    CrossReference = "vdfvd",
                    DistributionUnit = DistributionUnit.Bag,
                    Description = "bdbd",
                    InventoryType = InventoryType.Supply,
                    Name = "vdfvf",
                    Price = new decimal(7.34),
                    Quantity = 6,
                    Rank = Rank.C,
                    ReorderPoint = 11
                }
            };
        }
    }
}
