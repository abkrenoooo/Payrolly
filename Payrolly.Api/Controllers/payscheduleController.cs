using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.BLL.Filters;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Services;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PayScheduleController : ControllerBase
    {

        private readonly IPayScheduleService _payScheduleService;

        public PayScheduleController(IPayScheduleService payScheduleService)
        {
            _payScheduleService = payScheduleService;
        }

        #region POST Actions

        [HttpPost("add")]
        public async Task<IActionResult> CreatePayScheduleAsync([FromBody] PayScheduleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _payScheduleService.CreatePayScheduleAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion

        #region GET Actions 

        [HttpGet]
        public async Task<IActionResult> GetAllPayScheduleAsync()
        {
            var compnayId = User!.FindFirst("cid")!.Value;

            var response = await _payScheduleService.GetAllPayschedulesAsync(compnayId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPayScheduleAsync(string name)
        {
            var response = await _payScheduleService.GetPayScheduleAsync(name);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion

        [HttpGet("payperiod")]
        public async Task<IActionResult> GetPayPeriodAsync(string PayScheduleId)
        {
            var companyId = User.FindFirst("cid").Value;

            if (companyId is null)
                return BadRequest();

            var resposne = await _payScheduleService.GetPayPeriodAsync(companyId, PayScheduleId);

            if (!resposne.Success)
                return BadRequest(resposne.Message);

            return Ok(resposne);
        }
    }
}
