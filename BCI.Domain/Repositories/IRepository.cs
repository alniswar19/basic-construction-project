using BCI.Domain.QueryFilters;
using System.Linq.Expressions;

namespace BCI.Domain.Repositories
{
    public interface IRepository<T, Key>
    {
        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Get(Key id);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }

    public interface IRepositoryExtended<T, T2> where T : class where T2 : BaseFilter
    {
        Task<Tuple<IEnumerable<T>, int>> FindAll(T2 filters);
    }
}
