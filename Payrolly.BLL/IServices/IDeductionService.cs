using Payrolly.BLL.DTOs.Deduction;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface IDeductionService
    {
        Task<GenericResponse<DeductionDto>> CreateDeductionAsync(DeductionDto dto);
        Task<GenericResponse<object>> DeleteDeductionAsync(string id);
        Task<GenericResponse<DeductionDto>> GetDeductionAsync(string id);
        Task<GenericResponse<DeductionDto>> CreateEmployeeDeductionAsync(DeductionDto dto, string? employeeId = null);
    }
}
