using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Common.ViewModels.Inventory
{
    public class InventoryCreateVmValidation : AbstractValidator<InventoryCreateVm>
    {
        public InventoryCreateVmValidation()
        {
            RuleFor(vm => vm.Name).NotEmpty();
        }
    }
}
