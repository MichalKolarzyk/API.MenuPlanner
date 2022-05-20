using API.MenuPlanner.Database;
using API.MenuPlanner.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class DishRepository : MongoRepositoryBase<Dish>
    {
        //IMongoCollection<Dish> _mongoCollection;
        //public DishRepository(IMongoDbContext mongoDbContext)
        //{
        //    _mongoCollection = mongoDbContext.GetCollection<Dish>("Dishes");
        //}

        //public Task<List<Dish>> FindAsync(Expression<Func<Dish, bool>> expression)
        //{
        //    return _mongoCollection.Find(expression).ToListAsync();
        //}

        //public async Task AddAsync(Dish dish)
        //{
        //    await _mongoCollection.InsertOneAsync(dish);
        //}

        //public async Task AddRangeAsync(IEnumerable<Dish> items)
        //{
        //    await _mongoCollection.InsertManyAsync(items);
        //}

        //public Task<Dish?> FirstOrDefaultAsync(Expression<Func<Dish, bool>> expression)
        //{
        //    return _mongoCollection.Find(expression).FirstOrDefaultAsync();
        //}
        public DishRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Dishes")
        {
        }
    }
}
