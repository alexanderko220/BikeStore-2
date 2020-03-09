using AutoMapper;
using bikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Dictionaries
{
    public class SpecificationCategoryProfile : Profile
    {
        public SpecificationCategoryProfile()
        {
            CreateMap<SpecificationCategory, IdValue>()
               .ForMember(c => c.Id, ex => ex.MapFrom(x => x.SpecCatId as object))
               .ForMember(c => c.Value, ex => ex.MapFrom(x => x.SpecCatName))
               ;
        }
    }
}
