using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.Filters;
using Payrolly.BLL.IServices;
using System.Security.Claims;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region POST Actions

        [HttpPost("add")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _employeeService.CreateEmployeeAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedEmployeesAsync([FromBody] PaginationEmployeeFilter pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var compnayId = User!.FindFirst("cid")!.Value;

            var response = await _employeeService.GetPaginatedEmployees(pagination, compnayId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        #endregion

        #region GET and DELETE Actions 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(string id)
        {
            var response = await _employeeService.GetEmployeeAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(string id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        #endregion

        #region PATCH Actions

        [HttpPatch("update-personal")]
        public async Task<IActionResult> UpdatePersonalInfoAsync([FromBody] UpdatePersonalInfoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _employeeService.UpdatePersonalInfoAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPatch("update-payment-method")]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] UpdatePaymentMethodDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _employeeService.UpdatePaymentMethodAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPatch("update-pay-type")]
        public async Task<IActionResult> UpdatePayTypeAsync([FromBody] UpdatePayTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _employeeService.UpdatePayTypeAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPatch("update-employment-details")]
        public async Task<IActionResult> UpdateEmploymentDetailsAsync([FromBody] EmploymentDetailsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _employeeService.UpdateEmploymentDetailsAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion

        #region employment-details
        [HttpGet("{id}/employment-details")]
        public async Task<IActionResult> GetEmploymentDetailsAsync(string id)
        {
            var response = await _employeeService.GetEmploymentDetailsAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion

    }
}
