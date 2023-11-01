using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Tax;
using Payrolly.BLL.IServices;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TaxesController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxesController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost("General-tax")]
        public async Task<IActionResult> CreateGeneralTaxAsync([FromBody] FederalTaxDto dto)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.UpdateFederalTaxAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        [HttpPatch("federal-tax")]
        public async Task<IActionResult> UpdateFederalTaxAsync([FromBody] FederalTaxDto dto)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.UpdateFederalTaxAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPatch("state-tax")]
        public async Task<IActionResult> UpdateStateTaxAsync([FromBody] StateTaxDto dto)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.UpdateStateTaxAsync(dto, companyId, StateTaxType.StaticStateTax);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPatch("local-state-tax")]
        public async Task<IActionResult> UpdateLocalStateTaxAsync([FromBody] StateTaxDto dto)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.UpdateStateTaxAsync(dto, companyId, StateTaxType.StaticLocalStateTax);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("federal-tax")]
        public async Task<IActionResult> GetFederalTaxAsync()
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.GetFederalTaxDto(companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("state-tax/{stateName}")]
        public async Task<IActionResult> GetStateTaxAsync(string stateName)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.GetStateTaxDto(stateName, companyId, StateTaxType.StaticStateTax);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("local-state-tax/{stateName}")]
        public async Task<IActionResult> GetLocalStateTaxAsync(string stateName)
        {
            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxService.GetStateTaxDto(stateName, companyId, StateTaxType.StaticLocalStateTax);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
