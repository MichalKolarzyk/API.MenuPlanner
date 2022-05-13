using API.MenuPlanner.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Services
{
    public class DishService
    {
        private readonly IMongoCollection<Dish> _dishCollection;

        public DishService(IOptions<MenuPlannerDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _dishCollection = mongoDatabase.GetCollection<Dish>(databaseSettings.Value.DishesCollectionName);
        }

        public async Task<List<Dish>> GetAsync()
        {
            return await _dishCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Dish?> GetAsync(string id)
        {
            return await _dishCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Dish newDish) =>
            await _dishCollection.InsertOneAsync(newDish);

        public async Task UpdateAsync(string id, Dish updatedDish) =>
            await _dishCollection.ReplaceOneAsync(x => x.Id == id, updatedDish);

        public async Task RemoveAsync(string id) =>
            await _dishCollection.DeleteOneAsync(x => x.Id == id);
    }
}
