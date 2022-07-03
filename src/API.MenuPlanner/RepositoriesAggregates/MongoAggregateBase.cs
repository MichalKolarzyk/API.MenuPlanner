using API.MenuPlanner.Database;
using API.MenuPlanner.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace API.MenuPlanner.Aggregates
{
    public abstract class MongoAggregateBase<TBase, TResult> : MongoRepositoryReadBase<TResult>, IRepositoryRead<TResult>
    {
        protected IMongoCollection<TBase> _baseMongoCollection;
        protected IMongoDbContext _mongoDbContext;
        protected string _baseCollectionName;
        protected MongoAggregateBase(IMongoDbContext mongoDbContext, string baseCollectionName)
        {
            _mongoDbContext = mongoDbContext;
            _baseCollectionName = baseCollectionName;
            _baseMongoCollection = _mongoDbContext.GetCollection<TBase>(_baseCollectionName);
        }
    }
}
