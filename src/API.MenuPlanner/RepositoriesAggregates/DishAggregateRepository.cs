using API.MenuPlanner.Database;
using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace API.MenuPlanner.Aggregates
{
    public class DishAggregateRepository : MongoAggregateBase<Dish, DishAggregate>
    {
        public DishAggregateRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Dishes")
        {
        }

        protected override IMongoQueryable<DishAggregate> GetCollectionQueryable()
        {
            IMongoCollection<Recipe> recipes = _mongoDbContext.GetCollection<Recipe>("Recipes");

            return from d in _baseMongoCollection.AsQueryable()
                   join r in recipes on d.RecipeId equals r.Id into joined
                   from recipe in joined.DefaultIfEmpty()
                   select new DishAggregate
                   {
                       Id = d.Id,
                       Day = d.Day,
                       DishType = d.DishType,
                       Recipe = recipe,
                       UserId = d.UserId,
                   };
        }
    }
}
