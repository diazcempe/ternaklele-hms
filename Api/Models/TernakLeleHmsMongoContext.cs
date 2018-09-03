using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Models
{
    public class TernakLeleHmsMongoContext
    {
        private readonly IMongoDatabase _database;

        public TernakLeleHmsMongoContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>("Inventories");
    }
}
