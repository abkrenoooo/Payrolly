using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.User;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class UserMapping
    {
        public static ApplicationUser FromRegisterToUserEntity(this RegisterUserDto dto, Company company)
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                //Company = company,
                CompanyID = company.Id
            };
        }

        public static UserDto ToUserDto(this ApplicationUser user, string? token = null)
        {
            return new()
            {
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
               //Company=user.Company,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CompanyId = user.CompanyID,
                DateOfBirth = user.DateOfBirth,
                ImageUrl = user.ImageUrl,
                Occupation = user.Occupation,
                Address = user.Address,
                Country = user.Country,
                City = user.City,
                State = user.State,
                StreetAddress = user.StreetAddress,
                ZIPCode = user.ZIPCode
            };
        }

        #region ToUserDto
        public static ApplicationUser ToApplicationUserDto(this UserDto user)
        {
            return new()
            {
               
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
               Company=user.Company,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                State = user.State,
                StreetAddress = user.StreetAddress,
                ZIPCode = user.ZIPCode,
                PhoneNumber = user.PhoneNumber, 
                ImageUrl = user.ImageUrl,
            };
        }
        #endregion
    }
}
