using Payrolly.BLL.DTOs.TaxWithholding;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class TaxWithholdingMapping
    {
        public static TaxWithholding CreateNewTaxWithholding(this TaxWithholdingDto dto)
             => new()
             {
                 FederalWithholdingId = Guid.NewGuid().ToString(),
                 FilingStatusFederal = dto.FilingStatusFederal,
                 FederalCheck = dto.FederalCheck,
                 ClaimedDependent = dto.ClaimedDependent,
                 OtherIncome = dto.OtherIncome,
                 Deducations = dto.Deducations,
                 ExtraWithholding = dto.ExtraWithholding,
                 FilingStatusState = dto.FilingStatusState,
                 WithholdingAllowance = dto.WithholdingAllowance,
                 AdditionAmount = dto.AdditionAmount,
                 FUTA = dto.FUTA,
                 SocialSecurity = dto.SocialSecurity,
                 CASUIAndETT = dto.CASUIAndETT,
                 CASDI = dto.CASDI,
                 EmployeeId = dto.EmployeeId,
             };

        public static TaxWithholding ToTaxWithholding(this TaxWithholdingDto dto)
        {
            return new()
            {
                FederalWithholdingId = dto.FederalWithholdingId,
                FilingStatusFederal = dto.FilingStatusFederal,
                FederalCheck = dto.FederalCheck,
                ClaimedDependent = dto.ClaimedDependent,
                OtherIncome = dto.OtherIncome,
                Deducations = dto.Deducations,
                ExtraWithholding = dto.ExtraWithholding,
                FilingStatusState = dto.FilingStatusState,
                WithholdingAllowance = dto.WithholdingAllowance,
                AdditionAmount = dto.AdditionAmount,
                FUTA = dto.FUTA,
                SocialSecurity = dto.SocialSecurity,
                CASUIAndETT = dto.CASUIAndETT,
                CASDI = dto.CASDI,
                EmployeeId=dto.EmployeeId,
            };
        }
        public static TaxWithholdingDto ToTaxWithholdingDto(this TaxWithholding taxWithholding)
        {
            return new()
            {
                FederalWithholdingId = taxWithholding.FederalWithholdingId,
                FilingStatusFederal = taxWithholding.FilingStatusFederal,
                FederalCheck = taxWithholding.FederalCheck,
                ClaimedDependent = taxWithholding.ClaimedDependent,
                OtherIncome = taxWithholding.OtherIncome,
                Deducations = taxWithholding.Deducations,
                ExtraWithholding = taxWithholding.ExtraWithholding,
                FilingStatusState = taxWithholding.FilingStatusState,
                WithholdingAllowance = taxWithholding.WithholdingAllowance,
                AdditionAmount = taxWithholding.AdditionAmount,
                FUTA = taxWithholding.FUTA,
                SocialSecurity = taxWithholding.SocialSecurity,
                CASUIAndETT = taxWithholding.CASUIAndETT,
                CASDI = taxWithholding.CASDI,
                EmployeeId = taxWithholding.EmployeeId,
            };
        }
    }
}
