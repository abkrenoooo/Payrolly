using Payrolly.BLL.DTOs.TaxWithholding;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface ITaxWithholdingServices
    {
        Task<GenericResponse<TaxWithholdingDto>> CreateTaxWithholdingAsync(TaxWithholdingDto dto, string companyId);
        Task<GenericResponse<TaxWithholdingDto>> GetTaxWithholdingAsync(string id);
        Task<GenericResponse<TaxWithholding>> UpdateTaxWithholdingAsync(TaxWithholdingDto dto);


    }
}
