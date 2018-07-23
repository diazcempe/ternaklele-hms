using Common.Enums;

namespace Api.Models
{
    public class Inventory
    {
        public Inventory() {}

        public long InventoryId { get; set; }
        public string CrossReference { get; set; }
        public string Description { get; set; }
        public DistributionUnit DistributionUnit { get; set; }
        public InventoryType InventoryType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public Rank Rank { get; set; }
        public double ReorderPoint { get; set; }
    }
}
