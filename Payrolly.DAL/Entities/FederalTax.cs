using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class FederalTax
    {
        public string Id { get; set; } = null!;

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "EIN must be valid 9 digit")]
        public string EIN { get; set; } = null!;

        public string? TaxRate { get; set; }

        public string? TaxForm { get; set; }

        #region Relationship with company table
        [ForeignKey(nameof(Company))]
        public string CompanyId { get; set; } = null!;
        public Company Company { get; set; } = null!;
        #endregion
    }
}
