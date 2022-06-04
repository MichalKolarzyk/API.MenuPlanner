using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> FindAsync(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null, Expression<Func<T, object>>? keySelector = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);
        Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem);
        Task DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}
