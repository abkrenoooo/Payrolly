using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Paycheck
{
    public class GetGrossPayDto
    {
        public double TotalHours { get; set; }
        public decimal GrossPay { get; set; }
    }
}
