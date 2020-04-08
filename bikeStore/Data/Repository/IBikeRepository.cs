using bikeStore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bikeStore.Data.Repository
{
    public interface IBikeRepository
    {
        Task<IEnumerable<Bike>> GetBikesByCategoryAsync(long catId);
        Task<IEnumerable<Bike>> GetBikesAsync();
        Task<IEnumerable<Bike>> GetBikesAsync(IEnumerable<long> bikeIds);
        Task<Bike> GetBikeAsync(long id);
        void CreateBike(Bike bike);
        void CreateBikes(IEnumerable<Bike> bikes);
        void UpdateBike(Bike bike);
        void UpdateBikes(IEnumerable<Bike> bikes);
        void DeleteBike(Bike bike);
        void DeleteBikes(IEnumerable<Bike> bikes);
        Task<bool> SaveChangesAsync();
    }
}
