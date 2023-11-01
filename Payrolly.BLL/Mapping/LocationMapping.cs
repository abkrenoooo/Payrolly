using Payrolly.BLL.DTOs.Location;
using Payrolly.BLL.DTOs.User;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class LocationMapping
    {
        public static Location ToLocationEntity(this LocationDto dto, string companyId)
            => new Location()
            {
                Id = Guid.NewGuid().ToString(),
                Address = dto.Address,
                Country = dto.Country,
                State = dto.State,
                City = dto.City,
                StreetAddress = dto.StreetAddress,
                ZIPCode = dto.ZIPCode,
                CompanyId = companyId
            };

        public static LocationDto ToLocationDto(this Location location)
            => new LocationDto
            {
                Id = location.Id,
                Address = location.Address,
                Country = location.Country,
                State = location.State,
                City = location.City,
                StreetAddress = location.StreetAddress,
                ZIPCode = location.ZIPCode
            };

        public static Location FromRegisterUserToLocationEntity(this RegisterUserDto dto, Company company)
            => new Location()
            {
                Id = Guid.NewGuid().ToString(),
                Address = dto.Address,
                Country = dto.Country,
                State = dto.State,
                City = dto.City,
                StreetAddress = dto.StreetAddress,
                ZIPCode = dto.ZIPCode,
                Company = company,
                CompanyId = company.Id
            };
    }
}
