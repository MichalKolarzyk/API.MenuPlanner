using API.MenuPlanner.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api.MenuPlannerTest
{
    internal class RepositoryTestBase<T> : IRepository<T>
    {
        List<T> _collection = new List<T>();
        public Task AddAsync(T item)
        {
            return Task.Run(() => _collection.Add(item));
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            return Task.Run(() => _collection.AddRange(items));
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() => _collection.Where(expression.Compile()).ToList());
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() => _collection.FirstOrDefault(expression.Compile()));
        }

        public Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
