using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Attributes;

namespace Common.ViewModels.Inventory
{
    [Validator(typeof(InventoryCreateVmValidation))]
    public class InventoryCreateVm
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
