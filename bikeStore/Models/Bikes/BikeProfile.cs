using AutoMapper;
using bikeStore.Data.Entities;
using bikeStore.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Bikes
{
    public class BikeProfile : Profile
    {
        public BikeProfile()
        {
            CreateMap<Bike, BikeDTO>()
                .ForMember(b => b.BId, ex => ex.MapFrom(x => x.BId))
                .ForMember(b => b.BModel, ex => ex.MapFrom(x => x.BModel))
                .ForMember(b => b.BBrand, ex => ex.MapFrom(x => x.BBrand))
                .ForMember(b => b.BPrice, ex => ex.MapFrom(x => x.BPrice))
                .ForMember(b => b.BThumbImgContent, ex => ex.MapFrom(x => Convert.ToBase64String(x.BThumbImgContent)))
                .ForMember(b => b.IsInStock, ex => ex.MapFrom(x => x.IsInStock))
                .ForMember(b => b.BCategoryId, ex => ex.MapFrom(x => x.BCategoryId))
                .ForMember(b => b.BImgId, ex => ex.MapFrom(x => x.BImgId))
                .ForMember(b => b.BColors, ex => ex.MapFrom(x => x.BikeJunctions.Select( z => new IdValue { Id = (long)z.BJColor, Value = z.BJColor.GetDescription() }).ToArray()))
                .ForMember(b => b.BSizes, ex => ex.MapFrom(x => x.BikeJunctions.Select(z => new IdValue { Id = (long)z.BJSize, Value = z.BJSize.ToString() }).ToArray()));
        }
    }
}
