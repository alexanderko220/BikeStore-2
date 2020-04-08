using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bikeStore.Data.Repository
{
    public class BaseRepo<TEntity> : IRepo<TEntity>, IDisposable where TEntity : class
    {
        private StoreDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepo(StoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity item)
        {
            _dbSet.Add(item);
        }

        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            _dbSet.AddRange(items);
        }

        public virtual void Delete(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> items)
        {
            _dbSet.RemoveRange(items);
        }

        public virtual void Update(TEntity item)
        {
            _dbSet.Update(item);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> items)
        {

            _dbSet.UpdateRange(items);

        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = await Include(includeProperties).ToListAsync();
            return result.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;

                }
            }
        }
    }
}
