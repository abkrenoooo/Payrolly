using Payrolly.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Paycheck
{
    public class PayCheckFilter : PaginationFilter
    {
        public string PayScheduleId { get; set; }
        public DateTime GrossPayDate { get; set; }
    }
}
