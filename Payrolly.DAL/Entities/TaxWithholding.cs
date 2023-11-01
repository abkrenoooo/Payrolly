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
    public class TaxWithholding
    {
        #region Fedrale Withholding
        [Key]
        public string FederalWithholdingId { get; set; }
        public FilingStatusFederal FilingStatusFederal { get; set; }
        public bool FederalCheck { get; set; }
        public decimal ClaimedDependent { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal Deducations { get; set; }
        public decimal ExtraWithholding { get; set; }
        #endregion

        #region State Withholding
        public FilingStatusState FilingStatusState { get; set; }
        public int WithholdingAllowance { get; set; }
        public decimal AdditionAmount { get; set; }
        #endregion
        
        #region State Withholding
        public bool FUTA { get; set; }
        public bool SocialSecurity { get; set; }
        public bool CASUIAndETT { get; set; }
        public bool CASDI { get; set; }
        #endregion

        public string? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }
    }
}
