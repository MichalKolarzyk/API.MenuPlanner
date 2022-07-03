using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public interface IRepositoryWrite<T>
    {
        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);
        Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem);
        Task DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}
