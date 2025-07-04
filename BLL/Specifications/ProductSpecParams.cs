using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    public class ProductSpecParams
    {
        public const int MaxPageSize = 50;

        private int _pageSize = 6;

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public string? Sort { get; set; } 

        private string _search = string.Empty;

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }




    }
}