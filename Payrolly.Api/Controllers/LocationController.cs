using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.Location;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Services;
using Payrolly.DAL.Interface;
using System.Security.Claims;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        #region POST Actions
        [HttpPost("add")]
        public async Task<IActionResult> CreateLocationAsync([FromBody] LocationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = User!.FindFirst("cid")!.Value;

            var response = await _locationService.CreateLocationAsync(dto, companyId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion

        #region GET Actions 

        [HttpGet]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            var compnayId = User!.FindFirst("cid")!.Value;

            var response = await _locationService.GetAllLocationsAsync(compnayId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationAsync(string id)
        {
            var response = await _locationService.GetLocationAsync(id);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
        #endregion
    }
}
