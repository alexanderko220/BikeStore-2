using AutoMapper;
using BikeStore.Data.Entities;


namespace BikeStore.Models.Dictionaries
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, IdValue>()
                .ForMember( c => c.Id, ex => ex.MapFrom(x => x.ColorId as object))
                .ForMember( c => c.Value, ex => ex.MapFrom( x => x.ColorName))
                ;
        }
    }

}
