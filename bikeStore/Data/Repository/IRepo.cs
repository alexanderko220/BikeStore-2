using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bikeStore.Data.Repository
{
    public interface IRepo<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetRangeByConditionAsync(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity item);
        void AddRange(IEnumerable<TEntity> items);
        void Delete(TEntity item);
        void DeleteRange(IEnumerable<TEntity> items);
        void Update(TEntity item);
        void UpdateRange(IEnumerable<TEntity> items);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<TEntity>> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
