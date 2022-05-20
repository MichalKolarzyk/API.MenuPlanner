using MongoDB.Driver;

namespace API.MenuPlanner.Database
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
