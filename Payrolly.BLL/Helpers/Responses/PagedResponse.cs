using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Helpers.Responses
{
    public class PagedResponse<T> : GenericResponse<T> where T : class
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; } = 0;

        public PagedResponse()
        {
            PageNumber = 1;
            PageSize = 10;
            TotalRecords = 0;
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }
    }
}
