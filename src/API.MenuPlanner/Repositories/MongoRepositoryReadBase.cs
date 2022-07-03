using API.MenuPlanner.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public abstract class MongoRepositoryReadBase<T> : IRepositoryRead<T>
    {
        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return GetCollectionQueryable().Where(expression).ToListAsync();
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null, Expression<Func<T, object>>? keySelector = null)
        {
            IMongoQueryable<T> querable = GetCollectionQueryable();
            if (expression != null)
                querable = querable.Where(expression);

            if (keySelector != null)
                querable = querable.OrderBy(keySelector);

            var items = querable.Skip(skip ?? 0)
                .Take(take ?? 100)
                .ToListAsync();

            return items;
        }
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return GetCollectionQueryable().FirstOrDefaultAsync(expression);
        }

        protected abstract IMongoQueryable<T> GetCollectionQueryable();
    }
}
