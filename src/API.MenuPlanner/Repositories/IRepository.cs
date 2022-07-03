using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public interface IRepository<T> : IRepositoryRead<T>, IRepositoryWrite<T>
    {
    }
}
