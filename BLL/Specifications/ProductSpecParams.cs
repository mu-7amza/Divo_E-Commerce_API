using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        private string _search = string.Empty;

        public string Search
        {
            get => _search;
            set =>  _search = value.ToLower();
        } 

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public string Sort { get; set; } = string.Empty;
    }
}