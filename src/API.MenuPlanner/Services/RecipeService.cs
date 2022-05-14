using API.MenuPlanner.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Services
{
    public class RecipeService
    {
        private readonly IMongoCollection<Recipe> _recipeCollection;

        public RecipeService(IOptions<MenuPlannerDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _recipeCollection = mongoDatabase.GetCollection<Recipe>(databaseSettings.Value.RecipeCollectionName);
        }

        public async Task CreateAsync(Recipe newRecipe)
        {
            await _recipeCollection.InsertOneAsync(newRecipe);
        }
    }
}
