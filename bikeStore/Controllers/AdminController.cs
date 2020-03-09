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
        private readonly IBikeRepository _bikeRepository;
        private readonly ILogger<AdminController> _logger;
        private readonly IRepo<Color> _colorRepository;
        private readonly IRepo<Size> _sizeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISpecificationRepository _specificaionRepository;
        private readonly IRepo<SpecificationCategory> _specificationCategoryRepository;
        private readonly IMapper _mapper;

        public AdminController(IBikeRepository bikeRepository, 
                               ILogger<AdminController> logger,
                               IRepo<Size> sizeRepository,
                               IRepo<Color> colorRepository,
                               ICategoryRepository categoryRepository,
                               ISpecificationRepository specificaionRepository,
                               IRepo<SpecificationCategory> specificationCategoryRepository,
                               IMapper mapper)
        {
            _bikeRepository = bikeRepository ?? throw new ArgumentNullException(nameof(bikeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _colorRepository = colorRepository ?? throw new ArgumentNullException(nameof(colorRepository));
            _sizeRepository = sizeRepository ?? throw new ArgumentNullException(nameof(sizeRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _specificaionRepository = specificaionRepository ?? throw new ArgumentNullException(nameof(specificaionRepository));
            _specificationCategoryRepository = specificationCategoryRepository ?? throw new ArgumentNullException(nameof(specificationCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetBikesLis()
        {
            var bikes = await _bikeRepository.GetBikesAsync();
            if (bikes.Any())
                return Ok(bikes);
            else return NotFound();
        }

        [HttpGet]
        [Route("color")]
        public async Task<IActionResult> GetColorList()
        {
            try
            {
                var colors = await _colorRepository.GetAllAsync();
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
                var sizes = await _sizeRepository.GetAllAsync();
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
                var categories = await _categoryRepository.GetCategoriesByConditionAsync(x => x.MainCatId == null);
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
                var categories = await _categoryRepository.GetCategoriesByConditionAsync(x => x.MainCatId == catId);
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
                var categories = await _specificationCategoryRepository.GetRangeByConditionAsync( x => x.IsSpecCatActive);
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
                var specifications = await _specificaionRepository.GetSpecificationsByCategory(catId);
                if (specifications.Any())
                    return Ok(_mapper.Map<IEnumerable<Specification>, IEnumerable<SpecificationDTO>>(specifications));
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}