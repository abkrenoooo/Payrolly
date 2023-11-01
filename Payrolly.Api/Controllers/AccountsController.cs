using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payrolly.BLL.DTOs.User;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Entities;
using System.Security.Claims;

namespace Payrolly.Api.Controllers
{
    [Route("api/[controller]")] // server/api/accounts
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Private Properties
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructor
        public AccountsController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }
        #endregion

        #region Authentication Actions
        [HttpPost("register")] // api/accounts/register
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _userService.RegisterUserAsync(dto);

                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")] // api/accounts/login
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _userService.LoginAsync(dto);

                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update Security Actions
        [HttpPatch("security/update-email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmailAsync([FromBody] UpdateEmailDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound();
            }

            user.Email = dto.Email;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "email updated",
                    Data = user.ToUserDto()
                });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPatch("security/update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "password updated",
                    Data = user.ToUserDto()
                }); ;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPatch("security/update-phone")]
        [Authorize]
        public async Task<IActionResult> UpdatePhone([FromBody] string phoneNumber )
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.PhoneNumber = phoneNumber;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "phone updated",
                    Data = user.ToUserDto()
                }); ;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        #endregion

        #region Update Personal Actions
        [HttpPatch("personal/update-name")]
        [Authorize]
        public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateNameDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "name updated",
                    Data = user.ToUserDto()
                }); ;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPatch("personal/update-birthOfDate")]
        [Authorize]
        public async Task<IActionResult> UpdateDateOfBirthAsync([FromBody] DateTime DateOfBirth)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.DateOfBirth = DateOfBirth;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "date of birth updated",
                    Data = user.ToUserDto()
                });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPatch("personal/update-occupation")]
        [Authorize]
        public async Task<IActionResult> UpdateOccupationAsync([FromBody] string Occupation)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Occupation = Occupation;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "occupation updated",
                    Data = user.ToUserDto()
                }); ;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPatch("personal/update-address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressDto addressDto)
        {
            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Country = addressDto.Country;
            user.Address = addressDto.Address;
            user.StreetAddress = addressDto.StreetAddress;
            user.City = addressDto.City;
            user.State = addressDto.State;
            user.ZIPCode = addressDto.ZIPCode;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GenericResponse<UserDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "address updated",
                    Data = user.ToUserDto()
                });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        #endregion

    }
}
