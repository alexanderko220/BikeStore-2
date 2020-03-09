using bikeStore.Data;
using bikeStore.Data.Entities;
using bikeStore.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeStore.Data.Repository
{
    public class CategoryRepository : BaseRepo<Category>, ICategoryRepository
    {
        private StoreDbContext _context;
        private readonly ILogger<ICategoryRepository> _logger;

        public CategoryRepository(StoreDbContext context, ILogger<ICategoryRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("run GetCategoriesAsync");
                return await GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetCategoriesAsync  {ex}");
                throw;
            }
        }

        public async Task<Category> GetCategoryAsync(long id)
        {
            try
            {
                _logger.LogInformation($"GetCategoryAsync id={id}");
                return await _context.Categories.Where(x => x.CatId == id).FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetCategoryAsync {ex}");
                throw;
            }
        }

        public void CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    throw new ArgumentNullException(nameof(category));
                }

                _logger.LogInformation($"Try to add new Category");

                Add(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new Category {ex}");
                throw;
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    throw new ArgumentNullException(nameof(category));
                }
                _logger.LogInformation($"Try to update Category id = {category.CatId}");
                Update(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Category {ex}");
                throw;
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    throw new ArgumentNullException(nameof(category));
                }
                _logger.LogInformation($"Try to delete Category id= {category.CatId}");
                Delete(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete Category: {ex}");
                throw;
            }
        }

        public Task<IEnumerable<Category>> GetCategoriesByConditionAsync(Expression<Func<Category, bool>> expression)
        {
            try
            {
                _logger.LogInformation($"Try to get category range by  {JsonConvert.SerializeObject(expression.Parameters)}");
                return GetRangeByConditionAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get category range: {ex}");
                throw;
            }
        }
    }
}
