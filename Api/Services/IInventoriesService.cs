using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using AutoMapper;
using Common.Dtos;

namespace Api.Services
{
    public interface IInventoriesService
    {
        Task<IEnumerable<InventoryDto>> GetInventoriesAsync();
        Task<InventoryDto> GetInventoryAsync(long id);
    }

    public class InventoriesService : IInventoriesService
    {
        private readonly IInventoriesRepository _inventoriesRepo;
        private readonly IMapper _mapper;

        public InventoriesService(IInventoriesRepository inventoriesRepo, IMapper mapper)
        {
            _inventoriesRepo = inventoriesRepo;
            _mapper = mapper;
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
    }
}
