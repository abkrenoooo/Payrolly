using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payrolly.DAL.Entities;

namespace Payrolly.BLL.DTOs.TaxWithholding
{
    public class TaxWithholdingDto
    {
        #region Fedrale Withholding
        public string FederalWithholdingId { get; set; } = null!;
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

    }
}
