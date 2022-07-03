using API.MenuPlanner.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class MongoRepositoryBase<T> : MongoRepositoryReadBase<T>, IRepository<T>
    {
        protected IMongoCollection<T> _mongoCollection;
        protected string _collectionName;
        public MongoRepositoryBase(IMongoDbContext mongoDbContext, string collectionName)
        {
            _collectionName = collectionName;
            _mongoCollection = mongoDbContext.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T dish)
        {
            await _mongoCollection.InsertOneAsync(dish);
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _mongoCollection.InsertManyAsync(items);
        }

        public Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem)
        {
            return _mongoCollection.ReplaceOneAsync(filter, updatedItem);
        }
        public Task DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            return _mongoCollection.DeleteOneAsync<T>(filter);
        }

        protected override IMongoQueryable<T> GetCollectionQueryable()
        {
            return _mongoCollection.AsQueryable();
        }
    }
}
