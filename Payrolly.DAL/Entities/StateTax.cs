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
    public class StateTax
    {
        public string Id { get; set; } = null!;

        [Required]
        public StateTaxType StateTaxType { get; set; }

        public string StateName { get; set; } = null!;

        [StringLength(maximumLength: 12)]
        public string EmployerAccountNumber { get; set; } = null!;

        public decimal UnEmploymentInsuranceRate { get; set; }

        public decimal TrainingTaxRate { get; set; }

        #region Relationship with company table
        [ForeignKey(nameof(Company))]
        public string CompanyId { get; set; } = null!;
        public Company Company { get; set; } = null!;
        #endregion
    }
}
