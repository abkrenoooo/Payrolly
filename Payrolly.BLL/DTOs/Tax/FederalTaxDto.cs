using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Tax
{
    public class FederalTaxDto
    {
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "EIN must be valid 9 digit")]
        public string? EIN { get; set; }

        public string? TaxRate { get; set; }

        public string? TaxForm { get; set; }
    }
}
