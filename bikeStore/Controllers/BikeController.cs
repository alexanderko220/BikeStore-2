using AutoMapper;
using bikeStore.Data;
using bikeStore.Data.Entities;
using bikeStore.Data.Repository;
using BikeStore.Data.Entities;
using BikeStore.Models.Bikes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace bikeStore.Controllers
{
    [ApiController]
    [EnableCors("ApiCorsPolicy")]
    [Route("api/bikes")]
    public class BikeController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IBikeRepository _bikeRepository;
        private readonly ILogger<BikeController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepo<BikesColors> _bikesColorsRepo;
        private readonly IRepo<BikesSizes> _bikesSizesRepo;
        private readonly IRepo<StoreImages> _storeImgRepo;
        private readonly IRepo<ImgContent> _imgContentRepo;

        public BikeController( StoreDbContext context,
                              IBikeRepository bikeRepository, 
                              ILogger<BikeController> logger, 
                              IMapper mapper,
                              IRepo<BikesColors> bikesColorsRepo,
                              IRepo<BikesSizes> bikesSizesRepo,
                              IRepo<StoreImages> storeImgRepo,
                              IRepo<ImgContent> imgContentRepo
                              )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _bikeRepository = bikeRepository ?? throw new ArgumentNullException(nameof(bikeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bikesColorsRepo = bikesColorsRepo ?? throw new ArgumentNullException(nameof(bikesColorsRepo));
            _bikesSizesRepo = bikesSizesRepo ?? throw new ArgumentNullException(nameof(bikesSizesRepo));
            _storeImgRepo = storeImgRepo ?? throw new ArgumentNullException(nameof(storeImgRepo));
            _imgContentRepo = imgContentRepo ?? throw new ArgumentNullException(nameof(imgContentRepo));
        }

        [HttpGet]
        [Route("category/{catId}")]
        public async Task<IActionResult> GetBikesByCategory(long catId)
        {
            var bikes = await _bikeRepository.GetBikesByCategoryAsync(catId);
            if (bikes != null) return Ok(_mapper.Map<IEnumerable<Bike>, IEnumerable<BikeDTO>>(bikes));
             return NotFound();
        }

        [HttpGet]
        [Route("bike/{bikeId}")]
        public async Task<IActionResult> GetBike(long bikeId)
        {
            var bike = await _bikeRepository.GetBikeAsync(bikeId);
            if (bike != null)
                return Ok(bike);
            return NotFound();
        }

       
        [HttpPost]
        [Route("bike")]
        public async Task<IActionResult> CreateBike([FromForm] BikeMultiPartDTO model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    { 
                        StoreImages storeImage = new StoreImages();
                        storeImage.Description = $"Bike '{model.Bike.Brand} - {model.Bike.Model}'  image files";
                        _storeImgRepo.Add(storeImage);
                        await _storeImgRepo.SaveChangesAsync();

                        if (model.Files.Any())
                        {
                            // save bike images
                            foreach (var file in model.Files)
                            {

                                if (file.Length > 0)
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        file.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        _imgContentRepo.Add(new ImgContent
                                        {
                                            ImgContentMimeType = file.ContentType,
                                            StoreImgId = storeImage.StoreImgId,
                                            ImgCreateDt = DateTime.UtcNow,
                                            ImgContentName = file.FileName,
                                            Content = fileBytes,
                                            IsThumbnail = file.FileName.Equals(model.Bike.ThumbFileName)
                                        });
                                        await _imgContentRepo.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                       

                        Bike bike = _mapper.Map<BikeForCreation, Bike> (model.Bike);
                        bike.ImgId = storeImage.StoreImgId;
                        //create new bike
                        _bikeRepository.CreateBike(bike);
                        await _bikeRepository.SaveChangesAsync();

                        // add junction with colors
                        _bikesColorsRepo.AddRange(model.Bike.Colors.Select(colorId => new BikesColors() { BikeId = bike.BikeId, ColorId = colorId }).ToList());
                        await _bikesColorsRepo.SaveChangesAsync();
                        // add junction with sizes
                        _bikesSizesRepo.AddRange(model.Bike.Sizes.Select(sizeId => new BikesSizes() { BikeId = bike.BikeId, SizeId = sizeId}).ToList());
                        await _bikesSizesRepo.SaveChangesAsync();

                        transaction.Commit();
                        return Ok();
                    }

                    return BadRequest();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

      
        [HttpPut]
        [Route("bike")]
        public async Task<IActionResult> UpdateBike([FromForm] BikeMultiPartDTO model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        // update bike
                        Bike bike = _mapper.Map<BikeForCreation, Bike>(model.Bike);
                        _bikeRepository.UpdateBike(bike);
                        await _bikeRepository.SaveChangesAsync();

                        //get exist storeImages list
                        var storeImg = await _imgContentRepo.GetRangeByConditionAsync(i => i.StoreImgId == model.Bike.ImgId);
                        var tempImgContent = new List<ImgContent>();
                        if (model.Files.Any())
                        {
                            foreach (var file in model.Files)
                            {
                                if (file.Length > 0)
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        file.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        tempImgContent.Add(new ImgContent
                                        {
                                            ImgContentMimeType = file.ContentType,
                                            StoreImgId = model.Bike.ImgId ?? default,
                                            ImgCreateDt = DateTime.UtcNow,
                                            ImgContentName = file.FileName,
                                            Content = fileBytes,
                                            IsThumbnail = file.FileName.Equals(model.Bike.ThumbFileName)
                                        });

                                    }
                                }
                            }
                        }
                            
                        //assign new list of images, all existing images will be replaced
                        _imgContentRepo.DeleteRange(storeImg);
                        _imgContentRepo.AddRange(tempImgContent);
                        //storeImg.ImgContents = tempImgContent;
                        await _imgContentRepo.SaveChangesAsync();

                        // delete all exist  and create new colors junction
                        var existBikesColors = await _bikesColorsRepo.GetRangeByConditionAsync(c => c.BikeId == bike.BikeId);
                        _bikesColorsRepo.DeleteRange(existBikesColors);
                        await _bikesColorsRepo.SaveChangesAsync();
                        _bikesColorsRepo.AddRange(model.Bike.Colors.Select(colorId => new BikesColors() { BikeId = bike.BikeId, ColorId = colorId }).ToList());
                        await _bikesColorsRepo.SaveChangesAsync();

                        // delete all exist  and create new sizes junction
                        var existBikesSizes = await _bikesSizesRepo.GetRangeByConditionAsync(s => s.BikeId == bike.BikeId);
                        _bikesSizesRepo.DeleteRange(existBikesSizes);
                        await _bikesSizesRepo.SaveChangesAsync();
                        _bikesSizesRepo.AddRange(model.Bike.Sizes.Select(sizeId => new BikesSizes() { BikeId = bike.BikeId, SizeId = sizeId }).ToList());
                        await _bikesSizesRepo.SaveChangesAsync();

                        transaction.Commit();
                        return Ok();
                    }

                    return BadRequest();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}
