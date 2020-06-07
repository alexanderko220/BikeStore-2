using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikeStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bikeStore.Data.Repository
{
    public class BikeRepository : BaseRepo<Bike>, IBikeRepository
    {
        private StoreDbContext _context;
        private readonly ILogger<IBikeRepository> _logger;

        public BikeRepository(StoreDbContext context, ILogger<IBikeRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Bike>> GetBikesByCategoryAsync(long catId)
        {
            try
            {
                _logger.LogInformation($"run GetBikesByCategoryAsync, catId = {catId}");
                
                return await GetWithInclude(x => x.CategoryId == catId && x.IsInStock, c=> new object[]{ c.Category });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetBikesByCategoryAsync  {ex}");
                throw;
            }
        }

        public async Task<Bike> GetBikeAsync(long id)
        {
            try
            {
                _logger.LogInformation($"GetBikeAsync id={id}");
                return await _context.Bikes.Where(x => x.BikeId == id)
                    .Include(s => s.Specifications).ThenInclude(i => i.Specification)
                    .Include(c => c.Colors).ThenInclude(s => s.Color)
                    .Include(c => c.Sizes).ThenInclude(s => s.Size)
                    .Include(i => i.Images).ThenInclude(c => c.ImgContents)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetBikeAsync {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<Bike>> GetBikesAsync()
        {
            try
            {
                _logger.LogInformation("run GetBikesAsync");
                return await _context.Bikes.Include(c => c.Category)
                                           .Include(c => c.Colors).ThenInclude(s => s.Color)
                                           .Include(c => c.Sizes).ThenInclude(s => s.Size).Take(500).OrderBy( c=> c.Category.CatName).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetBikesAsync  {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<Bike>> GetBikesAsync(IEnumerable<long> bikeIds)
        {
            try
            {
                _logger.LogInformation("run GetBikesAsync");
                return await GetRangeByConditionAsync(x => bikeIds.Contains(x.BikeId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetBikesAsync  {ex}");
                throw;
            }
        }

        #region CRUD
        public void CreateBike(Bike bike)
        {
            try
            {
                if (bike == null)
                {
                    throw new ArgumentNullException(nameof(bike));
                }

                _logger.LogInformation($"Try to add new Bike");

                Add(bike);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new bike {ex}");
                throw;
            }
        }

        public void CreateBikes(IEnumerable<Bike> bikes)
        {
            try
            {
                if (bikes != null && bikes.Any())
                {
                    _logger.LogInformation($"Try to add new range of bikes");
                    AddRange(bikes);

                }
                else
                {
                    throw new ArgumentNullException(nameof(bikes));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new bikes range {ex}");
                throw;
            }
        }

        public void UpdateBike(Bike bike)
        {
            try
            {
                if (bike == null)
                {
                    throw new ArgumentNullException(nameof(bike));
                }
                _logger.LogInformation($"Try to update bike id = {bike.BikeId}");
                Update(bike);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update {ex}");
                throw;
            }
        }

        public void UpdateBikes(IEnumerable<Bike> bikes)
        {
            try
            {
                if (bikes != null && bikes.Any())
                {
                    _logger.LogInformation($"Try to update the range of bikes");
                    UpdateRange(bikes);

                }
                else
                {
                    throw new ArgumentNullException(nameof(bikes));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update range {ex}");
                throw;
            }
        }

        public void DeleteBike(Bike bike)
        {
            try
            {
                if (bike == null)
                {
                    throw new ArgumentNullException(nameof(bike));
                }
                _logger.LogInformation($"Try to delete bike id= {bike.BikeId}");
                Delete(bike);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete {ex}");
                throw;
            }
        }

        public void DeleteBikes(IEnumerable<Bike> bikes)
        {
            try
            {
                if (bikes != null && bikes.Any())
                {
                    _logger.LogInformation($"Try to delete the range of bikes");
                    DeleteRange(bikes);

                }
                else
                {
                    throw new ArgumentNullException(nameof(bikes));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete range {ex}");
                throw;
            }
        }


        //public async Task<bool> SaveChangesAsync()
        //{
        //    return await SaveChangesAsync();
        //}
        #endregion CRUD
    }
}
