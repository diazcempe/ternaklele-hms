using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.Inventory
{
    public class InventoryDetailsVm
    {
        public long InventoryId { get; set; }
        public string CrossReference { get; set; }
        public string Description { get; set; }
        public string DistributionUnit { get; set; }
        public string InventoryType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string Rank { get; set; }
        public double ReorderPoint { get; set; }
    }
}
