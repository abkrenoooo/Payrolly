using Payrolly.BLL.DTOs.Tax;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface ITaxService
    {
        Task<GenericResponse<FederalTaxDto>> UpdateFederalTaxAsync(FederalTaxDto dto, string companyId);
        Task<GenericResponse<StateTaxDto>> UpdateStateTaxAsync(StateTaxDto dto, string companyId, StateTaxType stateTaxType);
        Task<GenericResponse<FederalTaxDto>> GetFederalTaxDto(string companyId);
        Task<GenericResponse<StateTaxDto>> GetStateTaxDto(string stateName, string companyId, StateTaxType stateTaxType);
    }
}
