using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.Location;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.BLL.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface ILocationService
    {
        Task<GenericResponse<LocationDto>> CreateLocationAsync(LocationDto dto, string CompanyId);
        Task<GenericResponse<IEnumerable<string>>> GetAllLocationsAsync(string companyId);
        Task<GenericResponse<LocationDto>> GetLocationAsync(string id);
    }
}
