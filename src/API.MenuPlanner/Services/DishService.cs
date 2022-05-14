using API.MenuPlanner.Agregates;
using API.MenuPlanner.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Services
{
    public class DishService
    {
        private readonly IMongoCollection<Dish> _dishCollection;
        private readonly IMongoCollection<Recipe> _recipeCollection;

        public DishService(IOptions<MenuPlannerDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _dishCollection = mongoDatabase.GetCollection<Dish>(databaseSettings.Value.DishesCollectionName);
            _recipeCollection = mongoDatabase.GetCollection<Recipe>(databaseSettings.Value.RecipeCollectionName);
        }

        public async Task<List<Dish>> GetAsync()
        {
            return await _dishCollection.Find(_ => true).ToListAsync();
        }
        public async Task<DishAgregate?> GetAsync(string id)
        {
            DishAgregate dishAgregate = new DishAgregate();
            Dish dish = await _dishCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (dish == null)
                throw new Exception($"Not found dish with id {id}");

            dishAgregate.Id = id;
            dishAgregate.Recipe = await _recipeCollection.Find(x => x.Id == dish.RecipeId).FirstOrDefaultAsync();

            return dishAgregate;

        }

        public async Task CreateAsync(Dish newDish)
        {
            var recipe = _recipeCollection.Find(x => x.Id == newDish.RecipeId);
            if (recipe == null)
                throw new Exception($"Not found recepy with id {newDish.RecipeId}");

            await _dishCollection.InsertOneAsync(newDish);           
        }

        public async Task UpdateAsync(string id, Dish updatedDish) =>
            await _dishCollection.ReplaceOneAsync(x => x.Id == id, updatedDish);

        public async Task RemoveAsync(string id) =>
            await _dishCollection.DeleteOneAsync(x => x.Id == id);
    }
}
