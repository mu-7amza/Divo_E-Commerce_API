using BLL.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    public class ProductsWithCategoriesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithCategoriesAndBrandsSpecification()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
        }

        public ProductsWithCategoriesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
        }

    }
}
