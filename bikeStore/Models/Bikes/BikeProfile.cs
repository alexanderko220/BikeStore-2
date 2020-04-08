using AutoMapper;
using bikeStore.Data.Entities;
using System;
using System.Globalization;
using System.Linq;

namespace BikeStore.Models.Bikes
{
    public class BikeProfile : Profile
    {
        string specifier = "N";
        CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-Uk");

        public BikeProfile()
        {
            CreateMap<Bike, BikeDTO>()
                .ForMember(b => b.BikeId, ex => ex.MapFrom(x => x.BikeId))
                .ForMember(b => b.Model, ex => ex.MapFrom(x => x.Model))
                .ForMember(b => b.Brand, ex => ex.MapFrom(x => x.Brand))
                .ForMember(b => b.Price, ex => ex.MapFrom(x => x.Price.HasValue ? x.Price.Value.ToString(specifier, culture) : x.Price.ToString()))
                .ForMember(b => b.ThumbBase64, ex => ex.MapFrom(x => x.ThumbBase64))
                .ForMember(b => b.IsInStock, ex => ex.MapFrom(x => x.IsInStock))
                .ForMember(b => b.CategoryId, ex => ex.MapFrom(x => x.CategoryId))
                .ForMember(b => b.MainCategoryId, ex => ex.MapFrom(x => x.Category.MainCatId))
                .ForMember(b => b.ImgId, ex => ex.MapFrom(x => x.ImgId))
                .ForMember(b => b.Colors, ex => ex.MapFrom( x => x.Colors.Any() ? x.Colors.Select(c => c.Color.ColorId).ToArray(): null ))
                .ForMember(b => b.Sizes, ex => ex.MapFrom(x => x.Sizes.Any() ? x.Sizes.Select(c => c.Size.SizeId).ToArray() : null))
                ;
        }

    }
}
