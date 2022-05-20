using API.MenuPlanner.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Database
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public MongoDbContext(IOptions<MenuPlannerDatabaseSettings> databaseSettings)
        {
            _mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (collectionName == null)
                throw new Exception("You should define collectionName");

            return _db.GetCollection<T>(collectionName);
        }
    }
}
