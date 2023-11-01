using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payrolly.BLL.DTOs.User;
using Payrolly.BLL.Helpers;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Data;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Payrolly.BLL.Services
{
    public class UserService : IUserService
    {
        #region Private Properties
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPayScheduleRepository _payScheduleRepository;
        private readonly JWT _jwt;
        #endregion

        #region Constructor
        public UserService(
            UserManager<ApplicationUser> userManager,
            IOptions<JWT> options,
            ICompanyRepository companyRepository,
            ILocationRepository locationRepository,
            IPayScheduleRepository payScheduleRepository)
        {
            _userManager = userManager;
            _jwt = options.Value;
            _companyRepository = companyRepository;
            _locationRepository = locationRepository;
            _payScheduleRepository = payScheduleRepository;
        }
        #endregion

        #region Authentication Services
        public async Task<GenericResponse<UserDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                // Check if email or password true or not
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                    return new() { Message = "Invalid password or email" };


                // create token for the user
                var jwtSecurityToken = await CreateJwtToken(user);


                // return
                var company = await _companyRepository.GetByIDAsync<string>(user.CompanyID);
                //user.Company = company;
                var userDto = user.ToUserDto(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "logined",
                    Data = userDto
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Not Found"
                };
            }
        }

        public async Task<GenericResponse<UserDto>> RegisterUserAsync(RegisterUserDto dto)
        {
            try
            {
                // check if the user exist or not by user email
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user != null)
                    return new() { Message = "User is already exists" };

                // add new company
                var company = new Company { Id = Guid.NewGuid().ToString(), CompanyName = dto.CompanyName };

                if (!await _companyRepository.CreateAsync(company))
                    return new() { Message = "cannot register" };

                // add new location
                var location = dto.FromRegisterUserToLocationEntity(company);


                // add default pay schedule for the company
                PaySchedule paySchedule = new()
                {
                    PayScheduleId = Guid.NewGuid().ToString(),
                    Name = "Every Friday",
                    PayFrequencyTypes = PayFrequencyTypes.EveryWeek,
                    NextPayDay = DateTime.Now,
                    NextPayPeriod = DateTime.Now,
                    CompanyId = company.Id,
                };

                // create pay schedule and location in DB
                if (!await _locationRepository.CreateAsync(location) || !await _payScheduleRepository.CreateAsync(paySchedule))
                    return new() { Message = "cannot register" };

                // add new user
                var newUser = dto.FromRegisterToUserEntity(company);

                var result = await _userManager.CreateAsync(newUser, dto.Password);

                if (!result.Succeeded)
                    return new() { Message = "cannot register user" };

                // assign the user for a role
                await _userManager.AddToRoleAsync(newUser, RoleTypes.Admin.ToString());

                // add claim company id for the user
                await _userManager.AddClaimAsync(newUser, new Claim("cid", company.Id));

                var jwtSecurityToken = await CreateJwtToken(newUser);
                var userDto = newUser.ToUserDto(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Created",
                    Data = userDto
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
                return new() { Message = ex.Message };
            }

        }
        #endregion

        #region Adminstration Services
        public async Task<GenericResponse<object>> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new() { Message = "invalid id" };

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new() { Message = "error in in activate the user" };

            return new()
            {
                Success = true,
                StatusCode = 201,
                Message = "deleted"
            };
        }

        public async Task<GenericResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.
                Where(u => u.IsActive)
                .ToListAsync();

            var usersDto = users.Select(u => u.ToUserDto()).ToList();

            return new()
            {
                Success = true,
                StatusCode = 200,
                Message = "valid data",
                Data = usersDto
            };
        }
        #endregion

        #region Private Methods
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            #region Determine Claims for the token

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            #endregion

            #region Determine Signing Credentials

            var key = Encoding.UTF8.GetBytes(_jwt.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            #endregion

            #region Create And Return

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

            #endregion
        }
        #endregion
    }
}
