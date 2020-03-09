using bikeStore.Data;
using bikeStore.Data.Entities;
using bikeStore.Data.Repository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeStore.Data.Repository
{
    public class SpecificationRepository : BaseRepo<Specification>, ISpecificationRepository
    {
        private StoreDbContext _context;
        private readonly ILogger<ISpecificationRepository> _logger;
        public SpecificationRepository(StoreDbContext context, ILogger<ISpecificationRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region CRUD
        public void CreateSpecification(Specification specification)
        {
            try
            {
                if (specification == null)
                {
                    throw new ArgumentNullException(nameof(specification));
                }

                _logger.LogInformation($"Try to add new Specification");

                Add(specification);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new Specification {ex}");
                throw;
            }
        }
        public void UpdateSpecification(Specification specification)
        {
            try
            {
                if (specification == null)
                {
                    throw new ArgumentNullException(nameof(specification));
                }
                _logger.LogInformation($"Try to update specification id = {specification.SpecId}");
                Update(specification);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update specification id = {specification.SpecId} : {ex}");
                throw;
            }
        }
        public void DeleteSpecification(Specification specification)
        {
            try
            {
                if (specification == null)
                {
                    throw new ArgumentNullException(nameof(specification));
                }
                _logger.LogInformation($"Try to delete specification id= {specification.SpecId}");
                Delete(specification);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete specification id= {specification.SpecId}: {ex}");
                throw;
            }
        }
        #endregion CRUD

        public Task<IEnumerable<Specification>> GetSpecificationsByCategory( long catId)
        {
            try
            {
                _logger.LogInformation($"run GetSpecificationsByCategory, catId = {catId}");
                return GetRangeByConditionAsync(x => x.SpecCatId == catId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetSpecificationsByCategory catId= {catId}: {ex}");
                throw;
            }
        }

        public Task<Specification> GetSpecificationAsync(long id)
        {
            try
            {
                _logger.LogInformation($"run GetSpecificationByIdAsync, id = {id}");
                return GetByConditionAsync(x => x.SpecId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get specification id= {id}: {ex}");
                throw;
            }
        }

        public Task<IEnumerable<Specification>> GetSpecificationsAsync()
        {
            try
            {
                _logger.LogInformation($"run GetSpecificationsAsync");
                return GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetSpecificationsAsync: {ex}");
                throw;
            }
        }

        public Task<IEnumerable<Specification>> GetSpecificationsByConditionAsync(Expression<Func<Specification, bool>> expression)
        {
            try
            {
                _logger.LogInformation($"Try GetSpecificationsByConditionAsync by  {JsonConvert.SerializeObject(expression.Parameters)}");
                return GetRangeByConditionAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetSpecificationsByConditionAsync: {ex}");
                throw;
            }
        }

       
    }
}
