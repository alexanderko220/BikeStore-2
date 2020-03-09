using bikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeStore.Data.Repository
{
    public interface ISpecificationRepository
    {
        Task<IEnumerable<Specification>> GetSpecificationsAsync();
        Task<IEnumerable<Specification>> GetSpecificationsByCategory(long catId);
        Task<IEnumerable<Specification>> GetSpecificationsByConditionAsync(Expression<Func<Specification, bool>> expression);
        Task<Specification> GetSpecificationAsync(long id);
        void CreateSpecification(Specification specification);
        void UpdateSpecification(Specification specification);
        void DeleteSpecification(Specification specification);
    }
}
