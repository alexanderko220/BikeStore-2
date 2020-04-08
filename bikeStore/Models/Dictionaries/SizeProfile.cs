using AutoMapper;
using BikeStore.Data.Entities;


namespace BikeStore.Models.Dictionaries
{
    public class SizeProfile : Profile
    {
        public SizeProfile()
        {
            CreateMap<Size, IdValue>()
               .ForMember(c => c.Id, ex => ex.MapFrom(x => x.SizeId as object))
               .ForMember(c => c.Value, ex => ex.MapFrom(x => x.SizeName))
               ;
        }
    }
}
