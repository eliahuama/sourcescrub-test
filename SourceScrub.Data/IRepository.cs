using System.Linq.Expressions;

namespace SourceScrub.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<ICollection<T>> AddAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
