using API.MenuPlanner.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class MongoRepositoryBase<T> : IRepository<T>
    {
        protected IMongoCollection<T> _mongoCollection;
        protected string _collectionName;
        public MongoRepositoryBase(IMongoDbContext mongoDbContext, string collectionName)
        {
            _collectionName = collectionName;
            _mongoCollection = mongoDbContext.GetCollection<T>(collectionName);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return _mongoCollection.Find(expression).ToListAsync();
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null, Expression<Func<T, object>>? keySelector = null)
        {
            IMongoQueryable<T> querable = _mongoCollection.AsQueryable();
            if (expression != null)
                querable = querable.Where(expression);

            if (keySelector != null)
                querable = querable.OrderBy(keySelector);

            var items = querable.Skip(skip ?? 0)
                .Take(take ?? 100)
                .ToListAsync();

            return items;
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

        public Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem)
        {
            return _mongoCollection.ReplaceOneAsync(filter, updatedItem);
        }
        public Task DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            return _mongoCollection.DeleteOneAsync<T>(filter);
        }
    }
}
