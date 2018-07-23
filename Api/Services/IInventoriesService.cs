using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Common.Dtos;

namespace Api.Services
{
    public interface IInventoriesService
    {
        Task<IEnumerable<InventoryDto>> GetInventories();
    }

    public class InventoriesService : IInventoriesService
    {
        private readonly IInventoriesRepository _inventoriesRepo;

        public InventoriesService(IInventoriesRepository inventoriesRepo)
        {
            _inventoriesRepo = inventoriesRepo;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories()
        {
            var dtos = new List<InventoryDto>()
            {
                new InventoryDto()
                {
                    InventoryId = 1,
                    CrossReference = "Test",
                    DistributionUnit = "csdcsd",
                    Description = "fwefwe",
                    InventoryType = "csdcsd",
                    Name = "cdscsdc",
                    Price = new decimal(5.25),
                    Quantity = 2,
                    Rank = "C",
                    ReorderPoint = 3
                },
                new InventoryDto()
                {
                    InventoryId = 2,
                    CrossReference = "vdfvd",
                    DistributionUnit = "vdfvdf",
                    Description = "bdbd",
                    InventoryType = "referf",
                    Name = "vdfvf",
                    Price = new decimal(7.34),
                    Quantity = 6,
                    Rank = "B",
                    ReorderPoint = 11
                }
            }.ToAsyncEnumerable();

            return await dtos.ToList();
        }
    }
}
