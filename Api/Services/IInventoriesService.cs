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
        Task<IEnumerable<InventoryDto>> GetInventoriesAsync();
        Task<InventoryDto> GetInventoryAsync(long id);
        Task<OperationResult<Inventory>> CreateInventoryAsync(InventoryCreateVm vm);
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

        public async Task<IEnumerable<InventoryDto>> GetInventoriesAsync()
        {
            var inventories = await _inventoriesRepo.GetAllAsync();
            return inventories.Select(s => _mapper.Map<InventoryDto>(s));
        }

        public async Task<InventoryDto> GetInventoryAsync(long id)
        {
            var inventory = await _inventoriesRepo.GetByIdAsync(id);
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<OperationResult<Inventory>> CreateInventoryAsync(InventoryCreateVm vm)
        {
            // CHECKING
            if (await _inventoriesRepo.InventoryExistsAsync(vm.Name))
                return new OperationResult<Inventory>($"Inventory with {nameof(vm.Name)}: '{vm.Name}' is already exist in database.");

            // MAPPING
            var newInventory = _mapper.Map<Inventory>(vm);

            // ADDING DATA TO DB
            _inventoriesRepo.Add(newInventory);
            await _inventoriesRepo.SaveChangesAsync();

            // LOGGING AND RETURN
            _logger.LogInformation(ApiEvents.InventoryAdded, "Model information: {@eventVm}", vm);
            return new OperationResult<Inventory>(HttpStatusCode.Created);
        }
    }
}
