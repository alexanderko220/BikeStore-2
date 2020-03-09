using AutoMapper;
using bikeStore.Data.Entities;


namespace BikeStore.Models.Dictionaries
{
    public class CategoryDictionaryProfile: Profile
    {
        public CategoryDictionaryProfile()
        {
            CreateMap<Category, IdValue>()
              .ForMember(c => c.Id, ex => ex.MapFrom(x => x.CatId as object))
              .ForMember(c => c.Value, ex => ex.MapFrom(x => x.CatName))
              ;
        }
    }
}
