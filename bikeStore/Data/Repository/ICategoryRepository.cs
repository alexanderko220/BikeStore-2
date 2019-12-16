using bikeStore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeStore.Data.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(long id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
