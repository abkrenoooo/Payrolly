using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class GrossPay
    {
        public string Id { get; set; }
        public double RegularPay { get; set; }
        public double OverTime { get; set; } 
        public double HolidayPay { get; set; } 
        public decimal Bonus { get; set; }
        public decimal Commision { get; set; }
        public decimal PayRate { get; set; }
        public decimal EmpTax { get; set; }
        public string? CheckNum { get; set; }
        public bool Payment { get; set; }
        public string? PayScheduleId { get; set; }
        public DateTime GrossPayDate { get; set; }
        [ForeignKey(nameof(PayScheduleId))]
        public PaySchedule? PaySchedule { get; set; }  
        public string? EmployeeId { get; set; } 

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
        public string? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
    }
}
