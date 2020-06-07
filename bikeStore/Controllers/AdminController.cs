using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bikeStore.Data.Entities;
using bikeStore.Data.Repository;
using BikeStore.Data.Entities;
using BikeStore.Data.Repository;
using BikeStore.Models;
using BikeStore.Models.Admin;
using BikeStore.Models.Bikes;
using BikeStore.Models.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeStore.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBikeRepository _bikeRepo;
        private readonly ILogger<AdminController> _logger;
        private readonly IRepo<Color> _colorRepo;
        private readonly IRepo<Size> _sizeRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISpecificationRepository _specificaionRepo;
        private readonly IRepo<SpecificationCategory> _specificationCategoryRepo;
        private readonly IRepo<ImgContent> _imgRepo;
        private readonly IMapper _mapper;

        public AdminController(IBikeRepository bikeRepository, 
                               ILogger<AdminController> logger,
                               IRepo<Size> sizeRepository,
                               IRepo<Color> colorRepository,
                               ICategoryRepository categoryRepository,
                               ISpecificationRepository specificaionRepository,
                               IRepo<SpecificationCategory> specificationCategoryRepository,
                               IRepo<ImgContent> imgRepo,
                               IMapper mapper)
        {
            _bikeRepo = bikeRepository ?? throw new ArgumentNullException(nameof(bikeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _colorRepo = colorRepository ?? throw new ArgumentNullException(nameof(colorRepository));
            _sizeRepo = sizeRepository ?? throw new ArgumentNullException(nameof(sizeRepository));
            _categoryRepo = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _specificaionRepo = specificaionRepository ?? throw new ArgumentNullException(nameof(specificaionRepository));
            _specificationCategoryRepo = specificationCategoryRepository ?? throw new ArgumentNullException(nameof(specificationCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _imgRepo = imgRepo ?? throw new ArgumentNullException(nameof(imgRepo));
        }

        [HttpGet]
        public async Task<IActionResult> GetBikesLis()
        {
            try
            {
                var bikes = await _bikeRepo.GetBikesAsync();
             
                if (bikes.Any())
                    return Ok(_mapper.Map<IEnumerable<Bike>, IEnumerable<BikeDTO>>(bikes));
                else return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception( $"{e.Message}");
            }

        }

        [HttpGet]
        [Route("img/{storeImgId}")]
        public async Task<IActionResult> GetBikeImages( long storeImgId)
        {
            try
            {
                var images = await _imgRepo.GetRangeByConditionAsync(x => x.StoreImgId == storeImgId);
                if (images.Any()) return Ok(_mapper.Map<IEnumerable<ImgContent>, IEnumerable<FileDTO>>(images));

                return  NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("color")]
        public async Task<IActionResult> GetColorList()
        {
            try
            {
                var colors = await _colorRepo.GetAllAsync();
                if (colors.Any())
                    return Ok(_mapper.Map<IEnumerable<Color>, IEnumerable<IdValue>>(colors));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("size")]
        public async Task<IActionResult> GetSizeList()
        {
            try
            {
                var sizes = await _sizeRepo.GetAllAsync();
                if (sizes.Any())
                    return Ok(_mapper.Map<IEnumerable<Size>, IEnumerable<IdValue>>(sizes));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetMainCategoryList()
        {
            try
            {
                var categories = await _categoryRepo.GetCategoriesByConditionAsync(x => x.MainCatId == null);
                if (categories.Any())
                    return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<IdValue>>(categories));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Route("category/{catId}")]
        public async Task<IActionResult> GetCategoryByIdList(long catId)
        {
            try
            {
                var categories = await _categoryRepo.GetCategoriesByConditionAsync(x => x.MainCatId == catId);
                if (categories.Any())
                    return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<IdValue>>(categories));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

        [HttpGet]
        [Route("specifications/category")]
        public async Task<IActionResult> GetSpecificationsCategoryList()
        {
            try
            {
                var categories = await _specificationCategoryRepo.GetRangeByConditionAsync( x => x.IsSpecCatActive);
                if (categories.Any())
                    return Ok(_mapper.Map<IEnumerable<SpecificationCategory>, IEnumerable<IdValue>>(categories));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Route("specifications/{catId}")]
        public async Task<IActionResult> GetSpecifications(long catId)
        {
            try
            {
                var specs = await _specificaionRepo.GetSpecificationsByCategory(catId);
                if (specs.Any())
                    return Ok(_mapper.Map<IEnumerable<Specification>, IEnumerable<SpecificationDTO>>(specs));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("img/{id}")]
        public async Task<IActionResult> DeleteImage(long id)
        {
            try
            {
                var img = await _imgRepo.GetByConditionAsync( i => i.ImgContentId == id);

                if(img == null) return NotFound();

                _imgRepo.Delete(img);
                await _imgRepo.SaveChangesAsync().ConfigureAwait(false);
                return Ok();

            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }
    }
}