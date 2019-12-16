using AutoMapper;
using BikeStore.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bikeStore.Data.Entities;
using BikeStore.Models.Categories;

namespace BikeStore.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository category, ILogger<CategoryController> logger, IMapper mapper)
        {
            _categoryRepository = category ?? throw new ArgumentNullException(nameof(category));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetCategoriesAsync();

            if(categories != null) return Ok(_mapper.Map<IEnumerable<Category>,IEnumerable<CategoryDTO>>(categories));
            return NotFound();
        }
    }
}
