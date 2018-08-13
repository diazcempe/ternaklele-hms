using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Common.Dtos;
using Common.Enums;
using Microsoft.Extensions.Logging;

namespace Api.Repositories
{
    public interface IInventoriesRepository : IRepository<Inventory>
    {
    }

    public class InventoriesRepository : BaseRepository<Inventory>, IInventoriesRepository
    {
        private readonly ILogger<InventoriesRepository> _logger;

        public InventoriesRepository(TernakLeleHmsContext context, ILogger<InventoriesRepository> logger) : base(context)
        {
            _logger = logger;
        }
    }
}
