using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SourceScrub.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public Task<T?> GetAsync(int id)
        {
            return _dbSet.FindAsync(id).AsTask();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<ICollection<T>> AddAsync(ICollection<T> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
    }
}
