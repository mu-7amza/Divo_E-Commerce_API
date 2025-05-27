using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divo.Helpers
{
    public class Pagination<T>(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> data) where T : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int TotalCount { get; set; } = totalCount;
        public int PageSize { get; set; } = pageSize;
        public IReadOnlyList<T> Data { get; set; } = data;
    }   
}