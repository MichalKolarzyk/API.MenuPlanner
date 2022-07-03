using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public interface IRepositoryRead<T>
    {
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> FindAsync(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null, Expression<Func<T, object>>? keySelector = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
