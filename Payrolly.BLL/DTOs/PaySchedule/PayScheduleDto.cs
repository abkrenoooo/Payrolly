using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.PaySchedul
{
    public class PayScheduleDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public bool Payment { get; set; }
        public string PayScheduleId { get; set; } = null!;
        public PayFrequencyTypes? PayFrequencyTypes { get; set; }
        public string? PayFrequencyTypesName { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextPayPeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextPayDay { get; set; }
        public string? CompanyId { get; set; }

    }
}
