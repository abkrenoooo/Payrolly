using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class Company
    {
        [Key]
        public string Id { get; set; } = null!;

        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [ForeignKey(nameof(FederalTax))]
        public string? FedrialTaxId { get; set; }
        public virtual FederalTax? FederalTax { get; set; }

        public virtual List<ApplicationUser>? ApplicationUser { get; set; }

        public virtual List<Location>? Locations { get; set; }

        public virtual List<PaySchedule>? PaySchedules { get; set; }

        public virtual ICollection<StateTax>? StateTaxes { get; set; }
    }
}