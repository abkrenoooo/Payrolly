using Payrolly.BLL.DTOs.Paycheck;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface IPayCheckService
    {
        Task<PagedResponse<IEnumerable<CalculateGrossPayDto>>> GetPaginatedGrossPayAsync(PayCheckFilter payCheckFilter,string CompanyId);
        Task<GenericResponse<IEnumerable<GrossPayListItemDto>>> GetGrossPayItemsAsync(string companyId);
        Task<GenericResponse<List<CalculateGrossPayDto>>> CalculateGrossPayForEmployeeAsync(string companyId,  List<CalculateGrossPayDto> dto);
        Task<GenericResponse<List<GrossPayListItemDto>>> GetTotalRecordForAllPayChecks(string companyId, string PayScheduleId, DateTime NextPayDay);
    }
}
