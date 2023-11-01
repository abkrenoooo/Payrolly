using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class PaySchedule
    {
        [Key]
        public string PayScheduleId { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public PayFrequencyTypes? PayFrequencyTypes { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextPayPeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextPayDay { get; set; }

        //Nevegation Property
        public virtual List<Employee>? Employees { get; set; }
        public virtual List<GrossPay>? grossPays { get; set; }

        public string? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
    }
}
