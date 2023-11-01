using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.Filters;
using Payrolly.BLL.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface IEmployeeService
    {
        Task<GenericResponse<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto dto, string companyId);
        Task<GenericResponse<object>> DeleteEmployeeAsync(string id);
        Task<GenericResponse<EmployeeDto>> GetEmployeeAsync(string id);
        Task<GenericResponse<EmployeeDto>> UpdatePersonalInfoAsync(UpdatePersonalInfoDto dto);
        Task<GenericResponse<EmployeeDto>> UpdatePaymentMethodAsync(UpdatePaymentMethodDto dto);
        Task<GenericResponse<EmployeeDto>> UpdatePayTypeAsync(UpdatePayTypeDto dto);
        Task<PagedResponse<IEnumerable<PagedEmployeeItemDto>>> GetPaginatedEmployees(PaginationEmployeeFilter pagination, string companyId);
        Task<GenericResponse<EmploymentDetailsDto>> GetEmploymentDetailsAsync(string id);
        Task<GenericResponse<EmploymentDetailsDto>> UpdateEmploymentDetailsAsync(EmploymentDetailsDto dto);

    }
}
