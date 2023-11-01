using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Paycheck;
using Payrolly.BLL.IServices;
using Payrolly.DAL.Entities;

namespace Payrolly.Api.Controllers
{
    [Route("api/paychecks")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PaychecksController : ControllerBase
    {
        private readonly IPayCheckService _paycheckService;

        public PaychecksController(IPayCheckService paycheckService)
        {
            _paycheckService = paycheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaychecksAsync()
        {
            var companyId = User.FindFirst("cid")!.Value;

            if (companyId == null)
                return NotFound("company not found");

            var response = await _paycheckService.GetGrossPayItemsAsync(companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("EditGrossPay")]
        public async Task<IActionResult> CalcuateGrossPayAsync([FromBody] List<CalculateGrossPayDto> dto)
        {
            var companyId = User.FindFirst("cid")!.Value;

            if (companyId == null)
                return BadRequest("Invalid company");

            var response = await _paycheckService.CalculateGrossPayForEmployeeAsync(companyId, dto);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRecordAsync(string? PayScheduleId, DateTime NextPayDay)
        {
            var companyId = User.FindFirst("cid")!.Value;

            if (companyId is null)
                return BadRequest("invalid company");

            var response = await _paycheckService.GetTotalRecordForAllPayChecks(companyId, PayScheduleId, NextPayDay);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPost("PagedGrossPay")]
        public async Task<IActionResult> GetPaginatedGrossPayAsync([FromBody] PayCheckFilter pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var compnayId = User!.FindFirst("cid")!.Value;

            var response = await _paycheckService.GetPaginatedGrossPayAsync(pagination, compnayId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

    }
}
