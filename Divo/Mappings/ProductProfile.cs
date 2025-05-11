using AutoMapper;
using PL.Divo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DAL.Entities.Category, PL.Divo.Dtos.CategoryDto>().ReverseMap();

            CreateMap<DAL.Entities.Product, PL.Divo.Dtos.ProductToReturnDto>()
                .ForMember(d => d.Brand,o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl , o => o.MapFrom<ProductUrlResolver>());

            CreateMap<DAL.Entities.Product, PL.Divo.Dtos.ProductToSendDto>();

            CreateMap<DAL.Entities.Brand, PL.Divo.Dtos.BrandDto>().ReverseMap();

        }
    }
}
