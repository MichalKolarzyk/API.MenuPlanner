using API.MenuPlanner.Database;
using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Requests;
using MongoDB.Driver;
using System.Linq.Expressions;
using Tag = API.MenuPlanner.Entities.Tag;

namespace API.MenuPlanner.Repositories
{
    public class RecipeRepository : MongoRepositoryBase<Recipe>
    {
        public RecipeRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Recipes")
        {
        } 
    }
}
