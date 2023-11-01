using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.Location;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface IPayScheduleService
    {
        Task<GenericResponse<PayScheduleDto>> CreatePayScheduleAsync(PayScheduleDto dto, string companyId);
        Task<GenericResponse<PayScheduleDto>> GetPayScheduleAsync(string name);
        Task<GenericResponse<List<PayScheduleDto>>> GetAllPayschedulesAsync(string companyId);
        Task<GenericResponse<List<string>>> GetPayPeriodAsync(string companyId,string PayScheduleId);

    }
}
