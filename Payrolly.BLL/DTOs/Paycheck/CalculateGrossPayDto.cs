using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Paycheck
{
    public class CalculateGrossPayDto
    {
        public string GrossPayId { get; set; }
        public double RegularPay { get; set; }
        public double OverTime { get; set; }
        public double HolidayPay { get; set; }
        public decimal Bonus { get; set; }
        public decimal Commission { get; set; }
        public decimal EmpTax { get; set; }
        public string? CheckNum { get; set; }
        public decimal PayRate { get; set; }
        public bool Payment { get; set; }
        public string EmployeeName { get; set; }
        public DateTime GrossPayDate { get; set; }
        public string? CompanyId { get; set; }
        public string? PayScheduleId { get; set; }
        public string EmployeeId { get; set; }

    }
}
