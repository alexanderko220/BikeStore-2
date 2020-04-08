using AutoMapper;
using bikeStore.Data;
using bikeStore.Data.Entities;
using bikeStore.Data.Extensions;
using bikeStore.Data.Repository;
using BikeStore.Data.Entities;
using BikeStore.Models;
using BikeStore.Models.Bikes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bikeStore.Controllers
{
    [ApiController]
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

        //StoreImages
        //ImgContent

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

        [HttpPost]
        [Route("bike")]
        public async Task<IActionResult> CreateBike()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    IFormFileCollection bikeImeges = Request.Form.Files;
                    Dictionary<string, string> bikeInfo = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                    Bike bike = new Bike();
                    StoreImages storeImage = new StoreImages();
                    BikesColors bikesColorSize = new BikesColors();
                    

                    if (bikeInfo.Count > 0)
                    {
                        //Save files at first
                        if (bikeImeges.Count > 0)
                        {
                            storeImage.Description = $"Bike '{bikeInfo["brand"]} - {bikeInfo["model"]}'  image files";
                            _storeImgRepo.Add(storeImage);
                            await _storeImgRepo.SaveChangesAsync();

                            foreach (var file in bikeImeges)
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
                                            Content = fileBytes
                                        });
                                        await _imgContentRepo.SaveChangesAsync();
                                    }
                                }
                            }

                        }

                        bike.Brand = bikeInfo["brand"];
                        bike.CategoryId = long.Parse(bikeInfo["subCategoryId"]);
                        bike.Model = bikeInfo["model"];
                        bike.Price = bikeInfo["price"].ToDecimal();
                        bike.IsInStock = bikeInfo["isInStock"] == "true" ? true: false;
                        bike.ThumbBase64 = bikeInfo["thumbBase64"];
                        bike.ImgId = storeImage.StoreImgId;

                        _bikeRepository.CreateBike(bike);
                        await _bikeRepository.SaveChangesAsync();

                        var colors = bikeInfo["colors"].Split(',').ToArray();
                        var sizes = bikeInfo["sizes"].Split(',').ToArray();

                        // add junction with colors
                        foreach (var colorId in colors)
                        {
                            _bikesColorsRepo.Add(new BikesColors() { BikeId = bike.BikeId, ColorId = long.Parse(colorId) });
                            await _bikesColorsRepo.SaveChangesAsync();
                        }
                        // add junction with sizes
                        foreach (var sizeId in sizes)
                        {
                            _bikesSizesRepo.Add(new BikesSizes() { BikeId = bike.BikeId, SizeId = long.Parse(sizeId) });
                            await _bikesSizesRepo.SaveChangesAsync();
                        }

                        transaction.Commit();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }


            
        }
    }
}
