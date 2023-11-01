using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Tax
{
    public class StateTaxDto
    {
        public string StateName { get; set; } = null!;

        [MaxLength(12)]
        public string? EmployerAccountNumber { get; set; }

        public decimal UnEmploymentInsuranceRate { get; set; }

        public decimal TrainingTaxRate { get; set; }
    }
}
