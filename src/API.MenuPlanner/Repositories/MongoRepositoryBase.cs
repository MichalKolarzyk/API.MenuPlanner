using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class MongoRepositoryBase<T> : IRepository<T>
    {
        IMongoCollection<T> _mongoCollection;
        public MongoRepositoryBase(IMongoDbContext mongoDbContext, string collectionName)
        {
            _mongoCollection = mongoDbContext.GetCollection<T>(collectionName);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return _mongoCollection.Find(expression).ToListAsync();
        }

        public async Task AddAsync(T dish)
        {
            await _mongoCollection.InsertOneAsync(dish);
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _mongoCollection.InsertManyAsync(items);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return _mongoCollection.Find(expression).FirstOrDefaultAsync();
        }
    }
}
