using AutoMapper;
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
            CreateMap<DAL.Entities.Category, DAL.Dtos.CategoryDto>().ReverseMap();
            CreateMap<DAL.Entities.Product, DAL.Dtos.ProductDto>().ReverseMap();
        }
    }
}
