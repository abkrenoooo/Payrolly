using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.TaxWithholding;
using Payrolly.BLL.IServices;

namespace Payrolly.Api.Controllers
{
    [Route("api/Tax Withholding")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TaxWithholdingController : ControllerBase
    {
        private readonly ITaxWithholdingServices _taxWithholdingServices;

        public TaxWithholdingController(ITaxWithholdingServices taxWithholdingServices)
        {
            _taxWithholdingServices = taxWithholdingServices;
        }
        [HttpPost("add / Edit")]
        public async Task<IActionResult> CreateTaxWithholdingAsync([FromBody] TaxWithholdingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _taxWithholdingServices.CreateTaxWithholdingAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("GET {WithholdingId}")]
        public async Task<IActionResult> GetTaxWithholdingAsync(string EmployeeId)
        {
            var response = await _taxWithholdingServices.GetTaxWithholdingAsync(EmployeeId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }


        //[HttpPatch("update-Tax Withholding")]
        //public async Task<IActionResult> UpdatePersonalInfoAsync([FromBody] TaxWithholdingDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var response = await _taxWithholdingServices.UpdateTaxWithholdingAsync(dto);

        //    if (!response.Success)
        //        return BadRequest(response.Message);

        //    return Ok(response);
        //}
    }
}
