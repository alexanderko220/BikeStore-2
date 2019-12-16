using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bikeStore.Data.Entities;

namespace BikeStore.Models.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(c => c.CatId, exp => exp.MapFrom(x => x.CatId))
                .ForMember(c => c.CatDescr, exp => exp.MapFrom(x => x.CatDescr))
                .ForMember(c => c.CatName, exp => exp.MapFrom(x => x.CatName))
                .ForMember(c => c.MainCatId, exp => exp.MapFrom(x => x.MainCatId));
        }
    }
}
