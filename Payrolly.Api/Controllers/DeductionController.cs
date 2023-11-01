using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Deduction;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Services;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DeductionController : ControllerBase
    {
        private readonly IDeductionService _deductionService;

        public DeductionController(IDeductionService deductionService)
        {
            _deductionService = deductionService;
        }

        #region POST Actions

        [HttpPost("addDeduction")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] DeductionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _deductionService.CreateDeductionAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPost("addEmployeeDeduction")]
        public async Task<IActionResult> CreateEmployeeDeductionAsync([FromBody] DeductionDto dto, string? employeeId = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _deductionService.CreateEmployeeDeductionAsync(dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        #endregion

        #region GET and DELETE Actions 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeductionAsync(string id)
        {
            var response = await _deductionService.GetDeductionAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeductionAsync(string id)
        {
            var response = await _deductionService.DeleteDeductionAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        #endregion
    }
}
