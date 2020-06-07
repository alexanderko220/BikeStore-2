using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bikeStore.Data.Entities;
using BikeStore.Data.Entities;

namespace BikeStore.Models.Bikes
{
    public class BikeForCreationProfile : Profile
    {
        public BikeForCreationProfile()
        {
            CreateMap<BikeForCreation, Bike > ()
                .ForMember(b => b.BikeId, ex => ex.MapFrom(x => x.BikeId))
                .ForMember(b => b.Brand, ex => ex.MapFrom(x => x.Brand))
                .ForMember(b => b.CategoryId, ex => ex.MapFrom(x => x.CategoryId))
                .ForMember(b => b.Price, ex => ex.MapFrom(x => x.Price))
                .ForMember(b => b.Model, ex => ex.MapFrom(x => x.Model))
                .ForMember(b => b.IsInStock, ex => ex.MapFrom(x => x.IsInStock))
                .ForMember(b => b.ImgId, ex => ex.MapFrom(x => x.ImgId))
                .ForMember(b => b.ThumbBase64, ex => ex.MapFrom(x => x.ThumbBase64))
                .ForMember(b => b.Colors, ex => ex.MapFrom(x =>
                    x.JunkColors.Select(c => new BikesColors() {Id = c.Id, BikeId = c.BikeId, ColorId = c.ColorId}).ToList()
                ))
                .ForMember(b => b.Sizes, ex => ex.MapFrom(x => 
                    x.JunkSizes.Select(c => new BikesSizes() { Id = c.Id, BikeId = c.BikeId, SizeId = c.SizeId }).ToList()))
                ;
        }
    }
}
