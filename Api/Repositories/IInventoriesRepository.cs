using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Consts;
using Api.Models;
using Common.ViewModels.Inventory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Repositories
{
    public interface IInventoriesRepository
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByInternalIdAsync(string id);
        Task<Inventory> GetByIdAsync(long id);
        Task AddAsync(Inventory inventory);
        Task<bool> UpdateNoteAsync(InventoryEditVm vm);
        Task<bool> RemoveAsync(long id);
        Task<bool> IsExistsAsync(string name);
    }

    public class InventoriesRepository : IInventoriesRepository
    {
        private readonly TernakLeleHmsMongoContext _context;
        private readonly ILogger<InventoriesRepository> _logger;

        public InventoriesRepository(IOptions<MongoSettings> settings, ILogger<InventoriesRepository> logger)
        {
            _context = new TernakLeleHmsMongoContext(settings);
            _logger = logger;
        }

        //public async Task<bool> IsExistsAsync(string name) => await _context.Inventories.FindAsync(e => e.Name == name);
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            try
            {
                return await _context.Inventories.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task<Inventory> GetByInternalIdAsync(string id)
        {
            try
            {
                var internalId = GetInternalId(id);
                return await _context.Inventories
                    .Find(s => s.InternalId == internalId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task<Inventory> GetByIdAsync(long id)
        {
            try
            {
                return await _context.Inventories
                    .Find(s => s.InventoryId == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(Inventory inventory)
        {
            try
            {
                await _context.Inventories.InsertOneAsync(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateNoteAsync(InventoryEditVm vm)
        {
            var filter = Builders<Inventory>.Filter.Eq(s => s.InventoryId, vm.InventoryId);
            var update = Builders<Inventory>.Update
                .Set(s => s.CrossReference, vm.CrossReference)
                .CurrentDate(s => s.UpdatedOn);

            try
            {
                var actionResult = await _context.Inventories.UpdateOneAsync(filter, update);
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveAsync(long id)
        {
            try
            {
                var actionResult = await _context.Inventories.DeleteOneAsync(Builders<Inventory>.Filter.Eq("InventoryId", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            try
            {
                return await _context.Inventories
                    .Find(s => s.Name == name)
                    .AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiEvents.InventoryError, $"Exception has been thrown: {ex.Message}");
                throw;
            }
        }

        private static ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out var internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
