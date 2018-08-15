﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Common.Enums;
using FluentValidation.Attributes;

namespace Common.ViewModels.Inventory
{
    [Validator(typeof(InventoryCreateVmValidation))]
    public class InventoryCreateVm
    {
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

    [Validator(typeof(InventoryEditVmValidation))]
    public class InventoryEditVm : InventoryCreateVm
    {
        [Required]
        public long InventoryId { get; set; }
    }
}
