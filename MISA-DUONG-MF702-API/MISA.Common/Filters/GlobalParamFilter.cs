using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Filters
{
    public class GlobalParamFilter
    {
        public GlobalParamFilter()
        {
            PageIndex = 1;
            PageSize = 10;
            SortBy = "EmployeeId";
            OrderBy = "DESC";
        }

        public string SortBy { get; set; }
        public string OrderBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int? Status { get; set; }
        public int? WorkState { get; set; }
        public int? Gender { get; set; }

        public string Language { get; set; }
    }
}
