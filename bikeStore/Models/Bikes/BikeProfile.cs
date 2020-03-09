using AutoMapper;
using bikeStore.Data.Entities;
using System;


namespace BikeStore.Models.Bikes
{
    public class BikeProfile : Profile
    {
        public BikeProfile()
        {
            CreateMap<Bike, BikeDTO>()
                .ForMember(b => b.BId, ex => ex.MapFrom(x => x.BikeId))
                .ForMember(b => b.BModel, ex => ex.MapFrom(x => x.Model))
                .ForMember(b => b.BBrand, ex => ex.MapFrom(x => x.Brand))
                .ForMember(b => b.BPrice, ex => ex.MapFrom(x => x.Price))
                .ForMember(b => b.BThumbImgContent, ex => ex.MapFrom(x => Convert.ToBase64String(x.ThumbImgContent)))
                .ForMember(b => b.IsInStock, ex => ex.MapFrom(x => x.IsInStock))
                .ForMember(b => b.BCategoryId, ex => ex.MapFrom(x => x.CategoryId))
                .ForMember(b => b.BImgId, ex => ex.MapFrom(x => x.ImgId));
        }
    }
}
