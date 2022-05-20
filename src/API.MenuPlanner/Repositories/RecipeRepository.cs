using API.MenuPlanner.Database;
using API.MenuPlanner.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class RecipeRepository : MongoRepositoryBase<Recipe>
    {
        public RecipeRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Recipes")
        {
        }
    }
}
