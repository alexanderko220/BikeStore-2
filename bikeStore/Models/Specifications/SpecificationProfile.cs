using AutoMapper;
using bikeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Specifications
{
    public class SpecificationProfile: Profile
    {
        public SpecificationProfile()
        {
            CreateMap<Specification, SpecificationDTO>()
                .ForMember(b => b.SpecId, ex => ex.MapFrom(x => x.SpecId))
                .ForMember(b => b.Type, ex => ex.MapFrom(x => x.Type))
                .ForMember(b => b.Model, ex => ex.MapFrom(x => x.Model))
                .ForMember(b => b.Brand, ex => ex.MapFrom(x => x.Brand))
                .ForMember(b => b.Description, ex => ex.MapFrom(x => x.Description))
                ;
        }
    }
}
