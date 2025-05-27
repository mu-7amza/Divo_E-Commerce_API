using AutoMapper;
using DAL.Entities;
using PL.Divo.Dtos;

namespace PL.Divo.Helpers
{
    public class ProductUrlResolver(IConfiguration config) : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config = config;

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
