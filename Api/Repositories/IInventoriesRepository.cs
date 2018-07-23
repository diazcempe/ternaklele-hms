using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IInventoriesRepository : IRepository<Inventory>
    {
    }

    public class InventoriesRepository : BaseRepository<Inventory>, IInventoriesRepository
    {
        public InventoriesRepository(TernakLeleHmsContext context) : base(context)
        {
        }
    }
}
