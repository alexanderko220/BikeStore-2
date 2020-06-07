using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bikeStore.Data.Entities;

namespace BikeStore.Models.Admin
{
    public class FileImgProfile: Profile
    {
        public FileImgProfile()
        {
            CreateMap<ImgContent, FileDTO>()

                .ForMember(c => c.FileId, ex => ex.MapFrom(x => x.ImgContentId))
                .ForMember(c => c.FileName, ex => ex.MapFrom(x => x.ImgContentName))
                .ForMember(c => c.FileType, ex => ex.MapFrom(x => x.ImgContentMimeType))
                .ForMember(c => c.FileCreateDt, ex => ex.MapFrom(x => x.ImgCreateDt))
                .ForMember(c => c.Base64String, ex => ex.MapFrom(x => Convert.ToBase64String(x.Content)))
                .ForMember(c => c.IsThumbnail, ex => ex.MapFrom(x => x.IsThumbnail))
                ;

        }
    }
}
