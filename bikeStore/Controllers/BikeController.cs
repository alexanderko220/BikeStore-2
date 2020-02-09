using AutoMapper;
using bikeStore.Data.Entities;
using bikeStore.Data.Repository;
using BikeStore.Models.Bikes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bikeStore.Controllers
{
    [ApiController]
    [Route("api/bikes")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly ILogger<BikeController> _logger;
        private readonly IMapper _mapper;

        public BikeController(IBikeRepository bikeRepository, ILogger<BikeController> logger, IMapper mapper)
        {
            _bikeRepository = bikeRepository ?? throw new ArgumentNullException(nameof(_bikeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        [Route("category/{catId}")]
        public async Task<IActionResult> GetBikesByCategory(long catId)
        {
            var bikes = await _bikeRepository.GetBikesByCategoryAsync(catId);
            if (bikes != null) 
                return Ok(_mapper.Map<IEnumerable<Bike>, IEnumerable<BikeDTO>>(bikes));
            else return NotFound();
        }

        [HttpGet]
        [Route("bike/{bikeId}")]
        public async Task<IActionResult> GetBike(long bikeId)
        {
            var bike = await _bikeRepository.GetBikeAsync(bikeId);
            if (bike != null)
                return Ok(bike);
            else return NotFound();
        }
    }
}
