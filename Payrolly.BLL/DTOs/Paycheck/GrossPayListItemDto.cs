using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Paycheck
{
    public class GrossPayListItemDto
    {
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string GrossPayId { get; set; } = null!; 
        public string PayScheduleId { get; set; } = null!; 
        public bool Payment { get; set; }
        public double RegularPay { get; set; }
        public double OverTime { get; set; }
        public double HolidayPay { get; set; }
        public decimal Bonus { get; set; }
        public decimal Commission { get; set; }
        public decimal EmpTax { get; set; }
        public double TotalHours { get; set; }
        public decimal PayRate { get; set; }
        public decimal NetPay { get; set; }

    }
}
