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

        public Task DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            T item = _collection.FirstOrDefault(filter.Compile());
            _collection.Remove(item);

            return Task.Run(() => { });
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() => _collection.Where(expression.Compile()).ToList());
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null, Expression<Func<T, object>>? keySelector = null)
        {
            var compiledExpression = expression.Compile();
            return Task.Run(() => _collection.Where(compiledExpression).OrderBy(keySelector.Compile()).Skip(skip ?? 0).Take(take ?? 100).ToList());
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() => _collection.FirstOrDefault(expression.Compile()));
        }

        public Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T updatedItem)
        {
            var compiledExpression = filter.Compile();
            T item = _collection.FirstOrDefault(compiledExpression);
                
            for (int i = 0; i < _collection.Count; i++)
            {
                if (_collection[i].GetHashCode() == item.GetHashCode())
                {
                    _collection[i] = updatedItem;
                    break;
                }
            }
            return Task.Run(() => { });
        }
    }
}
