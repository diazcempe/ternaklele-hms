using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Consts;
using Api.Utils;
using Api.Models;
using Api.Repositories;
using AutoMapper;
using Common.Dtos;
using Common.ViewModels.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Services
{
    public interface IInventoriesService
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync();
        Task<InventoryDto> GetByIdAsync(long id);
        Task<OperationResult<object>> CreateAsync(InventoryCreateVm vm);
        Task<OperationResult<object>> DeleteAsync(long id);
    }

    public class InventoriesService : IInventoriesService
    {
        private readonly IInventoriesRepository _inventoriesRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoriesService> _logger;

        public InventoriesService(IInventoriesRepository inventoriesRepo, IMapper mapper, ILogger<InventoriesService> logger)
        {
            _inventoriesRepo = inventoriesRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync()
        {
            var inventories = await _inventoriesRepo.GetAllAsync();
            return inventories.Select(s => _mapper.Map<InventoryDto>(s));
        }

        public async Task<InventoryDto> GetByIdAsync(long id)
        {
            var inventory = await _inventoriesRepo.GetByIdAsync(id);
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<OperationResult<object>> CreateAsync(InventoryCreateVm vm)
        {
            // CHECKING
            if (await _inventoriesRepo.IsExistsAsync(vm.Name))
                return new OperationResult<object>($"Inventory with {nameof(vm.Name)}: '{vm.Name}' is already exist in database.");

            // MAPPING
            var newInventory = _mapper.Map<Inventory>(vm);

            // ADDING DATA TO DB
            _inventoriesRepo.Add(newInventory);
            await _inventoriesRepo.SaveChangesAsync();

            // LOGGING AND RETURN
            _logger.LogInformation(ApiEvents.InventoryAdded, $"Model information: {vm}");
            return new OperationResult<object>(HttpStatusCode.Created);
        }

        public async Task<OperationResult<object>> DeleteAsync(long id)
        {
            // CHECKING
            var inventory = await _inventoriesRepo.GetByIdAsync(id);
            if (inventory == null)
                return new OperationResult<object>(HttpStatusCode.NotFound);

            // CAPTURING original DTO for LOGGING purposes.
            var inventoryDto = _mapper.Map<InventoryDto>(inventory); // for logging purposes.

            // DELETING DATA FROM DB
            _inventoriesRepo.Remove(inventory);
            await _inventoriesRepo.SaveChangesAsync();

            // LOGGING AND RETURN
            _logger.LogInformation(ApiEvents.InventoryDeleted, $"Model information: {inventoryDto}");
            return new OperationResult<object>(HttpStatusCode.OK, inventoryDto);
        }
    }
}
