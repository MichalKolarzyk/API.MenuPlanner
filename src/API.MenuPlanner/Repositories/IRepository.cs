using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);
    }
}
