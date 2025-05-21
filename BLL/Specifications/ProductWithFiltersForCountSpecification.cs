using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Repositories;
using DAL.Entities;

namespace BLL.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId))
        {
            
        }
        
    }
}